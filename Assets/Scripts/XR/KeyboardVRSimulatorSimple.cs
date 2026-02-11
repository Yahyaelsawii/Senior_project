using UnityEngine;

public class KeyboardVRSimulatorSimple : MonoBehaviour
{
    public Transform xrOrigin;
    public Camera mainCamera;
    public float moveSpeed = 2f;

    void Start()
    {
        if (xrOrigin == null)
        {
            GameObject xrOriginObj = GameObject.Find("XR Origin (XR Rig)");
            if (xrOriginObj != null)
                xrOrigin = xrOriginObj.transform;
        }
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (xrOrigin == null) return;

        // Simple WASD movement
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) move -= Vector3.forward;
        if (Input.GetKey(KeyCode.A)) move -= Vector3.right;
        if (Input.GetKey(KeyCode.D)) move += Vector3.right;

        if (move.magnitude > 0)
        {
            move = mainCamera != null ? mainCamera.transform.TransformDirection(move) : move;
            move.y = 0;
            move.Normalize();
            xrOrigin.position += move * moveSpeed * Time.deltaTime;
        }

        // F key = teleport to 0,0,0
        if (Input.GetKeyDown(KeyCode.F))
        {
            xrOrigin.position = Vector3.zero;
            Debug.Log("Teleported to (0,0,0)");
        }
    }
}
