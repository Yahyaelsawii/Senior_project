using UnityEngine;

/// <summary>
/// Simple helper to play a sound when a UI button is clicked.
/// </summary>
public class UIButtonClickSound : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clickClip;

    void Awake()
    {
        // Auto-find AudioSource on this GameObject if not assigned
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Call this from a Button OnClick() to play the click sound.
    /// </summary>
    public void PlayClick()
    {
        if (audioSource != null && clickClip != null)
        {
            audioSource.PlayOneShot(clickClip);
        }
    }
}

