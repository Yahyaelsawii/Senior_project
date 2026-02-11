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

        int age = 0;
        int.TryParse(ageText, out age);

        // Store for later use (simple example using PlayerPrefs)
        if (!string.IsNullOrEmpty(name))
            PlayerPrefs.SetString("UserName", name);

        if (age > 0)
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

