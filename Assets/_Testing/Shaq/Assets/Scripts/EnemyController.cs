using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Current Bugs:
// - distanceToPlayer currently not working
// - Face Target currently yeilds negligible results
// - 
// - 
// - 
// - 
// - 


//Things to add:
//    - agent.setDestination usage based on certain conditions for the AI
//    - Switch case statement to act as a state machine for the AI (can be done later on)
//    - Review tutorials for setting up AI in unity as well as for setting up a third person controller (Overall refresher course needed)
//    - Add Face Target function
//    - 


public class EnemyController : MonoBehaviour
{
    public bool taskGive = true;
    private NavMeshAgent agent;
    public GameObject player;
    public float lookRadius;
    public float fov;
    public float maxRange;
    public float minRange;
    public Vector3 center;
    public float aspect;

    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        print("I live!");
        taskGive = false;
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;

        //Initial values for drawn Frustrum
        fov = 90f;
        maxRange = 16f;
        maxRange = 5f;
        aspect = 5;
    }

    // Update is called whenever
    void Update()
    {
        target = player.transform;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= lookRadius)
        {
            print("I can see you");

            agent.SetDestination(player.transform.position);
        }
        else
        {
            print("Where are you");

            agent.SetDestination(transform.position);
        }

        FaceTarget();

        //agent.SetDestination(player.transform.position);

        if (taskGive == false)
        {
            print("Give me a task to do pls");
        }

    }

    //function for facing the player when the AI is withing stopping distance of the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        //Drawing a magenta sphere at the AI's current position
        Gizmos.color = Color.magenta;
        //Gizmos.DrawFrustum(center , fov , maxRange, minRange, aspect);
        Gizmos.DrawFrustum(transform.position, fov, maxRange, minRange, aspect);

    }
}
