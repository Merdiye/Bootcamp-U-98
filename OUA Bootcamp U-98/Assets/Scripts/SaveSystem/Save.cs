using UnityEngine;
using UnityEngine.UI;

public class Save : MonoBehaviour
{
    public Button saveButton;
    public SaveSystem saveSystem;

    private void Start()
    {
        if (saveButton != null && saveSystem != null)
        {
            saveButton.onClick.AddListener(OnSaveButtonClicked);
            Debug.Log("Save button set up successfully.");
        }
        else
        {
            Debug.LogError("Save button or SaveSystem reference is missing.");
        }
    }

    private void OnSaveButtonClicked()
    {
        Debug.Log("Save button clicked.");
        saveSystem.SavePlayer();
    }
}