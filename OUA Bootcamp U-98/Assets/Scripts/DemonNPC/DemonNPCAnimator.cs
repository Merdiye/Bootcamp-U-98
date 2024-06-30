using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemonNPC2 : MonoBehaviour
{

    public Animator animator;
    [SerializeField] GameObject warrior;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 distanceToWarrior = transform.position - warrior.transform.position;
        if(distanceToWarrior.magnitude <= 4.0f)
        {
            animator.SetBool("isPunching", true);
        }

        else
        {
            animator.SetBool("isPunching", false);
        }
    }


}
