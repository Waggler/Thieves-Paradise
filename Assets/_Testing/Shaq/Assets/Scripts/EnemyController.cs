using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Current Bugs:
// - 


//Things to add:
//    - agent.setDestination usage based on certain conditions for the AI
//    - Review tutorials for setting up AI in unity as well as for setting up a third person controller (Overall refresher course needed)
//    - Switch case statement to act as a state machine for the AI (can be done later on)
//    - Enumeration for state machine (please save this for after you actually learn the ins and out of a state machine)
//    - Seperate redundant actions into seperate & callable functions with their necessary variables and all that
//    - 


//Done:
//    - Add Face Target function
//    - 

public class EnemyController : MonoBehaviour
{
    [Header("Manipulatable Variables for AI prefab")]

    #region Variables

    private bool alert;
    private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private float lookRadius;
    //private float fov;
    //private float maxRange;
    //private float minRange;
    //private Vector3 center;
    //private float aspect;
    [SerializeField] private readonly float pursuitSpeed;
    [SerializeField] private readonly float patrolSpeed;


    private Transform target;

    #endregion

    #region Start & Update
    // Start is called before the first frame update
    void Start()
    {
        print("I live!");
        alert = false;
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;

        ////Initial values for drawn Frustrum
        //fov = 90f;
        //maxRange = 16f;
        //maxRange = 5f;
        //aspect = 5;
    }


    //---------------------------------//
    void Update()
    {
        //public float pursuitSpeed;

        target = player.transform;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= lookRadius)
        {
            alert = true;

            print("I can see you");

            agent.SetDestination(player.transform.position);

            agent.speed = pursuitSpeed;
        }
        else
        {
            alert = false;

            print("Where are you");

            agent.SetDestination(transform.position);

            agent.speed = patrolSpeed;
        }

        switch (alert)
        {
            case true:
                print("I have been given purpose");
                break;

            case false:
                print("Give me a task to do pls");
                break;

                //default:
                //    print("This condition should literally be unreachable");
                //    break;
        }

        FaceTarget();

        //agent.SetDestination(player.transform.position);

    }//End Update
    #endregion

    #region
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
    // currently fleshing out this bit, might have another funtion that acts as a sort of gate for all these other funtions along the lines of "Mode controller" (or just have
    // that be handled by the initial switch case statement)
    void SetSpeed(bool detection, int mode)
    {
        if (detection == true)
        {

        switch (mode)
        {
            case 1:
                break;
            case 2:
                break;

            default:
                    break;
        }
    }

    }//End SetSpeed

    #endregion
}
