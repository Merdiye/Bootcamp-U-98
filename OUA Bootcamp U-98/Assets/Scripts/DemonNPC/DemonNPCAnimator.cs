using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemonNPCAnimator : MonoBehaviour
{

    public Animator animator;
    [SerializeField] GameObject warrior;
    public bool isPunchingBool;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isPunching();
        isPunchingBool = animator.GetBool("isPunching");
    }

    public void isPunching()
    {
        Vector3 distanceToWarrior = transform.position - warrior.transform.position;
        if (distanceToWarrior.magnitude <= 4.0f)
        {
            animator.SetBool("isPunching", true);
        }
        else
        {
            animator.SetBool("isPunching", false);
        }
    }


}
