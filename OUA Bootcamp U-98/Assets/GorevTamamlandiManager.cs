using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorevTamamlandiManager : MonoBehaviour
{
    public Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenMissionBox()
    {
        animator.SetBool("open", true);
    }
}