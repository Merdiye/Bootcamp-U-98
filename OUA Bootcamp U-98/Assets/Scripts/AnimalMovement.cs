
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AnimalWander : MonoBehaviour
{
    public float wanderRadius = 10f; // Hayvanlarýn dolaþabileceði alanýn yarýçapý
    public float wanderTimer = 5f;   // Hayvanlarýn bir noktadan diðerine geçiþ süresi

    private Transform target;
    private UnityEngine.AI.NavMeshAgent agent;
    private float timer;

    void OnEnable()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
