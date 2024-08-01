using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public UnityEvent missionEvent;
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
    public Animator animator;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentActors = actors;
        currentMessages = messages;
        activeMessage = 0;
        isActive = true;
        animator.SetBool("IsOpen", true);

        Debug.Log("Started Conversation Loaded messages:" + messages.Length);
        DisplayMessage();
        
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            missionEvent.Invoke();
            Debug.Log("Conversation Ended!");
            animator.SetBool("IsOpen", false);
        }
        
        isActive = false;

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space) && isActive == false)
        //{
        //    NextMessage();
        //}
    }

}
