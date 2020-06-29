using UnityEngine;
using TMPro;
using System;

public class TextProcessor : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text displayField;

    private void Start()
    {
        if (displayField)
            displayField.text = string.Empty;
    }

    public void OnEndEdit()
    {
        if (!displayField || !inputField)
        {
            if (!displayField) throw new ArgumentNullException(nameof(displayField));
            if (!inputField) throw new ArgumentNullException(nameof(inputField));
            return;
        }

        if (!string.IsNullOrEmpty(inputField.text))
        {
            string result = CustomCodeExecutor.ExecuteCommand(inputField.text);
            if (!string.IsNullOrEmpty(result))
                DisplayText(result);
        }

        RefocusTInputField();
    }

    private void DisplayText(string result)
    {
        displayField.text += string.IsNullOrEmpty(displayField.text) ? result : TextProcessorUtils.NEW_LINE + result;
    }

    private void RefocusTInputField()
    {
        inputField.text = string.Empty;
        inputField.Select();
        inputField.ActivateInputField();
    }

    public void OnClearDisplay()
    {
        Clear();
        RefocusTInputField();
    }
    private void Clear()
    {
        displayField.text = string.Empty;
    }

    public void OnQuit()
    {
        Quit();
    }
    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}