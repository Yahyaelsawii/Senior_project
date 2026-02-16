using UnityEngine;

/// <summary>
/// Bridge for UI buttons and slider to call BrainManager / LabToolManager methods.
/// Wired by the Editor setup script using persistent listeners.
///
/// All actions are gated through LabToolManager state checks.
/// Includes cooldown to prevent VR ray from firing buttons every frame.
/// </summary>
public class BrainDissectionUI : MonoBehaviour
{
    public BrainManager brainManager;

    // Cooldown to prevent button spam (XR ray fires continuously while trigger held)
    private float _buttonCooldown = 0.4f;
    private float _lastButtonTime = -10f;

    private bool CanPress()
    {
        if (Time.time - _lastButtonTime < _buttonCooldown) return false;
        _lastButtonTime = Time.time;
        return true;
    }

    // ---- Hemisphere Viewing (requires brain to be split) ----

    public void OnLeftClicked()
    {
        if (!CanPress()) return;
        if (!RequireGloves()) return;
        if (brainManager != null) brainManager.ShowLeftHemisphere();
    }

    public void OnRightClicked()
    {
        if (!CanPress()) return;
        if (!RequireGloves()) return;
        if (brainManager != null) brainManager.ShowRightHemisphere();
    }

    public void OnShowWholeClicked()
    {
        if (!CanPress()) return;
        if (!RequireGloves()) return;
        if (brainManager != null) brainManager.ShowWholeBrain();
    }

    // ---- Put Back Region (returns region to hemisphere, does NOT reset) ----

    public void OnPutBackClicked()
    {
        if (!CanPress()) return;
        if (brainManager != null) brainManager.PutBackRegion();
    }

    // ---- Reset (goes through LabToolManager to also reset split state) ----

    public void OnResetClicked()
    {
        if (!CanPress()) return;
        if (LabToolManager.Instance != null)
            LabToolManager.Instance.ResetLab();
        else if (brainManager != null)
            brainManager.ResetBrain();
    }

    // ---- Rotate / Zoom (requires gloves) ----

    public void OnRotateClicked()
    {
        if (!CanPress()) return;
        if (!RequireGloves()) return;
        if (brainManager != null) brainManager.RotateBrain();
    }

    public void OnZoomInClicked()
    {
        if (!CanPress()) return;
        if (!RequireGloves()) return;
        if (brainManager != null) brainManager.ZoomIn();
    }

    public void OnZoomOutClicked()
    {
        if (!CanPress()) return;
        if (!RequireGloves()) return;
        if (brainManager != null) brainManager.ZoomOut();
    }

    // ---- Opacity (requires gloves, NO cooldown -- slider is continuous) ----

    public void OnOpacityChanged(float value)
    {
        if (!RequireGloves()) return;
        if (brainManager != null) brainManager.SetBrainOpacity(value);
    }

    // ---- Helper ----

    private bool RequireGloves()
    {
        if (LabToolManager.Instance == null) return true;
        return LabToolManager.Instance.glovesEquipped;
    }
}
