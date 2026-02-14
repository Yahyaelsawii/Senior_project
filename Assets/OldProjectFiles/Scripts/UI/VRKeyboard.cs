using UnityEngine;
using TMPro;

/// <summary>
/// Very simple on-screen VR keyboard for TMP_InputField.
/// Place this on a world-space Canvas with key buttons.
/// Each key button calls PressKey/Backspace/Clear/Submit via OnClick.
/// </summary>
public class VRKeyboard : MonoBehaviour
{
    [Header("Target")]
    public TMP_InputField targetField;

    [Header("Optional")]
    [Tooltip("Form to submit when Enter/Submit is pressed (e.g. UserInfoForm).")]
    public UserInfoForm userInfoForm;

    public void SetTarget(TMP_InputField field)
    {
        targetField = field;
    }

    public void PressKey(string character)
    {
        if (targetField == null || string.IsNullOrEmpty(character))
            return;

        // Append at end for simplicity
        targetField.text += character;
        targetField.caretPosition = targetField.text.Length;
        targetField.ForceLabelUpdate();
    }

    public void Space()
    {
        PressKey(" ");
    }

    public void Backspace()
    {
        if (targetField == null || string.IsNullOrEmpty(targetField.text))
            return;

        int len = targetField.text.Length;
        targetField.text = targetField.text.Substring(0, len - 1);
        targetField.caretPosition = targetField.text.Length;
        targetField.ForceLabelUpdate();
    }

    public void Clear()
    {
        if (targetField == null)
            return;

        targetField.text = string.Empty;
        targetField.caretPosition = 0;
        targetField.ForceLabelUpdate();
    }

    public void Submit()
    {
        if (userInfoForm != null)
        {
            userInfoForm.OnSubmit();
        }
    }
}

