using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class babaController : MonoBehaviour
{
    public Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") == true)
        {
            
            animator.SetBool("isCheering", true);
        }
    }
}
