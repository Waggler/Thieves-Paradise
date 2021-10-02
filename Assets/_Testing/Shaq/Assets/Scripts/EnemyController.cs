using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//Current Bugs:
//    - FaceTarget function does not work when the AI's target is set to the player object
//    - 

//Things to add:
//    - https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.CalculatePath.html
//    - Setting target to waypoints and having the AI cycle through them
//    - Headers for different sections of code in order to better organize content displayed in the investigator
//    - Seperate Waypoint navigation method that reverses the order of the waypoints (Back and forth instead of in circles)
//          -This is already implemented but needs to be more efficient 
//    - Ability for the AI to check to see if there is a valid path to the set target before it begins moving towrads said target
//    - 

//Done:
//    - Created Group of empty objects w/ parent for testing patrol functionality
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

    #endregion
    #region AI Cycle Methods

    [System.Serializable]
    private enum CycleMethods
        {
            Cycle,
            Reverse
        }

    [SerializeField] CycleMethods waypointMethod;

    #endregion
    #endregion

    #region Waypoint Cycle Methods
    [SerializeField]private static readonly List<CycleMethods> Methods = new List<CycleMethods>
    {
        CycleMethods.Cycle,
        CycleMethods.Reverse
    };
    #endregion

    #region Variables

    //Important Variables
    [Header("Debug Variables")]
    private bool alert;
    private Transform target;
    private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private Text stateText;
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float faceRadius = 10f;
    [SerializeField] private float pursuitSpeed = 25f;
    [SerializeField] private float patrolSpeed = 10f;
    [SerializeField] private float waypointNextDistance = 2f;

    [Header("Temporary Variables")]
    //Temporary Variables
    //[SerializeField] private bool Cycle;
    //[SerializeField] private bool Reverse;

    [SerializeField] private float speedRead;
    #endregion

    #region Start & Update
    // Start is called before the first frame update
    void Start()
    {
        print("I live!");
        alert = false;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        stateText.text = "IDLE";

        if (waypoints.Count > 0)
        {
            target = waypoints[waypointIndex];
        }

        //if (myEnum == MyEnum.Option1)
        //{
        //    print("YEAH BABY, FUCK, STEAK ON A MONDAY BABY");
        //}// for example
    }//End Start


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
                target = player.transform;

                alert = true;

                print("Pursuit");

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(player.transform.position);

                SetAiSpeed("Pursuit");

                stateText.text = $"{target}";
                if (distanceToPlayer <= faceRadius)
                    {
                        target = player.transform;

                        FaceTarget();
                    }
            
                speedRead = pursuitSpeed;
            }
        else
            {
                alert = false;

                print("Patrol");

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(waypoints[waypointIndex].transform.position);

                SetAiSpeed("Patrol");

                target = waypoints[waypointIndex];

                stateText.text = $"{target}";

                speedRead = patrolSpeed;
            }

        switch (alert)
            {
                case true:
                    print("I have been given purpose");
                    break;

                case false:
                    print("Give me a task to do pls");
                    break;
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
        }//End Set AI Destination

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
            //Checking to see if the AI has reached the last waypoint
            //if (waypointIndex >= waypoints.Count - 1)
            //{
            //    if (Cycle == true)
            //    {
            //        waypointIndex = 0;

            //        //waypoints.Reverse();

            //        target = waypoints[0];
            //    }
            //    else if (Reverse == true)
            //    {
            //        waypointIndex = 0;

            //        waypoints.Reverse();

            //        target = waypoints[0];
            //    }
            //    //waypointIndex = 0;

            //    //waypoints.Reverse();

            //    //target = waypoints[0];
            //}
            //else
            //{
            //    waypointIndex++;

            //    target = waypoints[waypointIndex];
            //}
            //SetAIDestination(target.position);

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