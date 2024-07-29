using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        DialogueManager[] managers = FindObjectsOfType<DialogueManager>();
        if (managers.Length > 0)
        {
            managers[0].OpenDialogue(messages, actors);
        }
        else
        {
            Debug.LogError("No DialogueManager found in the scene.");
        }
    }
}

[System.Serializable]
public class Message
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}