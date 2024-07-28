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
    public Animator animator;
    public LayerMask player;
  


    public UnityEvent setMission;

    private void Start()
    {
        sentences = new Queue<string>();
         
    }
    




    public void StartDialogue(dialogue dialogue)
    {
       
        animator.SetBool("isOpen", true);

        animator.SetBool("IsOpen", false);


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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {

        animator.SetBool("isOpen", false);

        setMission.Invoke();
        Debug.Log("end of conversation");

    }
        
}
