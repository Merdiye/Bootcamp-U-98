using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernNPCanimator : MonoBehaviour
{
    private bool isStanding;
    private bool isSitting;
    private float sitTimer;
    private bool isWalking;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public void SetWalking(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }

    public void SetSitting(bool isSitting)
    {
        animator.SetBool("isSitting", isSitting);
    }


    public void SetStanding(bool isStanding)
    {
        animator.SetBool("isStanding", isStanding);
    }



}

