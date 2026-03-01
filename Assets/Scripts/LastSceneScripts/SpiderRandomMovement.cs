using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderRandomMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Vector3 destPoint;
    bool walkPointSet;
    float range = 2.7f  ;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.5f; 
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (!walkPointSet) 
        {
            SearchForDest();
        }

        if (walkPointSet)
        {
            agent.SetDestination(destPoint);
            if (Vector3.Distance(transform.position, destPoint) < agent.stoppingDistance)
            {
                walkPointSet = false;
            }
        }
    }

    void SearchForDest()
    {
        float z = Random.Range(-range, range);
        float x = Random.Range(-range, range);

        Vector3 randomPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
        {
            destPoint = hit.position;
            walkPointSet = true;
        }
    }
}
