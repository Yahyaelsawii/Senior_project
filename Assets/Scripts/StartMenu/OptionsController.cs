using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

/// <summary>
/// Connects Brightness and Contrast sliders to the URP post-processing Volume.
/// Brightness maps to ColorAdjustments.postExposure (range: -2 to +2 EV).
/// Contrast maps to ColorAdjustments.contrast (range: -100 to +100).
///
/// If no Volume with ColorAdjustments is found, it creates one at runtime.
/// </summary>
public class OptionsController : MonoBehaviour
{
    [Header("Sliders (wired by editor setup)")]
    public Slider brightnessSlider;
    public Slider contrastSlider;

    private ColorAdjustments _colorAdjustments;

    private void Start()
    {
        FindOrCreateColorAdjustments();

        // Set initial slider values based on current settings
        if (_colorAdjustments != null)
        {
            if (brightnessSlider != null)
            {
                brightnessSlider.minValue = -2f;
                brightnessSlider.maxValue = 2f;
                brightnessSlider.value = _colorAdjustments.postExposure.value;
                brightnessSlider.onValueChanged.AddListener(OnBrightnessChanged);
            }

            if (contrastSlider != null)
            {
                contrastSlider.minValue = -100f;
                contrastSlider.maxValue = 100f;
                contrastSlider.value = _colorAdjustments.contrast.value;
                contrastSlider.onValueChanged.AddListener(OnContrastChanged);
            }
        }
    }

    private void FindOrCreateColorAdjustments()
    {
        // Try to find an existing Volume with ColorAdjustments
        var volumes = FindObjectsByType<Volume>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var vol in volumes)
        {
            if (vol.profile != null && vol.profile.TryGet(out ColorAdjustments ca))
            {
                _colorAdjustments = ca;
                Debug.Log("[OptionsController] Found existing ColorAdjustments on Volume.");
                return;
            }
        }

        // If none found, create a global volume with ColorAdjustments
        var volumeGO = new GameObject("OptionsVolume");
        var volume = volumeGO.AddComponent<Volume>();
        volume.isGlobal = true;
        volume.priority = 10;
        volume.profile = ScriptableObject.CreateInstance<VolumeProfile>();

        _colorAdjustments = volume.profile.Add<ColorAdjustments>(true);
        _colorAdjustments.postExposure.overrideState = true;
        _colorAdjustments.contrast.overrideState = true;

        Debug.Log("[OptionsController] Created new global Volume with ColorAdjustments.");
    }

    private void OnBrightnessChanged(float value)
    {
        if (_colorAdjustments == null) return;
        _colorAdjustments.postExposure.overrideState = true;
        _colorAdjustments.postExposure.value = value;
    }

    private void OnContrastChanged(float value)
    {
        if (_colorAdjustments == null) return;
        _colorAdjustments.contrast.overrideState = true;
        _colorAdjustments.contrast.value = value;
    }
}
