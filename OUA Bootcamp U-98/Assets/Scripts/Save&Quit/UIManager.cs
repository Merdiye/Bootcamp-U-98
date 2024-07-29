using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public Character character;

    void Start()
    {
        if (saveButton == null) Debug.LogError("Save button is not assigned!");
        if (loadButton == null) Debug.LogError("Load button is not assigned!");
        if (character == null) Debug.LogError("Character is not assigned!");
        saveButton.onClick.AddListener(character.SaveGame);
        loadButton.onClick.AddListener(character.LoadGame);
    }
}
