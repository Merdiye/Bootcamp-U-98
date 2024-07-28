using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernOwnerController : MonoBehaviour
{
    public bool StarterButtonBool;
    public float StarterButtonRange;
    public Animator animator;
    DialogueManager dialogueManager;
    public Transform _player;
    public LayerMask player;
    public GameObject playerGameObj;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        dialogueManager = GetComponent<DialogueManager>();


    }
    private void Update()
    {
        StarterButtonBool = Physics.CheckSphere(transform.position, StarterButtonRange, player);

        if(StarterButtonBool)
        {
            dialogueManager.animator.SetBool("IsOpen", true);
            
        }
        
    }


}
