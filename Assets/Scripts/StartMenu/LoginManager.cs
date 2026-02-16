using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the login panel UI. Reads Name and Age input fields,
/// stores them in SessionData, logs the info, and transitions to the main menu.
///
/// References are wired by the StartMenuSetup editor script.
/// </summary>
public class LoginManager : MonoBehaviour
{
    [Header("UI References")]
    public InputField nameInputField;
    public InputField ageInputField;
    public Button submitButton;

    [Header("Manager References")]
    public MenuManager menuManager;

    private void Start()
    {
        if (submitButton != null)
            submitButton.onClick.AddListener(OnSubmit);
    }

    private void OnSubmit()
    {
        string userName = nameInputField != null ? nameInputField.text.Trim() : "";
        string userAge = ageInputField != null ? ageInputField.text.Trim() : "";

        if (string.IsNullOrEmpty(userName))
        {
            Debug.LogWarning("[LoginManager] Name field is empty.");
            return;
        }

        if (string.IsNullOrEmpty(userAge))
        {
            Debug.LogWarning("[LoginManager] Age field is empty.");
            return;
        }

        // Store in static session data
        SessionData.UserName = userName;
        SessionData.UserAge = userAge;

        Debug.Log($"[LoginManager] User logged in: Name={userName}, Age={userAge}");

        // Transition to main menu
        if (menuManager != null)
            menuManager.ShowMainMenu();
    }
}
