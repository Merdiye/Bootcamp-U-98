using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI dialogueText;
    private Queue<string> sentences;

    public UnityEvent setMission;

    private void Start()
    {
        sentences = new Queue<string>();
         
    }

    public void StartDialogue(dialogue dialogue)
    {
        

        nametext.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        setMission.Invoke();
        Debug.Log("end of conversation");
    }
        
}
