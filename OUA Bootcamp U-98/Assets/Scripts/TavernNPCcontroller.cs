using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TavernNPCcontroller : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public GameObject PATH;
    private Transform[] pathPoints;

    public int index = 0;

    public float minDistance;

    public bool isSitting = false; //****//

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        pathPoints = new Transform[PATH.transform.childCount];
        for(int i = 0; i < pathPoints.Length; i++)
        {
            pathPoints[i] = PATH.transform.GetChild(i);
        }
    }

    private void Update()
    {
        if(!isSitting)//***//
        {
            roam();
        }
        
    }

    void roam()
    {
        if (Vector3.Distance(transform.position, pathPoints[index].position) < minDistance) 
        {
            if (index >= 0 && index < pathPoints.Length - 1)//
            {
                index += 1;
            }
            else
            {
                
                isSitting = true;//***//
                animator.SetBool("isSitting", true);
                //index = 0;
            }
        }


        agent.SetDestination(pathPoints[index].position);
        animator.SetFloat("vertical", !agent.isStopped ? 1 : 0);
    }

 
}
