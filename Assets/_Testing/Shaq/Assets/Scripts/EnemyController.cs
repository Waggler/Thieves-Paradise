using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//Current Bugs:
//    - Shoddy timer made in CQCAttack function is currently broken

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
            ATTACK,
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
    private Transform target;
    private NavMeshAgent agent;

    [Header("Debug Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private Text stateText;
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float faceRadius = 10f;
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float pursuitSpeed = 25f;
    [SerializeField] private float patrolSpeed = 10f;
    //will be used when there is specific behavior for the SUSPICIOUS state
    //[SerializeField] private float sussySpeed = 10f;
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
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        stateText.text = "IDLE";
        stateMachine = EnemyStates.PASSIVE;

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
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        
        //At all times be sure that there is a condition to at least ENTER and EXIT the state that the AI is being put into
        switch (stateMachine)
        {
            #region Passive Behavior
            case EnemyStates.PASSIVE:
                print("Patrol");
                if (Vector3.Distance(target.transform.position, transform.position) <= waypointNextDistance)
                    {
                        SetNextWaypoint();
                    }
                if (distanceToPlayer <= lookRadius)
                    {
                        stateMachine = EnemyStates.SUSPICIOUS;
                    }

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(waypoints[waypointIndex].transform.position);

                SetAiSpeed("Patrol");

                target = waypoints[waypointIndex];

                stateText.text = $"{target}";

                speedRead = patrolSpeed;

                break;
            #endregion
            #region Suspicious Behavior
            case EnemyStates.SUSPICIOUS:
                //AI Suspicious state
                    //Checking if the player is within the AI's look radius
                    if (distanceToPlayer <= lookRadius)
                        {
                            //transform.position is being used because you cannot use Vector3 data when Transform is being called
                            SetAIDestination(player.transform.position);

                            SetAiSpeed("Pursuit");

                            target = player.transform;

                            stateText.text = $"{target}";

                            speedRead = pursuitSpeed;

                            //Checking if the player is within the AI's face radius
                            if (distanceToPlayer <= faceRadius)
                            {
                                target = player.transform;

                                FaceTarget();
                            }
                            
                        //Reconsider the placement of this pls
                        stateMachine = EnemyStates.HOSTILE;
                        }
                    else
                    {
                    stateMachine = EnemyStates.PASSIVE;   
                    }
                break;
            #endregion
            #region Hostile Behavior
            case EnemyStates.HOSTILE:
                //AI Hostile state
                if (distanceToPlayer <= attackRadius)
                    {
                        agent.speed = 0f;

                        stateMachine = EnemyStates.ATTACK;
                   }
                else
                {
                    stateMachine = EnemyStates.SUSPICIOUS;
                }
                break;
            #endregion
            #region Attack Behavior
            case EnemyStates.ATTACK:
                //AI Suspicious state
                CQCAttack();
                break;
            #endregion
            #region Ranged Attack Behavior
            case EnemyStates.RANGEDATTACK:
                //AI Suspicious state
                RangedAttack();
                break;
            #endregion
            #region Default Behavior / Bug Catcher
            default:
                print("State Not Found \a");
                break;
            #endregion
        }

    }//End Update
    #endregion

    #region AI Functions
    //---------------------------------//
    // Used to draw a sphere tied to the AI's current look radius. Really just serving as a diagnostic tool
    void OnDrawGizmosSelected()
        {
            //Drawing a magenta sphere
            Gizmos.color = Color.magenta;
            //Sphere radius tied to Look Radius variable
            Gizmos.DrawWireSphere(transform.position, lookRadius);

            //Drawing a Cyan Sphere
            Gizmos.color = Color.cyan;
            //Sphere radius tied to Face Radius variable
            Gizmos.DrawWireSphere(transform.position, faceRadius);

            //Drawing a Red sphere
            Gizmos.color = Color.red;
            //Sphere radius tied to Attack Radius variable
            Gizmos.DrawWireSphere(transform.position, attackRadius);
    }//End DrawGizmos

    //---------------------------------//
    // Function for facing the player when the AI is withing stopping distance of the player
    void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime /** 2f*/);
        }//End FaceTarget

    //---------------------------------//
    // Sets the AI speed
    // Needs to be reworked / improved
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
    // Function for setting AI destination
    void SetAIDestination(Vector3 point)
        {
            agent.SetDestination(point);
        }//End SetAIDestination

    //---------------------------------//
    // Revive mechanic set for certain AI prefabs
    void Revive()
    {

    }//End Revive

    //---------------------------------//
    // Call for reinforcements
    void CallForHelp()
    {

    }//End CallForHelp

    //---------------------------------//
    // Raises the security level for the area
    void RaiseSecurityLevel()
    {

    }//End RaiseSecurityLevel

    #region Attacks
    // Generic Funcitons to be add WAY down the line
    private void RangedAttack()
        {
        }//End RangedAttack

    private void CQCAttack()
        {
            //Any code seen here is meant for debugging purposes only.
            double timer;

            timer = Time.deltaTime;

            print($"The time is: {timer}");

            print("Talkin' a lot of shit for someone in crusading distance");

            if (timer == .001d)
                {
                    stateMachine = EnemyStates.HOSTILE;
                }   
        }//End CQCAttack

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



    #region Function Graveyard
    //--------------Attempted Function for setting target variable-------------------//
    //void SetTarget(Transform targetObj)
    //    {
    //        targetObj = target;

    //        return targetObj;
    //    }//End SetTarget
    // Consider deleting this function as a whole; setting the target variable can already be done with a brief line of  code
    //---------------------------------//




    //---------------------------------//
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
    #endregion
}