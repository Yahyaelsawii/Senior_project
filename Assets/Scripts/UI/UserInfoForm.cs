using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Simple name + age form shown on startup.
/// Hook the Submit button to OnSubmit().
/// </summary>
public class UserInfoForm : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField nameField;
    public TMP_InputField ageField;
    public GameObject formRoot; // panel / canvas to hide after submit
    public TMP_Text errorLabel; // red error text inside the panel

    [Header("Lock Other UI Until Submitted")]
    public Button[] buttonsToEnableOnSubmit; // background buttons (Play, Options, etc.)

    [Header("Lock Movement Until Submitted")]
    [Tooltip("Movement / turn provider scripts to enable only after Submit (e.g. DynamicMoveProvider, ContinuousTurnProvider, KeyboardVRSimulator).")]
    public Behaviour[] movementBehaviours;

    [Header("Behaviour")]
    [Tooltip("Optional: scene name to load after submit. Leave empty to just hide the form.")]
    public string nextSceneName;

    void Start()
    {
        // Disable movement at start until the form is submitted
        if (movementBehaviours != null)
        {
            foreach (var b in movementBehaviours)
            {
                if (b != null)
                    b.enabled = false;
            }
        }
    }

    public void OnSubmit()
    {
        var name = nameField != null ? nameField.text : string.Empty;
        var ageText = ageField != null ? ageField.text : string.Empty;

        // Basic validation
        bool hasName = !string.IsNullOrWhiteSpace(name);
        bool hasValidAge = int.TryParse(ageText, out int age) && age > 0;

        if (!hasName || !hasValidAge)
        {
            if (errorLabel != null)
            {
                errorLabel.text = "Please add your name and age";
                errorLabel.color = Color.red;
                errorLabel.gameObject.SetActive(true);
            }
            return; // Do not continue if invalid
        }

        // Clear error when valid
        if (errorLabel != null)
        {
            errorLabel.text = string.Empty;
            errorLabel.gameObject.SetActive(false);
        }

        // Store for later use (simple example using PlayerPrefs)
        PlayerPrefs.SetString("UserName", name);
        PlayerPrefs.SetInt("UserAge", age);

        PlayerPrefs.Save();

        // Hide the form UI
        if (formRoot != null)
            formRoot.SetActive(false);

        // Enable movement now that form is complete
        if (movementBehaviours != null)
        {
            foreach (var b in movementBehaviours)
            {
                if (b != null)
                    b.enabled = true;
            }
        }

        // Enable background buttons now that form is complete
        if (buttonsToEnableOnSubmit != null)
        {
            foreach (var btn in buttonsToEnableOnSubmit)
            {
                if (btn != null)
                    btn.interactable = true;
            }
        }

        // Optionally trigger a scene change via ScreenFader if configured
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            var fader = FindObjectOfType<ScreenFader>();
            if (fader != null)
            {
                fader.FadeToScene(nextSceneName);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}

