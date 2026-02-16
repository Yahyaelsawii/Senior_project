using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Attach to each brain region GameObject. Holds RegionData and forwards hover/select/activate
/// to BrainManager.
///
/// Keybind mapping (while holding tweezers or not):
///   - Grip (Activate):  rotate the whole brain (only after brain is sliced)
///   - Trigger (Select): pick region for inspection (only when tweezers held)
/// </summary>
public class BrainRegion : MonoBehaviour
{
    [Tooltip("Display name and description for this region")]
    public RegionData regionData;

    [Tooltip("Optional: assign BrainManager. If not set, will be found in scene.")]
    public BrainManager brainManager;

    private XRBaseInteractable _interactable;
    private Renderer _renderer;
    private Color _originalColor;
    private string _colorProperty;
    private bool _startedBrainRotate;

    // Saved original transform so PutBackRegion can restore it after RegionInspector rotates it
    [HideInInspector] public Vector3 originalLocalPosition;
    [HideInInspector] public Quaternion originalLocalRotation;
    [HideInInspector] public Vector3 originalLocalScale;
    private bool _transformSaved;

    private void Awake()
    {
        _interactable = GetComponent<XRBaseInteractable>();
        if (_interactable == null)
            _interactable = GetComponentInChildren<XRBaseInteractable>();

        if (brainManager == null)
            brainManager = FindFirstObjectByType<BrainManager>();

        _renderer = GetComponent<Renderer>();
        if (_renderer != null && _renderer.material != null)
        {
            var mat = _renderer.material;
            if (mat.HasProperty("_BaseColor"))
                _colorProperty = "_BaseColor";
            else if (mat.HasProperty("_Color"))
                _colorProperty = "_Color";

            if (_colorProperty != null)
                _originalColor = mat.GetColor(_colorProperty);
        }
    }

    private void Start()
    {
        // Save the original local transform at startup so we can restore after inspection
        if (!_transformSaved)
        {
            originalLocalPosition = transform.localPosition;
            originalLocalRotation = transform.localRotation;
            originalLocalScale = transform.localScale;
            _transformSaved = true;
        }
    }

    /// <summary>Restore this region to its original local transform (undoes RegionInspector rotation).</summary>
    public void RestoreOriginalTransform()
    {
        if (!_transformSaved) return;
        transform.localPosition = originalLocalPosition;
        transform.localRotation = originalLocalRotation;
        transform.localScale = originalLocalScale;
    }

    private void OnEnable()
    {
        if (_interactable == null) return;
        _interactable.hoverEntered.AddListener(OnHoverEntered);
        _interactable.hoverExited.AddListener(OnHoverExited);
        _interactable.selectEntered.AddListener(OnSelectEntered);
        _interactable.selectExited.AddListener(OnSelectExited);
        _interactable.activated.AddListener(OnActivated);
        _interactable.deactivated.AddListener(OnDeactivated);
    }

    private void OnDisable()
    {
        if (_interactable == null) return;
        _interactable.hoverEntered.RemoveListener(OnHoverEntered);
        _interactable.hoverExited.RemoveListener(OnHoverExited);
        _interactable.selectEntered.RemoveListener(OnSelectEntered);
        _interactable.selectExited.RemoveListener(OnSelectExited);
        _interactable.activated.RemoveListener(OnActivated);
        _interactable.deactivated.RemoveListener(OnDeactivated);
        if (_startedBrainRotate && brainManager != null)
        {
            brainManager.EndUserRotate();
            _startedBrainRotate = false;
        }
    }

    // ========================= EVENT HANDLERS =========================

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Gate: gloves must be equipped to hover
        if (!AreGlovesEquipped()) return;

        SetHighlight(true);
        brainManager?.OnRegionHoverEnter(this);
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        SetHighlight(false);
        brainManager?.OnRegionHoverExit(this);
    }

    // Trigger (Select): pick region for inspection — only when tweezers held
    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (!AreGlovesEquipped())
        {
            ShowToolMessage("Please equip your gloves first.");
            return;
        }
        if (!AreTweezersHeld())
        {
            ShowToolMessage("Hold the tweezers to select a brain region.");
            return;
        }
        brainManager?.OnRegionSelected(this);
    }

    private void OnSelectExited(SelectExitEventArgs args) { }

    // Grip (Activate): rotate the whole brain — only after brain is sliced; works with or without tweezers
    private void OnActivated(ActivateEventArgs args)
    {
        if (!AreGlovesEquipped()) return;
        if (LabToolManager.Instance != null && !LabToolManager.Instance.brainIsSplit) return;

        Transform interactorTransform = (args.interactorObject as MonoBehaviour)?.transform;
        if (interactorTransform != null && brainManager != null)
        {
            brainManager.StartUserRotate(interactorTransform);
            _startedBrainRotate = true;
        }
    }

    private void OnDeactivated(DeactivateEventArgs args)
    {
        if (_startedBrainRotate)
        {
            brainManager?.EndUserRotate();
            _startedBrainRotate = false;
        }
    }

    // ========================= TOOL CHECKS =========================

    private bool AreGlovesEquipped()
    {
        return LabToolManager.Instance != null && LabToolManager.Instance.glovesEquipped;
    }

    private bool AreTweezersHeld()
    {
        return LabToolManager.Instance != null && LabToolManager.Instance.isHoldingTweezers;
    }

    private void ShowToolMessage(string msg)
    {
        if (brainManager != null && brainManager.regionUIController != null)
            brainManager.regionUIController.ShowHoverName(msg);
    }

    // ========================= VISUALS =========================

    /// <summary>Tint the region yellow on hover, restore original color on exit.</summary>
    public void SetHighlight(bool on)
    {
        if (_renderer == null || _colorProperty == null) return;
        var mat = _renderer.material;
        if (on)
            mat.SetColor(_colorProperty, new Color(1f, 0.9f, 0.3f, 1f));
        else
            mat.SetColor(_colorProperty, _originalColor);
    }

    /// <summary>Re-cache the original color (e.g. after materials are restored).</summary>
    public void RefreshOriginalColor()
    {
        if (_renderer != null && _colorProperty != null)
            _originalColor = _renderer.material.GetColor(_colorProperty);
    }
}
