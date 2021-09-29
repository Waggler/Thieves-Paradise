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
    #region Variables

    private bool taskGive = true;
    private NavMeshAgent agent;
    [SerializeField]private GameObject player;
    [SerializeField]private float lookRadius; 
    //private float fov;
    //private float maxRange;
    //private float minRange;
    //private Vector3 center;
    //private float aspect;
    [SerializeField]private float pursuitSpeed;
    [SerializeField]private float patrolSpeed;


    private Transform target;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        print("I live!");
        taskGive = false;
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;

        ////Initial values for drawn Frustrum
        //fov = 90f;
        //maxRange = 16f;
        //maxRange = 5f;
        //aspect = 5;
    }


    // Update is called whenever
    //---------------------------------//
    void Update()
    {
        //public float pursuitSpeed;

        target = player.transform;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= lookRadius)
        {
            taskGive = true;

            print("I can see you");

            agent.SetDestination(player.transform.position);

            agent.speed = pursuitSpeed;
        }
        else
        {
            taskGive = false;

            print("Where are you");

            agent.SetDestination(transform.position);

            agent.speed = patrolSpeed;
        }

        switch (taskGive)
        {
            case true:
                print("I have been given purpose");
                break;

            case false:
                print("Give me a task to do pls");
                break;

            default:
                break;
        }

        FaceTarget();

        //agent.SetDestination(player.transform.position);

    }//End Update

    //---------------------------------//
    void OnDrawGizmosSelected()
    {
        //Drawing a magenta sphere at the AI's current position
        Gizmos.color = Color.magenta;
        //Gizmos.DrawFrustum(center , fov , maxRange, minRange, aspect);
        //Gizmos.DrawFrustum(transform.position, fov, maxRange, minRange, aspect);
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }//End DrawGizmos


    //function for facing the player when the AI is withing stopping distance of the player
    //---------------------------------//
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }//End FaceTarget


    //---------------------------------//
    void SetTarget()
    {

    }//End SetTarget

    //---------------------------------//
    void SetSpeed()
    { 

    }//End SetSpeed
}
