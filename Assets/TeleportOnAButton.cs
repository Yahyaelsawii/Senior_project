using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportOnAButton : MonoBehaviour
{
    [Header("References")]
    public Transform xrOrigin;                 // XR Origin (XR Rig)
    public InputActionReference aButtonAction; // Right-hand A / Primary Button

    [Header("Teleport Target")]
    public Vector3 targetPosition = Vector3.zero; // Defaults to (0,0,0)

    void OnEnable()
    {
        if (aButtonAction != null)
        {
            aButtonAction.action.performed += OnAPressed;
            aButtonAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (aButtonAction != null)
        {
            aButtonAction.action.performed -= OnAPressed;
        }
    }

    private void OnAPressed(InputAction.CallbackContext ctx)
    {
        if (xrOrigin != null)
        {
            xrOrigin.position = targetPosition;
        }
    }
}

