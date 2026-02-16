using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

/// <summary>
/// Central brain dissection controller.
///
/// THE #1 RULE: Everything stays DIRECTLY IN FRONT of the user.
///   - Brain never moves from its visual center position
///   - Rotation: compute visual center, rotate root, snap center back
///   - Split: tiny separation scaled to actual brain size
///   - Hemisphere select: just show/hide, zero position changes
///   - Zoom: move XR rig toward whatever is being viewed
/// </summary>
public class BrainManager : MonoBehaviour
{
    [Header("Hemisphere References")]
    public GameObject leftHemisphere;
    public GameObject rightHemisphere;

    [Header("Brain Root")]
    public GameObject brainRoot;

    [Header("Kidney Tray (hemispheres go here after split)")]
    public Transform kidneyTray;

    [Header("UI")]
    public RegionUIController regionUIController;

    // ---- State ----
    public enum ViewState { WholeBrain, LeftFocused, RightFocused, RegionSelected }
    private ViewState _currentState = ViewState.WholeBrain;
    public bool IsInspectingRegion => _currentState == ViewState.RegionSelected;

    // ---- Selected region ----
    private BrainRegion _selectedRegion;
    private RegionInspector _activeInspector;
    private ViewState _stateBeforeRegionSelect; // so PutBack knows which hemisphere to restore

    // ---- ORIGINAL state (set once at Start, NEVER modified) ----
    private Vector3 _originalRootPosition;
    private Quaternion _originalRootRotation;

    // ---- Current locked root position (updated after rotation to keep center fixed) ----
    private Vector3 _lockedRootPosition;
    private bool _initialized;

    // ---- Grab-to-rotate: user holds Grip on brain, release stops immediately ----
    private Transform _rotateInteractor;
    private Quaternion _lastInteractorRotation;
    private bool _hasLastInteractorRotation;
    private bool _rotateIsLeftHand;

    // (Hemisphere positions are never modified - brain stays in place)

    // ---- Opacity ----
    private float _brainOpacity = 1f;
    private List<MaterialData> _materialCache = new List<MaterialData>();
    private bool _materialsCached;

    private struct MaterialData
    {
        public Renderer renderer;
        public Material material;
        public Color originalColor;
        public int originalRenderQueue;
    }

    private void Start()
    {
        if (brainRoot == null) return;

        _originalRootPosition = brainRoot.transform.position;
        _originalRootRotation = brainRoot.transform.rotation;
        _lockedRootPosition = _originalRootPosition;

        _initialized = true;
        CacheMaterials();

        Debug.Log($"[BrainManager] Init. Root={_originalRootPosition}, " +
                  $"VisualCenter={ComputeVisualCenter()}");
    }

    // ===================== POSITION ENFORCEMENT =====================

    private void Update()
    {
        // Stop rotating as soon as user releases Grip (activate) — don't rely on deactivate event
        // (ray can move off brain while rotating, so brain might not receive OnDeactivated)
        if (_rotateInteractor != null && !IsRotateButtonStillHeld())
        {
            EndUserRotate();
        }
    }

    private void LateUpdate()
    {
        if (!_initialized || brainRoot == null) return;

        // Apply grab-to-rotate: controller rotation delta drives brain rotation, center stays fixed
        if (_rotateInteractor != null)
        {
            Quaternion currentRot = _rotateInteractor.rotation;
            if (_hasLastInteractorRotation)
            {
                Quaternion delta = currentRot * Quaternion.Inverse(_lastInteractorRotation);
                ApplyRotationKeepingCenterFixed(delta);
            }
            _lastInteractorRotation = currentRot;
            _hasLastInteractorRotation = true;
        }
        else
        {
            _hasLastInteractorRotation = false;
        }

        brainRoot.transform.position = _lockedRootPosition;
    }

    /// <summary>True if the hand that started rotate is still holding Grip (activate).</summary>
    private bool IsRotateButtonStillHeld()
    {
        var device = InputDevices.GetDeviceAtXRNode(_rotateIsLeftHand ? XRNode.LeftHand : XRNode.RightHand);
        if (!device.isValid) return false;
        // Grip is typically the "activate" binding in XR Controller
        if (device.TryGetFeatureValue(CommonUsages.gripButton, out bool grip) && grip) return true;
        // Fallback: some setups bind activate to trigger when not selecting
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool trigger) && trigger) return true;
        return false;
    }

    // ===================== VISUAL CENTER =====================

    /// <summary>World-space center of all visible brain renderers.</summary>
    private Vector3 ComputeVisualCenter()
    {
        if (brainRoot == null) return Vector3.zero;
        Bounds b = new Bounds();
        bool first = true;
        foreach (var r in brainRoot.GetComponentsInChildren<Renderer>(true))
        {
            if (r == null || !r.enabled) continue;
            if (first) { b = r.bounds; first = false; }
            else b.Encapsulate(r.bounds);
        }
        return first ? brainRoot.transform.position : b.center;
    }

    // ===================== MATERIAL CACHE =====================

    private void CacheMaterials()
    {
        _materialCache.Clear();
        if (brainRoot == null) return;
        foreach (var rend in brainRoot.GetComponentsInChildren<Renderer>(true))
        {
            if (rend == null) continue;
            foreach (var mat in rend.materials)
            {
                if (mat == null) continue;
                _materialCache.Add(new MaterialData
                {
                    renderer = rend,
                    material = mat,
                    originalColor = mat.color,
                    originalRenderQueue = mat.renderQueue
                });
            }
        }
        _materialsCached = true;
    }

    // ===================== BRAIN SPLIT =====================

    // Saved original hierarchy state so we can undo the split
    private Transform _leftHemiOrigParent;
    private Transform _rightHemiOrigParent;
    private Vector3 _leftHemiOrigLocal;
    private Vector3 _rightHemiOrigLocal;
    private Quaternion _leftHemiOrigRotation;
    private Quaternion _rightHemiOrigRotation;
    private Vector3 _leftHemiOrigScale;
    private Vector3 _rightHemiOrigScale;
    private bool _hemiPositionsSaved;

    // Saved kidney tray positions (world space) so we can send hemispheres back
    private Vector3 _leftKidneyPos;
    private Vector3 _rightKidneyPos;
    private Quaternion _leftKidneyRot;
    private Quaternion _rightKidneyRot;

    /// <summary>
    /// On split: both hemispheres move into the KidneyTray, spaced apart side by side.
    /// The surgical tray (brain's original position) becomes empty.
    /// Both hemispheres remain VISIBLE in the kidney tray.
    /// </summary>
    public void PerformBrainSplit()
    {
        // Save original state (once)
        if (!_hemiPositionsSaved)
        {
            if (leftHemisphere != null)
            {
                _leftHemiOrigParent = leftHemisphere.transform.parent;
                _leftHemiOrigLocal = leftHemisphere.transform.localPosition;
                _leftHemiOrigRotation = leftHemisphere.transform.localRotation;
                _leftHemiOrigScale = leftHemisphere.transform.localScale;
            }
            if (rightHemisphere != null)
            {
                _rightHemiOrigParent = rightHemisphere.transform.parent;
                _rightHemiOrigLocal = rightHemisphere.transform.localPosition;
                _rightHemiOrigRotation = rightHemisphere.transform.localRotation;
                _rightHemiOrigScale = rightHemisphere.transform.localScale;
            }
            _hemiPositionsSaved = true;
        }

        if (kidneyTray != null)
        {
            // Use the kidney tray's world-space bounds to find the tray's longest horizontal axis
            Bounds trayBounds = ComputeWorldBounds(kidneyTray.gameObject);
            Vector3 trayCenter = trayBounds.center;
            float trayTop = trayBounds.max.y;

            // Find the tray's longest horizontal dimension (X or Z) for spacing direction
            bool spreadAlongZ = trayBounds.size.z > trayBounds.size.x;

            // Compute hemisphere world-space size for spacing
            float hemiSize = 0f;
            if (leftHemisphere != null)
            {
                Bounds lb = ComputeWorldBounds(leftHemisphere);
                hemiSize = spreadAlongZ ? lb.size.z : lb.size.x;
            }
            // spacing = half a hemisphere width + generous gap so they're clearly separated
            // minimum 5cm, typically ~60% of hemisphere size
            float spacing = Mathf.Max(0.05f, hemiSize * 0.6f);

            // Place left hemisphere
            if (leftHemisphere != null)
            {
                leftHemisphere.transform.SetParent(kidneyTray, true);
                Bounds lb = ComputeWorldBounds(leftHemisphere);
                Vector3 leftTarget;
                if (spreadAlongZ)
                    leftTarget = new Vector3(trayCenter.x, trayTop, trayCenter.z - spacing);
                else
                    leftTarget = new Vector3(trayCenter.x - spacing, trayTop, trayCenter.z);
                leftHemisphere.transform.position += (leftTarget - lb.center);

                _leftKidneyPos = leftHemisphere.transform.position;
                _leftKidneyRot = leftHemisphere.transform.rotation;
            }

            // Place right hemisphere
            if (rightHemisphere != null)
            {
                rightHemisphere.transform.SetParent(kidneyTray, true);
                Bounds rb = ComputeWorldBounds(rightHemisphere);
                Vector3 rightTarget;
                if (spreadAlongZ)
                    rightTarget = new Vector3(trayCenter.x, trayTop, trayCenter.z + spacing);
                else
                    rightTarget = new Vector3(trayCenter.x + spacing, trayTop, trayCenter.z);
                rightHemisphere.transform.position += (rightTarget - rb.center);

                _rightKidneyPos = rightHemisphere.transform.position;
                _rightKidneyRot = rightHemisphere.transform.rotation;
            }

            // Both visible in the kidney tray
            SetHemisphereVisible(leftHemisphere, true);
            SetHemisphereVisible(rightHemisphere, true);

            Debug.Log($"[BrainManager] Brain split -> hemispheres in KidneyTray. " +
                      $"Spread along {(spreadAlongZ ? "Z" : "X")}, spacing={spacing:F3}");
        }
        else
        {
            // Fallback: no kidney tray
            float localGap = 0.01f;
            if (leftHemisphere != null)
                leftHemisphere.transform.localPosition = _leftHemiOrigLocal + new Vector3(-localGap, 0, 0);
            if (rightHemisphere != null)
                rightHemisphere.transform.localPosition = _rightHemiOrigLocal + new Vector3(localGap, 0, 0);
            Debug.Log("[BrainManager] Brain split (no KidneyTray, small local separation).");
        }
    }

    /// <summary>Restore hemispheres to their original parent and local positions (BrainRoot).</summary>
    private void UndoBrainSplit()
    {
        if (!_hemiPositionsSaved) return;
        ReturnToSurgicalTray(leftHemisphere, _leftHemiOrigParent, _leftHemiOrigLocal, _leftHemiOrigRotation, _leftHemiOrigScale);
        ReturnToSurgicalTray(rightHemisphere, _rightHemiOrigParent, _rightHemiOrigLocal, _rightHemiOrigRotation, _rightHemiOrigScale);
    }

    /// <summary>Send a hemisphere back to the kidney tray at its saved position.</summary>
    private void SendToKidneyTray(GameObject hemi, Vector3 savedPos, Quaternion savedRot)
    {
        if (hemi == null || kidneyTray == null) return;
        hemi.transform.SetParent(kidneyTray, true);
        hemi.transform.position = savedPos;
        hemi.transform.rotation = savedRot;
        SetHemisphereVisible(hemi, true);
    }

    /// <summary>Return a hemisphere to surgical tray (BrainRoot) at its original local transform.</summary>
    private void ReturnToSurgicalTray(GameObject hemi, Transform origParent,
        Vector3 origLocalPos, Quaternion origLocalRot, Vector3 origLocalScale)
    {
        if (hemi == null || !_hemiPositionsSaved) return;
        hemi.transform.SetParent(origParent, false);
        hemi.transform.localPosition = origLocalPos;
        hemi.transform.localRotation = origLocalRot;
        hemi.transform.localScale = origLocalScale;
        SetHemisphereVisible(hemi, true);
    }

    /// <summary>World-space bounds of any GameObject (from all child renderers).</summary>
    private Bounds ComputeWorldBounds(GameObject go)
    {
        Bounds b = new Bounds(go.transform.position, Vector3.zero);
        bool first = true;
        foreach (var r in go.GetComponentsInChildren<Renderer>(true))
        {
            if (r == null) continue;
            if (first) { b = r.bounds; first = false; }
            else b.Encapsulate(r.bounds);
        }
        return b;
    }

    // ===================== HEMISPHERE SELECTION =====================
    // Selected hemisphere returns to surgical tray. Other stays VISIBLE in kidney tray.

    public void ShowLeftHemisphere()
    {
        if (_currentState == ViewState.RegionSelected) return;
        if (LabToolManager.Instance != null && !LabToolManager.Instance.brainIsSplit) return;

        // Send RIGHT back to kidney tray (visible there)
        SendToKidneyTray(rightHemisphere, _rightKidneyPos, _rightKidneyRot);

        // Bring LEFT to surgical tray (visible, in front of user)
        ReturnToSurgicalTray(leftHemisphere, _leftHemiOrigParent, _leftHemiOrigLocal, _leftHemiOrigRotation, _leftHemiOrigScale);

        _currentState = ViewState.LeftFocused;
        Debug.Log("[BrainManager] Viewing Left Hemisphere on surgical tray. Right in kidney tray.");
    }

    public void ShowRightHemisphere()
    {
        if (_currentState == ViewState.RegionSelected) return;
        if (LabToolManager.Instance != null && !LabToolManager.Instance.brainIsSplit) return;

        // Send LEFT back to kidney tray (visible there)
        SendToKidneyTray(leftHemisphere, _leftKidneyPos, _leftKidneyRot);

        // Bring RIGHT to surgical tray (visible, in front of user)
        ReturnToSurgicalTray(rightHemisphere, _rightHemiOrigParent, _rightHemiOrigLocal, _rightHemiOrigRotation, _rightHemiOrigScale);

        _currentState = ViewState.RightFocused;
        Debug.Log("[BrainManager] Viewing Right Hemisphere on surgical tray. Left in kidney tray.");
    }

    public void ShowWholeBrain()
    {
        if (_currentState == ViewState.RegionSelected) return;

        // Both hemispheres back to surgical tray, fully visible
        ReturnToSurgicalTray(leftHemisphere, _leftHemiOrigParent, _leftHemiOrigLocal, _leftHemiOrigRotation, _leftHemiOrigScale);
        ReturnToSurgicalTray(rightHemisphere, _rightHemiOrigParent, _rightHemiOrigLocal, _rightHemiOrigRotation, _rightHemiOrigScale);

        _currentState = ViewState.WholeBrain;
    }

    private void SetHemisphereVisible(GameObject hemi, bool visible)
    {
        if (hemi == null) return;
        foreach (var r in hemi.GetComponentsInChildren<Renderer>(true))
            if (r != null) r.enabled = visible;
    }

    // ===================== ROTATE =====================

    /// <summary>
    /// Apply a rotation to the brain root while keeping the visual center fixed in world space.
    /// Used by both the old 15° button and by grab-to-rotate.
    /// </summary>
    private void ApplyRotationKeepingCenterFixed(Quaternion deltaRotation)
    {
        if (brainRoot == null) return;

        Vector3 centerBefore = ComputeVisualCenter();
        brainRoot.transform.rotation = deltaRotation * brainRoot.transform.rotation;
        Vector3 centerAfter = ComputeVisualCenter();
        brainRoot.transform.position += (centerBefore - centerAfter);
        _lockedRootPosition = brainRoot.transform.position;
    }

    /// <summary>
    /// Start grab-to-rotate: user holds Grip (activate) on the brain. Only allowed after brain is sliced.
    /// Works with or without tweezers. Trigger = pick region when tweezers held.
    /// </summary>
    public void StartUserRotate(Transform interactorTransform)
    {
        if (interactorTransform == null) return;
        if (LabToolManager.Instance != null && !LabToolManager.Instance.glovesEquipped) return;
        if (LabToolManager.Instance != null && !LabToolManager.Instance.brainIsSplit) return;
        if (_currentState == ViewState.RegionSelected) return;

        _rotateInteractor = interactorTransform;
        _lastInteractorRotation = interactorTransform.rotation;
        _hasLastInteractorRotation = false;
        // Determine which hand so we can poll grip release (deactivate may not fire if ray left the brain)
        string name = interactorTransform.name ?? "";
        _rotateIsLeftHand = name.IndexOf("left", System.StringComparison.OrdinalIgnoreCase) >= 0;
    }

    /// <summary>
    /// End grab-to-rotate when user releases trigger.
    /// </summary>
    public void EndUserRotate()
    {
        _rotateInteractor = null;
        _hasLastInteractorRotation = false;
    }

    /// <summary>
    /// Rotates the brain 15 degrees (e.g. from UI button). Kept for accessibility.
    /// </summary>
    public void RotateBrain()
    {
        if (brainRoot == null) return;
        if (LabToolManager.Instance != null && !LabToolManager.Instance.glovesEquipped) return;

        ApplyRotationKeepingCenterFixed(Quaternion.Euler(0f, 15f, 0f));
    }

    // ===================== ZOOM =====================

    public void ZoomIn()
    {
        if (LabToolManager.Instance != null && !LabToolManager.Instance.glovesEquipped) return;
        MoveXRRigToward(0.10f);
    }

    public void ZoomOut()
    {
        if (LabToolManager.Instance != null && !LabToolManager.Instance.glovesEquipped) return;
        MoveXRRigToward(-0.10f);
    }

    private void MoveXRRigToward(float distance)
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        Transform xrRig = cam.transform;
        while (xrRig.parent != null && xrRig.parent.name != "DontDestroyOnLoad")
            xrRig = xrRig.parent;

        // Zoom toward whatever the user is currently looking at
        Vector3 target;
        if (_currentState == ViewState.RegionSelected && _selectedRegion != null)
        {
            // Zoom toward selected region
            var rend = _selectedRegion.GetComponent<Renderer>();
            target = rend != null ? rend.bounds.center : _selectedRegion.transform.position;
        }
        else
        {
            // Zoom toward brain visual center
            target = ComputeVisualCenter();
        }

        Vector3 dir = (target - cam.transform.position).normalized;
        if (dir.sqrMagnitude < 0.001f) dir = cam.transform.forward;
        xrRig.position += dir * distance;
    }

    // ===================== HOVER =====================

    public void OnRegionHoverEnter(BrainRegion region)
    {
        if (region == null || region.regionData == null) return;
        if (_currentState == ViewState.RegionSelected) return;
        regionUIController?.ShowHoverName(region.regionData.displayName);
    }

    public void OnRegionHoverExit(BrainRegion region)
    {
        if (_currentState == ViewState.RegionSelected) return;
        regionUIController?.ClearHoverName();
    }

    // ===================== SELECT REGION =====================

    public void OnRegionSelected(BrainRegion region)
    {
        if (region == null || region.regionData == null) return;
        if (_currentState == ViewState.RegionSelected) return;

        // Remember which state we were in so PutBack can restore it
        _stateBeforeRegionSelect = _currentState;

        _selectedRegion = region;
        _currentState = ViewState.RegionSelected;

        // Determine which hemisphere this region belongs to
        bool isLeftRegion = IsChildOf(region.gameObject, leftHemisphere);
        bool isRightRegion = IsChildOf(region.gameObject, rightHemisphere);

        // Hide the hemisphere the region came from (the region itself stays visible)
        // Keep the OTHER hemisphere visible in the kidney tray
        if (isLeftRegion)
        {
            // Hide left hemisphere regions except the selected one
            SetVisExcept(leftHemisphere, region.gameObject);
            // Right stays visible in kidney tray (don't touch it)
        }
        else if (isRightRegion)
        {
            // Hide right hemisphere regions except the selected one
            SetVisExcept(rightHemisphere, region.gameObject);
            // Left stays visible in kidney tray (don't touch it)
        }
        else
        {
            // Fallback: hide everything except selection
            HideAllExcept(region.gameObject);
        }

        // Freeze physics
        var rb = region.GetComponent<Rigidbody>();
        if (rb != null) { rb.isKinematic = true; rb.useGravity = false; }

        // Add slow in-place auto-rotation
        _activeInspector = region.gameObject.GetComponent<RegionInspector>();
        if (_activeInspector == null)
            _activeInspector = region.gameObject.AddComponent<RegionInspector>();
        _activeInspector.StartInspecting();

        // Show details panel
        regionUIController?.ClearHoverName();
        regionUIController?.ShowRegionDetails(region.regionData);

        Debug.Log($"[BrainManager] Selected region: {region.regionData.displayName} " +
                  $"(from {(isLeftRegion ? "Left" : isRightRegion ? "Right" : "Unknown")} hemisphere)");
    }

    /// <summary>Check if a GameObject is a child of another.</summary>
    private bool IsChildOf(GameObject child, GameObject parent)
    {
        if (child == null || parent == null) return false;
        return child.transform.IsChildOf(parent.transform);
    }

    // ===================== OPACITY =====================

    public void SetBrainOpacity(float opacity)
    {
        if (LabToolManager.Instance != null && !LabToolManager.Instance.glovesEquipped) return;

        _brainOpacity = Mathf.Clamp01(opacity);
        if (!_materialsCached) CacheMaterials();

        foreach (var md in _materialCache)
        {
            if (md.material == null || md.renderer == null) continue;

            if (_currentState == ViewState.RegionSelected && _selectedRegion != null)
            {
                if (md.renderer.gameObject == _selectedRegion.gameObject) continue;
                if (md.renderer.transform.IsChildOf(_selectedRegion.transform)) continue;
            }

            Color c = md.originalColor;
            c.a = _brainOpacity;
            md.material.color = c;

            if (_brainOpacity < 0.99f)
            {
                if (md.material.HasProperty("_Surface")) md.material.SetFloat("_Surface", 1);
                md.material.SetOverrideTag("RenderType", "Transparent");
                if (md.material.HasProperty("_SrcBlend"))
                    md.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                if (md.material.HasProperty("_DstBlend"))
                    md.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                if (md.material.HasProperty("_ZWrite"))
                    md.material.SetInt("_ZWrite", 0);
                md.material.renderQueue = 3000;
                md.material.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
                md.material.DisableKeyword("_SURFACE_TYPE_OPAQUE");
            }
            else
            {
                if (md.material.HasProperty("_Surface")) md.material.SetFloat("_Surface", 0);
                md.material.SetOverrideTag("RenderType", "Opaque");
                if (md.material.HasProperty("_SrcBlend"))
                    md.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                if (md.material.HasProperty("_DstBlend"))
                    md.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                if (md.material.HasProperty("_ZWrite"))
                    md.material.SetInt("_ZWrite", 1);
                md.material.renderQueue = md.originalRenderQueue;
                md.material.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");
                md.material.EnableKeyword("_SURFACE_TYPE_OPAQUE");
            }
        }

        if (_currentState == ViewState.RegionSelected && _selectedRegion != null && _brainOpacity > 0.01f)
        {
            ShowAllRegions();
            EnsureRegionFullyVisible(_selectedRegion);
        }

        if (_currentState == ViewState.RegionSelected && _selectedRegion != null)
            EnsureRegionFullyVisible(_selectedRegion);
    }

    private void EnsureRegionFullyVisible(BrainRegion region)
    {
        var rend = region.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.enabled = true;
            foreach (var mat in rend.materials)
            {
                if (mat == null) continue;
                Color c = mat.color; c.a = 1f; mat.color = c;
            }
        }
    }

    // ===================== PUT BACK REGION =====================

    /// <summary>
    /// Returns the selected region to its hemisphere. Does NOT reset the brain.
    /// After put-back, both hemispheres are visible in the kidney tray.
    /// The user can continue selecting other regions.
    /// </summary>
    public void PutBackRegion()
    {
        if (_currentState != ViewState.RegionSelected || _selectedRegion == null)
            return;

        Debug.Log("[BrainManager] PutBackRegion called");

        // Stop inspector rotation
        if (_activeInspector != null)
        {
            Destroy(_activeInspector);
            _activeInspector = null;
        }

        // Remove highlight
        _selectedRegion.SetHighlight(false);

        // Restore the region's original local transform (undoes RegionInspector auto-rotation)
        _selectedRegion.RestoreOriginalTransform();

        // Determine which hemisphere the region belongs to
        bool isLeftRegion = IsChildOf(_selectedRegion.gameObject, leftHemisphere);

        // Clear selection
        _selectedRegion = null;

        // Restore visibility of the hemisphere the region came from
        if (isLeftRegion)
            SetHemisphereVisible(leftHemisphere, true);
        else
            SetHemisphereVisible(rightHemisphere, true);

        // The OTHER hemisphere is already visible in the kidney tray (we never touched it)

        // Return to the state we were in before selecting the region
        // (both hemispheres in kidney tray, or one on surgical tray)
        _currentState = _stateBeforeRegionSelect;

        // If we were in a hemisphere-focused view, make sure that hemisphere is on surgical tray
        // and the other is in kidney tray
        if (_currentState == ViewState.LeftFocused)
        {
            ReturnToSurgicalTray(leftHemisphere, _leftHemiOrigParent, _leftHemiOrigLocal, _leftHemiOrigRotation, _leftHemiOrigScale);
            SendToKidneyTray(rightHemisphere, _rightKidneyPos, _rightKidneyRot);
        }
        else if (_currentState == ViewState.RightFocused)
        {
            ReturnToSurgicalTray(rightHemisphere, _rightHemiOrigParent, _rightHemiOrigLocal, _rightHemiOrigRotation, _rightHemiOrigScale);
            SendToKidneyTray(leftHemisphere, _leftKidneyPos, _leftKidneyRot);
        }
        else
        {
            // Both in kidney tray (came from WholeBrain state or directly from split)
            SendToKidneyTray(leftHemisphere, _leftKidneyPos, _leftKidneyRot);
            SendToKidneyTray(rightHemisphere, _rightKidneyPos, _rightKidneyRot);
        }

        // Reset opacity
        _brainOpacity = 1f;
        SetBrainOpacity(1f);
        if (regionUIController != null && regionUIController.opacitySlider != null)
            regionUIController.opacitySlider.value = 1f;

        regionUIController?.ClearHoverName();
        regionUIController?.HideRegionDetails();

        Debug.Log("[BrainManager] Region put back. Both hemispheres visible, dissection continues.");
    }

    // ===================== RESET (FULL) =====================

    public void ResetBrain()
    {
        Debug.Log("[BrainManager] ResetBrain called");

        if (_selectedRegion != null)
        {
            if (_activeInspector != null)
            {
                Destroy(_activeInspector);
                _activeInspector = null;
            }
            _selectedRegion.SetHighlight(false);
            _selectedRegion = null;
        }

        // Reset hemispheres to original local positions
        UndoBrainSplit();

        // Reset root to ORIGINAL position and rotation
        if (brainRoot != null)
        {
            brainRoot.transform.position = _originalRootPosition;
            brainRoot.transform.rotation = _originalRootRotation;
            _lockedRootPosition = _originalRootPosition;
        }

        ShowAllRegions();
        _currentState = ViewState.WholeBrain;

        // Reset opacity
        _brainOpacity = 1f;
        SetBrainOpacity(1f);
        if (regionUIController != null && regionUIController.opacitySlider != null)
            regionUIController.opacitySlider.value = 1f;

        regionUIController?.ClearHoverName();
        regionUIController?.HideRegionDetails();
    }

    // ===================== VISIBILITY =====================

    private void HideAllExcept(GameObject keepVisible)
    {
        if (leftHemisphere != null) SetVisExcept(leftHemisphere, keepVisible);
        if (rightHemisphere != null) SetVisExcept(rightHemisphere, keepVisible);
    }

    private void SetVisExcept(GameObject hemi, GameObject keep)
    {
        foreach (var r in hemi.GetComponentsInChildren<Renderer>(true))
        {
            if (r == null) continue;
            r.enabled = (r.gameObject == keep || r.transform.IsChildOf(keep.transform));
        }
    }

    private void ShowAllRegions()
    {
        SetHemisphereVisible(leftHemisphere, true);
        SetHemisphereVisible(rightHemisphere, true);
    }
}
