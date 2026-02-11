using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple screen fade in/out when changing scenes.
/// Attach to a Canvas that has a full‑screen Image with a CanvasGroup.
/// </summary>
public class ScreenFader : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup canvasGroup;   // CanvasGroup on the full‑screen black image

    [Header("Settings")]
    public float fadeDuration = 0.5f; // seconds

    void Awake()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    void Start()
    {
        // Start the scene faded in (from black to clear)
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            StartCoroutine(Fade(1f, 0f));
        }
    }

    /// <summary>
    /// Call this from a button to fade out and then load another scene.
    /// </summary>
    public void FadeToScene(string sceneName)
    {
        if (gameObject.activeInHierarchy && canvasGroup != null)
            StartCoroutine(FadeOutAndLoad(sceneName));
        else
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        canvasGroup.alpha = from;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return Fade(0f, 1f);              // fade to black
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);      // switch scene
    }
}

