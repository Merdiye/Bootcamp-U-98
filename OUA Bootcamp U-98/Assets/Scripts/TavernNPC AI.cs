using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TavernNPCAI : MonoBehaviour
{
    public NavMeshAgent _agent;
    private Chair currentChair;
    //public Transform _chair;
    //public GameObject chairGameObj;
    //public LayerMask ground, chair;
    public Vector3 destinationPoint;
    private bool destinationPointSet;
    public float walkPointRange;
    public float sightRange;
    public bool chairInSightRange;
    private bool canSit;
    TavernNPCanimator animator;
    
    


    private void Awake()
    {

        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<TavernNPCanimator>();
        canSit = false;

        
        //chairGameObj = GameObject.Find("chair");


    }
    private void Update()
    {
        Patroling();

    }

    void Patroling()
    {
        Vector3 randomPoint = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
        _agent.SetDestination(randomPoint);
        animator.SetWalking(true);
        destinationPointSet = true;
    }

    void FindChairandSit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sightRange);
        Chair foundChair = null;
        foreach (Collider collider in colliders)
        {
            Chair chair = collider.GetComponent<Chair>();

            if (chair != null && !chair.isOccupied)
            {

                foundChair = chair;
                break;

            }

        }
        if (foundChair != null && !foundChair.isOccupied )
        {
            _agent.SetDestination(foundChair.transform.position);
            currentChair = foundChair;
            currentChair.isOccupied = true;
            canSit = true;
            destinationPointSet = false;
            animator.SetWalking(false);
            animator.SetSitting(true);

        }
        else
        {
            if (!destinationPointSet)
            {
                Patroling();
            }
        }
        
    }




}
