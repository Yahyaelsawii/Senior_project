using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Simulates VR controllers and movement using keyboard/mouse for testing in Editor
/// </summary>
public class KeyboardVRSimulator : MonoBehaviour
{
    [Header("References")]
    public Transform xrOrigin; // XR Origin (XR Rig)
    public Camera mainCamera;  // Main Camera
    
    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float turnSpeed = 90f; // degrees per second
    public float lookSensitivity = 2f;
    
    [Header("Keyboard Controls")]
    [Tooltip("WASD = Move, Mouse = Look, Q/E = Turn, Space/Shift = Up/Down")]
    public Key moveForward = Key.W;
    public Key moveBackward = Key.S;
    public Key moveLeft = Key.A;
    public Key moveRight = Key.D;
    public Key turnLeft = Key.Q;
    public Key turnRight = Key.E;
    public Key moveUp = Key.Space;
    public Key moveDown = Key.LeftShift;
    
    [Header("Controller Button Simulation")]
    [Tooltip("Press these keys to simulate controller buttons")]
    public Key simulateAButton = Key.F;   // Right A button
    public Key simulateBButton = Key.G;   // Right B button
    public Key simulateXButton = Key.T;   // Left X button
    public Key simulateYButton = Key.Y;   // Left Y button
    public Key simulateTrigger = Key.Z;   // Keyboard Z = trigger
    public Key simulateGrip = Key.X;      // Keyboard X = grip
    
    // Controller references removed - not needed for keyboard simulation
    
    private Vector2 moveInput;
    private Vector2 lookInput;
    private bool isMouseLookActive = false;
    
    void Start()
    {
        // Auto-find references if not assigned
        if (xrOrigin == null)
        {
            GameObject xrOriginObj = GameObject.Find("XR Origin (XR Rig)");
            if (xrOriginObj != null)
                xrOrigin = xrOriginObj.transform;
        }
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
                mainCamera = FindObjectOfType<Camera>();
        }
        
        // Lock cursor for mouse look
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (xrOrigin == null || mainCamera == null)
            return;
        
        // Movement input
        HandleMovement();
        
        // Mouse look
        HandleMouseLook();
        
        // Controller button simulation
        HandleButtonSimulation();
    }
    
    void HandleMovement()
    {
        moveInput = Vector2.zero;
        
        // WASD movement
        if (Keyboard.current[moveForward].isPressed) moveInput.y += 1f;
        if (Keyboard.current[moveBackward].isPressed) moveInput.y -= 1f;
        if (Keyboard.current[moveLeft].isPressed) moveInput.x -= 1f;
        if (Keyboard.current[moveRight].isPressed) moveInput.x += 1f;
        
        // Normalize diagonal movement
        if (moveInput.magnitude > 1f)
            moveInput.Normalize();
        
        // Apply movement relative to camera forward
        if (moveInput.magnitude > 0.1f)
        {
            Vector3 forward = mainCamera.transform.forward;
            Vector3 right = mainCamera.transform.right;
            forward.y = 0f; // Keep movement horizontal
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            
            Vector3 moveDirection = (forward * moveInput.y + right * moveInput.x) * moveSpeed * Time.deltaTime;
            xrOrigin.position += moveDirection;
        }
        
        // Vertical movement
        if (Keyboard.current[moveUp].isPressed)
            xrOrigin.position += Vector3.up * moveSpeed * Time.deltaTime;
        if (Keyboard.current[moveDown].isPressed)
            xrOrigin.position -= Vector3.up * moveSpeed * Time.deltaTime;
        
        // Turn left/right
        float turnAmount = 0f;
        if (Keyboard.current[turnLeft].isPressed) turnAmount -= 1f;
        if (Keyboard.current[turnRight].isPressed) turnAmount += 1f;
        
        if (Mathf.Abs(turnAmount) > 0.1f)
        {
            xrOrigin.Rotate(0f, turnAmount * turnSpeed * Time.deltaTime, 0f);
        }
    }
    
    void HandleMouseLook()
    {
        // Toggle mouse look with Escape
        if (Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            isMouseLookActive = !isMouseLookActive;
            Cursor.lockState = isMouseLookActive ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !isMouseLookActive;
        }
        
        if (isMouseLookActive && Mouse.current != null)
        {
            Vector2 delta = Mouse.current.delta.ReadValue();
            float yaw = delta.x * lookSensitivity * Time.deltaTime;
            float pitch = -delta.y * lookSensitivity * Time.deltaTime;
            
            // Rotate camera for pitch (up/down)
            mainCamera.transform.Rotate(pitch, 0f, 0f);
            
            // Rotate XR Origin for yaw (left/right)
            xrOrigin.Rotate(0f, yaw, 0f);
        }
    }
    
    void HandleButtonSimulation()
    {
        // Simulate A button (Right controller)
        if (Keyboard.current[simulateAButton].wasPressedThisFrame)
        {
            Debug.Log("[Keyboard Simulator] A Button Pressed (Right Controller)");
            // Trigger any A button actions
            SimulateButtonPress("A");
        }
        
        // Simulate B button (Right controller)
        if (Keyboard.current[simulateBButton].wasPressedThisFrame)
        {
            Debug.Log("[Keyboard Simulator] B Button Pressed (Right Controller)");
            SimulateButtonPress("B");
        }
        
        // Simulate X button (Left controller)
        if (Keyboard.current[simulateXButton].wasPressedThisFrame)
        {
            Debug.Log("[Keyboard Simulator] X Button Pressed (Left Controller)");
            SimulateButtonPress("X");
        }
        
        // Simulate Y button (Left controller)
        if (Keyboard.current[simulateYButton].wasPressedThisFrame)
        {
            Debug.Log("[Keyboard Simulator] Y Button Pressed (Left Controller)");
            SimulateButtonPress("Y");
        }
        
        // Simulate Trigger
        if (Keyboard.current[simulateTrigger].wasPressedThisFrame)
        {
            Debug.Log("[Keyboard Simulator] Trigger Pressed");
            SimulateButtonPress("Trigger");
        }
        
        // Simulate Grip
        if (Keyboard.current[simulateGrip].wasPressedThisFrame)
        {
            Debug.Log("[Keyboard Simulator] Grip Pressed");
            SimulateButtonPress("Grip");
        }
    }
    
    void SimulateButtonPress(string buttonName)
    {
        // Find TeleportOnAButton script and trigger it if A button
        if (buttonName == "A")
        {
            TeleportOnAButton teleportScript = FindObjectOfType<TeleportOnAButton>();
            if (teleportScript != null)
            {
                // Manually trigger the teleport
                if (teleportScript.xrOrigin != null)
                {
                    teleportScript.xrOrigin.position = teleportScript.targetPosition;
                    Debug.Log($"[Keyboard Simulator] Teleported to {teleportScript.targetPosition}");
                }
            }
        }
        
        // You can add more button handlers here for other scripts
    }
    
    void OnGUI()
    {
        // Show controls on screen
        GUIStyle style = new GUIStyle();
        style.fontSize = 14;
        style.normal.textColor = Color.white;
        
        string controls = "=== Keyboard VR Simulator ===\n" +
                         "WASD: Move\n" +
                         "Q/E: Turn Left/Right\n" +
                         "Space/Shift: Move Up/Down\n" +
                         "Mouse: Look Around (Press ESC to toggle)\n" +
                         "F: Simulate A Button\n" +
                         "G: Simulate B Button\n" +
                         "T: Simulate X Button\n" +
                         "Y: Simulate Y Button\n" +
                         "Left Click: Trigger\n" +
                         "Right Click: Grip";
        
        GUI.Label(new Rect(10, 10, 400, 300), controls, style);
    }
}
