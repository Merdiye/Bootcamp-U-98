using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchNPCAI : MonoBehaviour
{
    public NavMeshAgent witchAgent;
    [SerializeField] public GameObject playerGameObj;
    public PlayerHealth playerHealth;
    [SerializeField] public Transform _player;
    public LayerMask ground, player;
    public Vector3 destinationPoint;
    private bool destinationPointSet;
    public float walkPointRange;
    public float timeBetweenAttacks;
    public bool isCanAttack;
    public GameObject attackObj;
    public float sightRange;
    public float attackRange;
    public bool playerInAttackRange;
    public bool playerInSightRange;
    public float attackDamage = 1f;


    private void Awake()
    {
        playerHealth = playerGameObj.GetComponent<PlayerHealth>();
        isCanAttack = true;
        witchAgent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);
    }


    void SearchPlayerPoint() //
    {
      
    }
}

