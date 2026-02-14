using UnityEngine;

/// <summary>
/// Keeps the XR Character Controller at a minimum height so the player never collapses to the floor
/// when headset tracking isn't ready yet. Add this to the same GameObject as the CharacterController (XR Origin).
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class ClampXRCharacterControllerHeight : MonoBehaviour
{
    [Tooltip("Minimum capsule height (meters). Use ~1.2–1.4 so you never go to zero.")]
    [Min(0.5f)]
    public float minHeight = 1.2f;

    [Tooltip("Minimum Y for capsule center (keeps feet near floor).")]
    public float minCenterY = 0.6f;

    CharacterController _cc;

    void Awake()
    {
        _cc = GetComponent<CharacterController>();
    }

    void LateUpdate()
    {
        if (_cc == null) return;

        if (_cc.height < minHeight)
        {
            float oldCenterY = _cc.center.y;
            _cc.height = minHeight;
            // Keep center so bottom of capsule stays near feet
            _cc.center = new Vector3(_cc.center.x, Mathf.Max(minCenterY, minHeight * 0.5f + _cc.skinWidth), _cc.center.z);
        }
    }
}
