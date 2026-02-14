using UnityEngine;
using Unity.XR.CoreUtils;

/// <summary>
/// Ensures the XR Origin uses Floor tracking so the world stays fixed when you move your head.
/// Add to the same GameObject as XROrigin (XR Origin root). Re-applies Floor mode after a short
/// delay so Quest/OpenXR has time to initialize.
/// </summary>
[RequireComponent(typeof(XROrigin))]
public class ForceFloorTrackingOrigin : MonoBehaviour
{
    [Tooltip("Delay in seconds before forcing Floor mode (Quest often needs this).")]
    public float delaySeconds = 0.5f;

    float _timer;
    XROrigin _origin;

    void Awake()
    {
        _origin = GetComponent<XROrigin>();
    }

    void Start()
    {
        ApplyFloorMode();
    }

    void Update()
    {
        if (_timer < delaySeconds)
        {
            _timer += Time.deltaTime;
            if (_timer >= delaySeconds)
                ApplyFloorMode();
        }
    }

    void ApplyFloorMode()
    {
        if (_origin == null) return;
        _origin.RequestedTrackingOriginMode = XROrigin.TrackingOriginMode.Floor;
    }
}
