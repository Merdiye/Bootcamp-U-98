using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public Character character;

    void Start()
    {
        saveButton.onClick.AddListener(character.SaveGame);
        loadButton.onClick.AddListener(character.LoadGame);
    }
}
