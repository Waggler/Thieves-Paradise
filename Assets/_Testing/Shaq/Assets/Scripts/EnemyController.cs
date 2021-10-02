using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//Current Bugs:
//    - 

//Things to add:
//    - Create a feature that makes the AI calculate a path to it's target before it goes after it, can go from there and create logic on what to do if the target is unreachable
//      - https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.CalculatePath.html
//    - Begin work/ research on the state machine
//    - Make general function for setting AI target
//    - 

//Done:
//    - Created Group of empty objects w/ parent for testing patrol functionality
//    - Setting target to waypoints and having the AI cycle through them
//    - Headers for different sections of code in order to better organize content displayed in the investigator
//    - Separate Waypoint navigation method that reverses the order of the waypoints (Back and forth instead of in circles)
//    - 

public class EnemyController : MonoBehaviour
{
    #region Enumerations

    #region AI State Machine

    private enum EnemyStates
        {
            PASSIVE,
            SUSPICIOUS,
            HOSTILE,
            RANGEDATTACK
        }

    [Header("AI State")]

    [SerializeField] EnemyStates stateMachine;

    #endregion.

    #region AI Cycle Methods

    [System.Serializable]
    private enum CycleMethods
        {
            Cycle,
            Reverse
        }

    [Header("Waypoint Cycling Method")]

    [SerializeField] CycleMethods waypointMethod;

    #endregion

    #endregion

    #region Waypoint Cycle Methods List
    [SerializeField]private static readonly List<CycleMethods> Methods = new List<CycleMethods>
    {
        CycleMethods.Cycle,
        CycleMethods.Reverse
    };
    #endregion

    #region Variables

    //Important Variables
    private bool alert;
    private Transform target;
    private NavMeshAgent agent;

    [Header("Debug Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private Text stateText;
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float faceRadius = 10f;
    [SerializeField] private float pursuitSpeed = 25f;
    [SerializeField] private float patrolSpeed = 10f;
    [SerializeField] private float waypointNextDistance = 2f;

    [Header("Temporary Variables")]

    //Temporary Variables
    [SerializeField] private float speedRead;

    #endregion

    #region Start & Update
    //  Using Awake() instead of Start() so that when spawning is functional, the AI won't break
    void Awake()
    {
        print("I live!");
        alert = false;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        stateText.text = "IDLE";

        //checks to see if there are any objects in the waypoints list
        //if there are, then it will start the game by setting the target to the first waypoint on the list
        if (waypoints.Count > 0)
        {
            target = waypoints[waypointIndex];
        }
    }//End Awake


    //---------------------------------//
    void Update()
    {
        //Start of navigating enemy state machine
        //switch (EnemyStates)
        //  {
        //      case EnemyStates.PASSIVE:
        //          print("");
        //          break;

        //      case EnemyStates.SUSPICIOUS:
        //          print("");

        //          break;

        //      case EnemyStates.HOSTILE:
        //          print("");
        //          break;

        //      default:
        //          print("");
        //          break;
        //  }

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (Vector3.Distance(target.transform.position, transform.position) <= waypointNextDistance)
            {
                SetNextWaypoint();
            }

        if (distanceToPlayer <= lookRadius)
            {
                print("Pursuit");

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(player.transform.position);

                SetAiSpeed("Pursuit");

                target = player.transform;

                stateText.text = $"{target}";

                speedRead = pursuitSpeed;

                if (distanceToPlayer <= faceRadius)
                    {
                        target = player.transform;

                        FaceTarget();
                    }
            }
        else
            {
                print("Patrol");

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(waypoints[waypointIndex].transform.position);

                SetAiSpeed("Patrol");

                target = waypoints[waypointIndex];

                stateText.text = $"{target}";

                speedRead = patrolSpeed;
            }
    }//End Update
    #endregion

    #region General Functions
    //---------------------------------//
    //Used to draw a sphere tied to the AI's current look radius. Really just serving as a diagnostic tool
    void OnDrawGizmosSelected()
        {
            //Drawing a magenta sphere at the AI's current position
            Gizmos.color = Color.magenta;
            //Gizmos.DrawFrustum(center , fov , maxRange, minRange, aspect);
            //Gizmos.DrawFrustum(transform.position, fov, maxRange, minRange, aspect);
            Gizmos.DrawWireSphere(transform.position, lookRadius);

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, faceRadius);

        }//End DrawGizmos


    //---------------------------------//
    //function for facing the player when the AI is withing stopping distance of the player
    void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime /** 2f*/);
        }//End FaceTarget

    ////--------------Attempted Function for setting target variable-------------------//
    //void SetTarget(Transform targetObj)
    //    {
    //        targetObj = target;

    //        return targetObj;
    //    }//End SetTarget
    // Consider deleting this function as a whole; setting the target variable can already be done with a brief line of  code
    //---------------------------------//

    void DummyFunction()
    {

    }

    //---------------------------------//
    // currently fleshing out this bit, might have another funtion that acts as a sort of gate for all these other funtions along the lines of "Mode controller" (or just have
    // that be handled by the initial switch case statement)
    void SetAiSpeed(string detection)
        {
            switch (detection)
            {
            //Setting pursuit speed
                case "Pursuit":
                    agent.speed = Mathf.Lerp(patrolSpeed, pursuitSpeed, 1);
                    break;
            //Setting patrol speed
                case "Patrol":
                    agent.speed = Mathf.Lerp(pursuitSpeed, patrolSpeed, 1);
                    break;
                default:
                    print("Speed unspecified");
                    break;
            }

        }//End SetSpeed

    //---------------------------------//
    //Function for setting AI destination
    void SetAIDestination(Vector3 point)
        {
            agent.SetDestination(point);
        }//End SetAIDestination

    //-----------Failed attempt at a distance calculator------------//
    //Used to calculate distance between two objects (using x, y, & z positions)
    //Might actually delete this one, seems dumb in the long run
    //void CalculateDistance(Vector3 obj1, Vector3 obj2)
    //    {
    //        float distance; 

    //        distance = Vector3.Distance(obj1, obj2);

    //        return distance;
    //    }   
    #endregion

    #region Waypoints Logic
    [Header("Waypoint Variables")]
    [SerializeField] private List<Transform> waypoints;
    //waypoints.Count will be used to get the number of points in the list (similar to array.Length)
    private int waypointIndex = 0;

    #region Waypoints Functions
    void SetNextWaypoint()
        {
        switch (waypointMethod)
        {
            case CycleMethods.Cycle:
                //Insert original code for navigating waypoints here
                if (waypointIndex >= waypoints.Count - 1)
                    {
                        waypointIndex = 0;

                        target = waypoints[0];
                    }
                else
                    {
                        waypointIndex++;

                        target = waypoints[waypointIndex];
                    }
                SetAIDestination(target.position);

                break;
            case CycleMethods.Reverse:
                //Insert reverse based method for navigating waypoints here
                if (waypointIndex >= waypoints.Count - 1)
                    {
                        waypointIndex = 0;

                        waypoints.Reverse();

                        target = waypoints[0];
                    }
                else
                    {
                        waypointIndex++;

                        target = waypoints[waypointIndex];
                    }
                SetAIDestination(target.position);

                break;
            default:
                print("Cycling method not found \a");
                break;
        }
    }
    #endregion

    #endregion

    #region AI Attacks
    //Generic Funcitons to be add WAY down the line
    private void RangedAttack()
        {
        }

    private void CQCAttack()
        {
        }

    #endregion
}