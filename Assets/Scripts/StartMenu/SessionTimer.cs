using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Displays the user name and session elapsed time in a small HUD panel.
/// Format: "UserName | MM:SS"
///
/// StartTimer() is called by MenuManager after Play is pressed.
/// The timer text UI reference is wired by the editor setup script.
/// </summary>
public class SessionTimer : MonoBehaviour
{
    [Header("UI Reference")]
    public Text timerText;
    public GameObject hudPanel;

    private float _startTime;
    private bool _running;

    private void Start()
    {
        // Hide HUD until Play
        if (hudPanel != null)
            hudPanel.SetActive(false);
    }

    /// <summary>Called by MenuManager when Play is pressed.</summary>
    public void StartTimer()
    {
        _startTime = Time.time;
        _running = true;

        if (hudPanel != null)
            hudPanel.SetActive(true);

        Debug.Log("[SessionTimer] Timer started.");
    }

    public void StopTimer()
    {
        _running = false;
    }

    private void Update()
    {
        if (!_running || timerText == null) return;

        float elapsed = Time.time - _startTime;
        int minutes = Mathf.FloorToInt(elapsed / 60f);
        int seconds = Mathf.FloorToInt(elapsed % 60f);

        string name = string.IsNullOrEmpty(SessionData.UserName) ? "User" : SessionData.UserName;
        timerText.text = $"{name}  |  {minutes:00}:{seconds:00}";
    }
}
