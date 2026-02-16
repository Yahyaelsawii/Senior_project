using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the main menu panel transitions:
///   Login -> Main Menu -> (Play, Tutorial, Options, Back to Login)
///
/// On Play: fades out the menu canvas, opens doors, then enables movement.
/// References are wired by StartMenuSetup editor script.
/// </summary>
public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    [Header("Canvas")]
    public CanvasGroup menuCanvasGroup;
    public GameObject startMenuCanvas;

    [Header("References")]
    public DoorController doorController;
    public MovementGate movementGate;
    public SessionTimer sessionTimer;

    [Header("Settings")]
    public float fadeDuration = 1.0f;

    private void Start()
    {
        // Start by showing the login panel
        ShowLoginPanel();
    }

    // ========================= PANEL TRANSITIONS =========================

    public void ShowLoginPanel()
    {
        SetPanel(loginPanel);
        // Ensure canvas is fully visible
        if (menuCanvasGroup != null)
        {
            menuCanvasGroup.alpha = 1f;
            menuCanvasGroup.interactable = true;
            menuCanvasGroup.blocksRaycasts = true;
        }
        if (startMenuCanvas != null)
            startMenuCanvas.SetActive(true);
    }

    public void ShowMainMenu()
    {
        SetPanel(mainMenuPanel);
    }

    public void ShowOptions()
    {
        SetPanel(optionsPanel);
    }

    public void BackToLoginFromOptions()
    {
        ShowMainMenu();
    }

    public void ReturnToLogin()
    {
        // Clear session data
        SessionData.UserName = "";
        SessionData.UserAge = "";

        ShowLoginPanel();
    }

    // ========================= PLAY =========================

    public void OnPlayPressed()
    {
        Debug.Log("[MenuManager] Play pressed. Starting lab sequence...");
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // 1. Fade out the menu canvas
        yield return StartCoroutine(FadeOutCanvas());

        // 2. Hide the canvas entirely
        if (startMenuCanvas != null)
            startMenuCanvas.SetActive(false);

        // 3. Open the doors
        if (doorController != null)
        {
            doorController.OpenDoors();
            // Wait for doors to finish opening
            yield return new WaitForSeconds(doorController.openDuration + 0.2f);
        }

        // 4. Enable movement
        if (movementGate != null)
            movementGate.EnableMovement();

        // 5. Start the session timer
        if (sessionTimer != null)
            sessionTimer.StartTimer();

        Debug.Log("[MenuManager] Lab is ready. Player can move.");
    }

    // ========================= TUTORIAL =========================

    public void OnTutorialPressed()
    {
        Debug.Log("[MenuManager] Tutorial pressed. Loading TutorialScene...");
        if (Application.CanStreamedLevelBeLoaded("TutorialScene"))
            SceneManager.LoadScene("TutorialScene");
        else
            Debug.LogWarning("[MenuManager] TutorialScene not found in Build Settings. Add it via File > Build Settings.");
    }

    // ========================= FADE =========================

    private IEnumerator FadeOutCanvas()
    {
        if (menuCanvasGroup == null) yield break;

        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;

        float startAlpha = menuCanvasGroup.alpha;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            menuCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            yield return null;
        }

        menuCanvasGroup.alpha = 0f;
    }

    // ========================= HELPERS =========================

    private void SetPanel(GameObject target)
    {
        if (loginPanel != null) loginPanel.SetActive(target == loginPanel);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(target == mainMenuPanel);
        if (optionsPanel != null) optionsPanel.SetActive(target == optionsPanel);
    }
}
