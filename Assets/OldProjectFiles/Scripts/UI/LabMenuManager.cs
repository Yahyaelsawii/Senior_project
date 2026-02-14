using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

/// <summary>
/// Lab-only start menu controller.
/// Attach this (for example) to the Left Controller in the Lab scene and
/// assign the world-space Canvas or panel (e.g. Start_menu) to 'menuRoot'.
/// Pressing the left controller's menu button (three lines) will toggle the
/// menu on and off. Buttons can call the public methods below.
/// </summary>
public class LabMenuManager : MonoBehaviour
{
    [Tooltip("Root GameObject of the start menu UI (e.g. world-space Canvas under Left Controller).")]
    public GameObject menuRoot;

    [Tooltip("Transform of the player's head/camera (e.g. XR Origin/Main Camera).")]
    public Transform headTransform;

    [Tooltip("Distance in front of the head where the menu appears.")]
    public float menuDistance = 1.5f;

    [Tooltip("Vertical offset from head height for the menu (0 = same height).")]
    public float verticalOffset = -0.2f;

    [Tooltip("Optional dimmer object (e.g. a large semi-transparent panel) enabled when the menu is open.")]
    public GameObject dimmer;

    // Tracks previous frame menu button state so we can detect rising edge.
    private bool _menuButtonPrev;

    private void Start()
    {
        // Auto-assign if not set: find Start_menu and Main Camera.
        if (menuRoot == null)
        {
            var go = GameObject.Find("Start_menu");
            if (go != null) menuRoot = go;
        }
        if (headTransform == null && Camera.main != null)
        {
            headTransform = Camera.main.transform;
        }

        // Start with menu hidden inside the Lab environment.
        if (menuRoot != null)
        {
            menuRoot.SetActive(false);
        }

        if (dimmer != null)
        {
            dimmer.SetActive(false);
        }
    }

    private void Update()
    {
        bool menuButtonNow = GetLeftMenuButton();

        // On press (down edge): toggle menu visibility.
        if (menuButtonNow && !_menuButtonPrev)
        {
            if (menuRoot != null)
            {
                bool show = !menuRoot.activeSelf;
                menuRoot.SetActive(show);

                if (dimmer != null)
                    dimmer.SetActive(show);

                if (show)
                    UpdateMenuPose();
            }
        }

        // While menu is open, keep it in front of the player's view.
        if (menuRoot != null && menuRoot.activeSelf)
        {
            UpdateMenuPose();
        }

        _menuButtonPrev = menuButtonNow;
    }

    /// <summary>
    /// Called by the Resume button in the Lab start menu.
    /// Simply hides the menu and keeps you in the Lab.
    /// </summary>
    public void OnResumeButtonClick()
    {
        if (menuRoot != null)
        {
            menuRoot.SetActive(false);
        }

        if (dimmer != null)
        {
            dimmer.SetActive(false);
        }
    }

    /// <summary>
    /// Called by the Main Menu button in the Lab start menu.
    /// Loads the main menu scene (Main_screen).
    /// </summary>
    public void OnMainMenuButtonClick()
    {
        // Make sure Main_screen is in Build Settings.
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_screen");
    }

    /// <summary>
    /// Positions the menu in front of the player's head so it "follows the face".
    /// </summary>
    private void UpdateMenuPose()
    {
        if (menuRoot == null || headTransform == null)
            return;

        // Forward direction projected onto horizontal plane so the menu is upright.
        Vector3 forward = headTransform.forward;
        forward.y = 0f;
        if (forward.sqrMagnitude < 0.0001f)
            forward = headTransform.forward;
        forward.Normalize();

        Vector3 targetPos = headTransform.position + forward * menuDistance;
        targetPos.y = headTransform.position.y + verticalOffset;

        menuRoot.transform.position = targetPos;
        menuRoot.transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
    }

    /// <summary>
    /// Reads the "menu" button on the left-hand XR controller.
    /// On Quest, this is the button with three horizontal lines.
    /// </summary>
    private bool GetLeftMenuButton()
    {
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, devices);

        foreach (var device in devices)
        {
            bool pressed;
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out pressed) && pressed)
            {
                return true;
            }
        }

        return false;
    }
}

