using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls all world-space UI elements: tool status, status message,
/// hover label, region details panel, hemisphere buttons, control buttons, opacity slider.
///
/// New fields for the lab tool flow are set by the editor setup script
/// and updated at runtime by LabToolManager.
/// </summary>
public class RegionUIController : MonoBehaviour
{
    // ========================= HOVER =========================
    [Header("Hover Label")]
    public Text hoverNameTextLegacy;
    [Tooltip("The parent panel of the hover text (the background)")]
    public GameObject hoverPanel;

    // ========================= DETAILS =========================
    [Header("Region Details Panel")]
    public GameObject detailsPanel;
    public Text regionTitleTextLegacy;
    public Text regionShortDescriptionTextLegacy;
    public Text regionDetailedDescriptionTextLegacy;

    // ========================= SLIDER =========================
    [Header("Opacity Slider")]
    public Slider opacitySlider;

    // ========================= MAIN PANEL =========================
    [Header("Main Button Panel")]
    public GameObject mainButtonPanel;

    // ========================= LAB TOOL UI =========================
    [Header("Lab Tool UI (set by editor setup)")]
    [Tooltip("Text showing tool equip status (Gloves/Knife/Tweezers)")]
    public Text toolStatusText;

    [Tooltip("Text showing current instruction / status message")]
    public Text statusMessageText;

    [Tooltip("Panel containing View Left / View Right / Show Whole buttons")]
    public GameObject hemispherePanel;

    [Tooltip("Panel containing Rotate, Zoom, Reset, Opacity controls")]
    public GameObject controlPanel;

    // ========================= HOVER METHODS =========================

    public void ShowHoverName(string regionName)
    {
        if (hoverNameTextLegacy != null)
            hoverNameTextLegacy.text = regionName;

        if (hoverPanel != null)
            hoverPanel.SetActive(true);
        else if (hoverNameTextLegacy != null)
            hoverNameTextLegacy.transform.parent.gameObject.SetActive(true);
    }

    public void ClearHoverName()
    {
        if (hoverPanel != null)
            hoverPanel.SetActive(false);
        else if (hoverNameTextLegacy != null)
            hoverNameTextLegacy.transform.parent.gameObject.SetActive(false);
    }

    // ========================= REGION DETAILS =========================

    public void ShowRegionDetails(RegionData data)
    {
        if (data == null) return;
        if (detailsPanel != null) detailsPanel.SetActive(true);
        if (mainButtonPanel != null) mainButtonPanel.SetActive(false);

        if (regionTitleTextLegacy != null) regionTitleTextLegacy.text = data.displayName;
        if (regionShortDescriptionTextLegacy != null)
            regionShortDescriptionTextLegacy.text = data.shortDescription;
        if (regionDetailedDescriptionTextLegacy != null)
            regionDetailedDescriptionTextLegacy.text = data.detailedDescription;
    }

    public void HideRegionDetails()
    {
        if (detailsPanel != null) detailsPanel.SetActive(false);
        if (mainButtonPanel != null) mainButtonPanel.SetActive(true);
    }

    // ========================= LAB TOOL STATUS =========================

    /// <summary>Update the tool status bar (called by LabToolManager).</summary>
    public void SetToolStatus(string status)
    {
        if (toolStatusText != null)
            toolStatusText.text = status;
    }

    /// <summary>Update the instruction / status message (called by LabToolManager).</summary>
    public void SetStatusMessage(string message)
    {
        if (statusMessageText != null)
            statusMessageText.text = message;
    }

    /// <summary>Show or hide the hemisphere view buttons panel.</summary>
    public void ShowHemisphereButtons(bool visible)
    {
        if (hemispherePanel != null)
            hemispherePanel.SetActive(visible);
    }

    /// <summary>Show or hide the control buttons panel (rotate, zoom, reset, opacity).</summary>
    public void ShowControlButtons(bool visible)
    {
        if (controlPanel != null)
            controlPanel.SetActive(visible);
    }
}
