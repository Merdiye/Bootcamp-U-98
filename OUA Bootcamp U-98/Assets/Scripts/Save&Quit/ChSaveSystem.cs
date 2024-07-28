using System.IO;
using UnityEngine;

public static class SaveSystem
{
    public static string filePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveCharacter(Character character)
    {
        CharacterData data = new CharacterData(character.transform.position, character.health);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

    public static CharacterData LoadCharacter()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CharacterData data = JsonUtility.FromJson<CharacterData>(json);
            return data;
        }
        else
        {
            return null;
        }
    }
}
