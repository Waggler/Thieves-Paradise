using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Current Bugs:
//    - AI currently moves to quickly to go to it's target without missing and having to loop back around
//    - 

//Things to add:
//    - Refactor SetAISpeed() method
//    - 
//    - 

//Done:
//    - Barebone functionality between eyeball prefab and guardAI
//    - 
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
    //bool NavMeshAgent.autoBraking();


    [Header("Object References")]
    [SerializeField] private GameObject player;
    [SerializeField] private EyeballScript eyeball;
    

    [Header("Diagnostic Text")]
    [SerializeField] private Text stateText;
    [SerializeField] private Text targetText;

    [Header("Guard Movement Speed")]
    [SerializeField] [Range(0, 10)] private float patrolSpeed = 5f;
    [SerializeField] [Range(0, 10)] private float susSpeed = 6.5f;
    [SerializeField] [Range(0, 10)] private float hostileSpeed = 8f;

    [Header("Misc. Variables")]
    [SerializeField] private float attackRadius = 10f;
    [SerializeField] private float waypointNextDistance = 2f;
    [SerializeField] private float rotateSpeed;


    //[Header("Debug Variables")]
    //[SerializeField] bool shitBool = true;


    #endregion

    #region Awake & Update

    #region Awake
    //---------------------------------//
    //  Using Awake() instead of Start() so that when spawning is functional, the AI won't break
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        stateMachine = EnemyStates.PASSIVE;

        //Checks to see if there is no value for the player object reference
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }

        //checks to see if there are any objects in the waypoints list
        if (waypoints.Count > 0)
        {
            target = waypoints[waypointIndex];
        }
        else
        {
            print("No waypoints added to guard instance");
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
                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(waypoints[waypointIndex].transform.position);

                SetAiSpeed(patrolSpeed);

                target = waypoints[waypointIndex];

                targetText.text = $"{target}";


                //Checking to see if the player is visible
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

                        SetAiSpeed(susSpeed);

                        target.transform.position = eyeball.lastKnownLocation;

                        targetText.text = $"Player";

                        //transform.position is being used because you cannot use Vector3 data when Transform is being called
                        SetAIDestination(player.transform.position);

                        FaceTarget();
                        
                        if (distanceToPlayer <= attackRadius)
                        {
                        // SUSPICIOUS >> HOSTILE
                        stateMachine = EnemyStates.HOSTILE;
                        }
                    }

                else if (eyeball.canCurrentlySeePlayer == false && eyeball.susLevel > 0)
                {
                    //Using transform.position in order to translate Vector3 data to Transform
                    target.transform.position = eyeball.lastKnownLocation;

                    SetAIDestination(target.transform.position);

                    //SetAIDestination(waypoints[waypointIndex].transform.position);

                    //Destroy(GetComponent<PlayerController>);
                }

                else
                    {
                        target = waypoints[waypointIndex];

                        //SetAIDestination(waypoints[waypointIndex].transform.position);
                        SetAIDestination(target.transform.position);
                        
                        // SUSPICIOUS >> PASSIVE
                        stateMachine = EnemyStates.PASSIVE;   
                    }
                break;
            #endregion

            #region Hostile Behavior
            case EnemyStates.HOSTILE:
                //AI Hostile state
                stateText.text = EnemyStates.HOSTILE.ToString();

                SetAiSpeed(hostileSpeed);

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
            //AI Attack state
            case EnemyStates.ATTACK:

                

                FaceTarget();

                stateText.text = EnemyStates.ATTACK.ToString();

                if (Timer(5f) == false)
                {
                    stateMachine = EnemyStates.SUSPICIOUS;
                }

                if (target == player && Vector3.Distance(target.transform.position, transform.position) <= attackRadius)
                {
                    //Destroy(GetComponent<>);

                    //Kill player

                    //Cut to black or

                    //Lose screen
                }

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
    void SetAiSpeed(float speed)
    {
        agent.speed = Mathf.Lerp(agent.speed, speed, 1);
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
    //Used as a timer, insert a float for the time and it returns when the time is over
    bool  Timer(float feedTime)
    {
        feedTime -= Time.deltaTime;

        if (feedTime <= 0)
        {
            return false;
        }
        else
        {
            print($"{feedTime}");

            return true;
        }
    }//End Timer

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
    #endregion

    #region Waypoints Logic
    [Header("Waypoints List")]
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