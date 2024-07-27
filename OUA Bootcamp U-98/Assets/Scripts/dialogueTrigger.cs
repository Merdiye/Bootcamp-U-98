using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public dialogue dialogue;

    public void TiggerDialogue()
    {
        DialogueManager manager = FindObjectOfType<DialogueManager>();
        if (manager != null)
        {
            manager.StartDialogue(dialogue);
        }
        else
        {
            Debug.LogWarning("DialogueManager bulunamadı!");

        }

     

        }
}
