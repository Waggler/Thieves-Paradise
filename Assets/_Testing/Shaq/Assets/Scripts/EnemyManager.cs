using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Current Bugs:
//    - 
//    - 

//Things to add:
//    - Create a feature that makes the AI calculate a path to it's target before it goes after it, can go from there and create logic on what to do if the target is unreachable
//      - https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.CalculatePath.html
//    - Begin work/ research on the state machine
//    - Make general function for setting AI target
//    -  Look into coroutines
//    - Better method for handling the Attack state (might need to make use of an animation controller)

//Done:
//    - Created Group of empty objects w/ parent for testing patrol functionality
//    - Setting target to waypoints and having the AI cycle through them
//    - Headers for different sections of code in order to better organize content displayed in the investigator
//    - Separate Waypoint navigation method that reverses the order of the waypoints (Back and forth instead of in circles)
//    - 

public class EnemyManager : MonoBehaviour
{
    #region Enumerations

    #region AI State Machine

    private enum EnemyStates
        {
            PASSIVE,
            WARY,
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

    [Header("Waypoint Cycling")]

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

    #region AI Coroutines
    IEnumerator Attack()
    {
        //if (stateMachine == EnemyStates.ATTACK)
        //{
            float timer = 10f;

            timer -= Time.deltaTime;

            print($"The time is: {timer}");

            print("YOU HAVE ENTERED THE COROUTINE");


            targetText.text = "THWAK";

            //print("Talkin' a lot of shit for someone in crusading distance");

            if (timer <= 0)
            {
                stateMachine = EnemyStates.SUSPICIOUS;

                targetText.text = $"{target}";

                return null;
            }
            return null;
        //}
        //else
        //{
        //    return null;
        //}
    }

    #endregion

    #region Variables

    //Important Variables
    [Header("Private Variables")]
    private Transform target;
    private NavMeshAgent agent;

    [Header("Debug Variables")]
    [SerializeField] private GameObject player;
    
    [Header("Diagnostic Text for AI instances (Dev only)")]
    [SerializeField] private Text stateText;
    [SerializeField] private Text targetText;

    [Header("Movement Variables")]
    //[SerializeField] private float lookRadius = 10f;
    //[SerializeField] private float faceRadius = 10f;
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float pursuitSpeed = 25f;
    [SerializeField] private float waypointNextDistance = 2f;
    [SerializeField] private float rotateSpeed;
    //[SerializeField] private GameObject projectile;


    [Header("Eyeball Integration")]
    [SerializeField] private EyeballScript eyeball;


    [Header("Guard Movement Speed")]
    [SerializeField] private float patrolSpeed = 10f;
    //will be used when there is specific behavior for the SUSPICIOUS state
    //[SerializeField] private float sussySpeed = 10f;


    //[Header("Temporary Variables")]
    //Temporary Variables


    #endregion

    #region Awake & Update

    #region Awake
    //---------------------------------//
    //  Using Awake() instead of Start() so that when spawning is functional, the AI won't break
    void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }

        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        stateMachine = EnemyStates.PASSIVE;

        //checks to see if there are any objects in the waypoints list
        //if there are, then it will start the game by setting the target to the first waypoint on the list
        if (waypoints.Count > 0)
        {
            target = waypoints[waypointIndex];
        }
    }//End Awake
    #endregion


    //---------------------------------//
    void Update()
    {

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        
        //At all times be sure that there is a condition to at least ENTER and EXIT the state that the AI is being put into
        switch (stateMachine)
        {
            #region Passive Behavior
            case EnemyStates.PASSIVE:
                //AI Passive state
                stateText.text = EnemyStates.PASSIVE.ToString();

                if (Vector3.Distance(target.transform.position, transform.position) <= waypointNextDistance)

                    {
                        SetNextWaypoint();
                    }
                //if (distanceToPlayer <= lookRadius)
                //Checking to see if the player is visible
                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(waypoints[waypointIndex].transform.position);

                SetAiSpeed("Patrol");

                target = waypoints[waypointIndex];

                targetText.text = $"{target}";

                if (eyeball.canCurrentlySeePlayer && eyeball.susLevel > 5)
                    {
                        print("Player seen, susLevel over 5. Going into SUSPICIOUS state");
                        // PASSIVE >>>> SUSPICIOUS
                        stateMachine = EnemyStates.SUSPICIOUS;
                    }

                break;
            #endregion

            #region Wary
            case EnemyStates.WARY:
                //AI Wary State 
                FaceTarget();
                // Insert timer
                //stateMachine = EnemyStates.PASSIVE;

                //stateMachine = EnemyStates.SUSPICIOUS;
                
                break;
            #endregion

            #region Suspicious Behavior
            case EnemyStates.SUSPICIOUS:
                //AI Suspicious state
                stateText.text = EnemyStates.SUSPICIOUS.ToString();

                //Checking if the player is within the AI's look radius
                //if (distanceToPlayer <= lookRadius)
                if (eyeball.canCurrentlySeePlayer == true || eyeball.susLevel > 5)
                {
                    //transform.position is being used because you cannot use Vector3 data when Transform is being called
                    SetAIDestination(player.transform.position);

                        SetAiSpeed("Pursuit");

                        target = player.transform;

                        targetText.text = $"{target}";

                        FaceTarget();

                        
                        if (distanceToPlayer <= attackRadius)
                        {
                        // SUSPICIOUS >> PASSIVE
                        stateMachine = EnemyStates.HOSTILE;
                        }
                    }
                else if (eyeball.canCurrentlySeePlayer == false && eyeball.susLevel > 0)
                {
                    //Using transform.position in order to translate Vector3 data to Transform
                    target.transform.position = eyeball.lastKnownLocation;

                    SetAIDestination(target.transform.position);
                }
                else
                    {
                        // SUSPICIOUS >> PASSIVE
                        stateMachine = EnemyStates.PASSIVE;   
                    }
                break;
            #endregion

            #region Hostile Behavior
            case EnemyStates.HOSTILE:
                //AI Hostile state
                stateText.text = EnemyStates.HOSTILE.ToString();

                if (distanceToPlayer <= attackRadius)
                    {
                        // HOSTILE >> ATTACK
                        stateMachine = EnemyStates.ATTACK;
                    }
                else
                    {
                        // HOSTILE >> SUSPICIOUS
                        stateMachine = EnemyStates.SUSPICIOUS;
                    }
                break;
            #endregion

            #region Attack Behavior
            case EnemyStates.ATTACK:
                //AI Attack state
                FaceTarget();

                stateText.text = EnemyStates.ATTACK.ToString();

                //print("You are being attacked");

                //Psuedo code for new attack method
                //if (target == player  && Vector3.Distance(target.transform.position, transform.position) <= killRadius)
                //{
                //    Kill Player

                //    Cut to black

                //    Shitty Lose screen saying "You've been caught"
                //}


                //Check for collision with player
                //If collided, they lose


                break;
            #endregion

            #region Ranged Attack Behavior
            case EnemyStates.RANGEDATTACK:
                //AI Suspicious state
                stateText.text = EnemyStates.RANGEDATTACK.ToString();
                
                RangedAttack();
                break;
            #endregion

            #region Default Behavior / Bug Catcher
            default:
                stateText.text = "State not found";

                target = null;

                targetText.text = $"Target = {targetText}";
                break;
            #endregion
        }

    }//End Update
    #endregion

    #region AI Functions
    //---------------------------------//
    // Function for facing the player when the AI is withing stopping distance of the player
    void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed);
        }//End FaceTarget

    //---------------------------------//
    // Sets the AI speed
    // Needs to be reworked / improved
    void SetAiSpeed(string detection)
        {
        detection = detection.ToUpper();

            switch (detection)
            {
            //Setting pursuit speed
                case "PURSUIT":
                    agent.speed = Mathf.Lerp(patrolSpeed, pursuitSpeed, 1);
                    break;
            //Setting patrol speed
                case "PATROL":
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

    private void OnDrawGizmos()
    {
        //Gizmo color
        Gizmos.color = Color.red;
        //Gizmo type
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

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

    void AlertLocation()
    {

    }

    #region Attacks
    // Generic Funcitons to be add WAY down the line
    private void RangedAttack()
        {
        }//End RangedAttack

    private void CQCAttack(float timer)
        {
            //Any code seen here is meant for debugging purposes only.

            // At this point, the timer is functional but the method of calling the CQCAttack function needs to be worked on
            // atm currently known method results in infinitely calling the function when the conditions for calling it are met
            
            timer -= Time.deltaTime;

            print($"The time is: {timer}");

            targetText.text = "THWAK";

            //print("Talkin' a lot of shit for someone in crusading distance");

            if (timer <= 0)
                {
                    stateMachine = EnemyStates.SUSPICIOUS;

                    targetText.text = $"{target}";
                }   
        }//End CQCAttack

    private void Timer(float length)
    {
        length -= Time.deltaTime;

        print($"Time remaining: {length}");


        if (length <= 0)
        {
            return;
        }
    }

    #endregion

    #endregion

    #region Waypoints Logic
    [Header("Waypoint Variables")]
    [SerializeField] private List<Transform> waypoints;
    //waypoints.Count will be used to get the number of points in the list (similar to array.Length)
    private int waypointIndex = 0;


    #endregion
    
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
}