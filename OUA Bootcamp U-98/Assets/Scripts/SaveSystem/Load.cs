using UnityEngine;
using UnityEngine.UI;

public class Load : MonoBehaviour
{
    public Button loadButton;
    public SaveSystem saveSystem;

    private void Start()
    {
        if (loadButton != null && saveSystem != null)
        {
            loadButton.onClick.AddListener(OnLoadButtonClicked);
            Debug.Log("Load button set up successfully.");
        }
        else
        {
            Debug.LogError("Load button or SaveSystem reference is missing.");
        }
    }

    private void OnLoadButtonClicked()
    {
        Debug.Log("Load button clicked.");
        saveSystem.LoadPlayer();
    }
}
