using UnityEngine;

public class Character : MonoBehaviour
{
    public int health = 100;

    public void OnApplicationQuit()
    {
        SaveSystem.SaveCharacter(this);
        Debug.Log("Game Saved on Quit!");
    }

    public void SaveGame()
    {
        SaveSystem.SaveCharacter(this);
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        CharacterData data = SaveSystem.LoadCharacter();
        if (data != null)
        {
            Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
            transform.position = position;
            health = data.health;
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No save file found!");
        }
    }

    public void Start()
    {
        LoadGame();
    }
}
