using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Current Bugs:
//    - AI currently moves to quickly to go to it's target without missing and having to loop back around
//    - 


//Things to add:
//    - State history (store the current and previous state that the AI was in)
//    - Rework AI pathing / pathfinding


//Suspicion Manager Notes:
//  - Look at Among Us task manager / meter for reference/inspiration on the overall suspicion manager
//    - 
//    - 


public class EnemyManager : MonoBehaviour
{
    #region Enumerations

    #region AI State Machine

    public enum EnemyStates
    {
        //TO DO: Add a Staggered / Stunned State
        //  - 

        PASSIVE,
        WARY,
        SUSPICIOUS,
        HOSTILE,
        ATTACK,
        RANGEDATTACK,
        STUNNED
    }

    [Header("AI State")]

    public EnemyStates stateMachine;

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

    #region Waypoints Logic
    [Header("Waypoints List")]
    //WILL BREAK IF THE LIST IS NOT THE TRANSFORM DATA TYPE
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

                        target = waypoints[0].position;
                    }
                else
                    {
                        waypointIndex++;

                        target = waypoints[waypointIndex].position;
                    }
                SetAIDestination(target);

                break;
            case CycleMethods.Reverse:
                //Insert reverse based method for navigating waypoints here
                if (waypointIndex >= waypoints.Count - 1)
                    {
                        waypointIndex = 0;

                        waypoints.Reverse();

                        target = waypoints[0].position;
                    }
                else
                    {
                        waypointIndex++;

                        target = waypoints[waypointIndex].position;
                    }
                SetAIDestination(target);

                break;
            default:
                print("Cycling method not found \a");
                break;
        }
    }
    #endregion

    #region Coroutines

    #endregion Coroutines

    #region Variables

    [Header("Private Variables")]
    [HideInInspector] private Vector3 target;
    [HideInInspector] private NavMeshAgent agent;
    [HideInInspector] private Rigidbody m_Rigidbody;
    [HideInInspector] public bool autoBraking = true;

    [Header("Object References")]
    [Tooltip("References the player object")]
    [SerializeField] private GameObject player;
    [Tooltip("References the guard's eyeball prefab / object")]
    [SerializeField] private EyeballScript eyeball;
    

    [Header("Diagnostic Text")]
    [Tooltip("References the state text (displays the state the guard is in)")]
    [SerializeField] private Text stateText;
    [Tooltip("References the target text (displays the guard's current target)")]
    [SerializeField] private Text targetText;
    [Tooltip("References the lose text for the game (this is NOT permanent)")]
    [SerializeField] private Text loseText;

    [Header("Guard Movement Speed")]
    [Tooltip("The speed that the AI moves at in the PATROL state")]
    [SerializeField] [Range(0, 10)] private float patrolSpeed = 5f;
    [Tooltip("The speed that the AI moves at in the SUSPICIOS state")]
    [SerializeField] [Range(0, 10)] private float susSpeed = 6.5f;
    [Tooltip("The speed that the AI moves at in the STUNNED state")]
    [SerializeField] [Range(0, 10)] private float stunSpeed = 0f;
    [Tooltip("The speed that the AI moves at in the HOSTILE state")]
    [SerializeField] [Range(0, 10)] private float hostileSpeed = 8f;

    [Header("Misc. Variables")]
    [Tooltip("The distance the guard needs to be from the target/player before it attacks them")]
    [SerializeField] private float attackRadius = 10f;
    [Tooltip("The distance the guards is from it's waypoint before it get's it's next waypoint")]
    [SerializeField] private float waypointNextDistance = 2f;
    [Tooltip("The speed at which the guard turns to face a target (functionality varies)")]
    [SerializeField] [Range (0, 50)]private float rotateSpeed;
    [Tooltip("When enabled, the guard will wait when it reaches it's 'waypointNextDistance'")]
    [SerializeField] private bool isWait;
    [Tooltip("The amount of time that the guard waits when 'isWait' is enabled")]
    [SerializeField] private float waitTime;
    [HideInInspector] private float waitTimeReset;

    [Header("Global Suspicion Manager Ref")]
    [Tooltip("Reference to the suspicion manager")]
    [SerializeField] private SuspicionManager suspicionManager;

    [Header("Debug / Testing Variables")]
    //Variable may need to be renamed in the future based on further implementations with Charlie
    [Tooltip("Duration of the guard's Stun state duration")]
    [SerializeField] private float stunTime;
    [HideInInspector] private float stunTimeReset;
    [SerializeField] [Range (0, 50)]private float guardKnockbackForce;

    #endregion

    #region Awake & Update

    #region Awake
    //---------------------------------//
    //  Using Awake() instead of Start() so that when spawning is functional, the AI won't break
    void Awake()
    {
        Init();
    }//End Awake
    #endregion

    #region Update
    //---------------------------------//
    //Function called every frame
    void Update()
    {


        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position + Vector3.up);

        //At all times be sure that there is a condition to at least ENTER and EXIT the state that the AI is being put into
        switch (stateMachine)
        {
            #region Passive Behavior
            case EnemyStates.PASSIVE:

                stateText.text = stateMachine.ToString();

                switch (isWait)
                {
                #region isWait == true
                    case true:

                        //Checks to see if it is at specified distance for getting it's next waypoint
                        if (Vector3.Distance(target, transform.position) <= waypointNextDistance)
                        {

                            if (waitTime > 0)
                            {
                                waitTime -= Time.fixedDeltaTime;

                            }
                            else if (waitTime <= 0)
                            {
                                waitTime = waitTimeReset;

                                SetNextWaypoint();
                            }

                        }

                        SetAiSpeed(patrolSpeed);

                        target = waypoints[waypointIndex].position;

                        //transform.position is being used because you cannot use Vector3 data when Transform is being called
                        SetAIDestination(target);

                        targetText.text = $"{target}";

                        FaceTarget(target);


                        //Exit condition
                        //Checking to see if the player is visible
                        if (eyeball.canCurrentlySeePlayer  /*&&*/ || eyeball.susLevel > 5)
                        {
                            //print("Player seen, susLevel over 5. Going into SUSPICIOUS state");
                            // PASSIVE >>>> SUSPICIOUS
                            stateMachine = EnemyStates.SUSPICIOUS;
                        }

                        break;
                #endregion isWait == true

                #region isWait == false
                    case false:

                        //Checks to see if it is at specified distance for getting it's next waypoint
                        if (Vector3.Distance(target, transform.position) <= waypointNextDistance)
                        {
                            SetNextWaypoint();
                        }

                        SetAiSpeed(patrolSpeed);

                        target = waypoints[waypointIndex].position;

                        //transform.position is being used because you cannot use Vector3 data when Transform is being called
                        SetAIDestination(target);

                        //targetText.text = $"{target}";
                        //Less confusing when showing the target on the debug canvas
                        targetText.text = $"{waypoints[waypointIndex]}";

                        FaceTarget(target);


                        //Exit condition
                        //Checking to see if the player is visible
                        if (eyeball.canCurrentlySeePlayer  /*&&*/ || eyeball.susLevel > 5)
                        {
                            //print("Player seen, susLevel over 5. Going into SUSPICIOUS state");
                            // PASSIVE >>>> SUSPICIOUS
                            stateMachine = EnemyStates.SUSPICIOUS;
                        }
                        break;
                        #endregion isWait == false
                }
                break;
            #endregion Passive Behavior

            #region Wary
            case EnemyStates.WARY:

                stateText.text = stateMachine.ToString();


                //AI Wary State 
                FaceTarget(target);
                // Insert timer
                //stateMachine = EnemyStates.PASSIVE;

                //stateMachine = EnemyStates.SUSPICIOUS;
                
                break;
            #endregion Wary

            #region Suspicious Behavior
            case EnemyStates.SUSPICIOUS:

                stateText.text = stateMachine.ToString();

                //Exit Condition > Hostile
                //Checking if the player is within the AI's look radius
                if (eyeball.canCurrentlySeePlayer == true || eyeball.susLevel > 5)
                {

                        SetAiSpeed(susSpeed);

                        target = eyeball.lastKnownLocation;

                        targetText.text = "Player";

                        //transform.position is being used because you cannot use Vector3 data when Transform is being called
                        SetAIDestination(target);

                        //Rework this so that it's based on the suspicion level instead of a generic radius
                        if (distanceToPlayer <= attackRadius)
                        {
                        // SUSPICIOUS >> HOSTILE
                        stateMachine = EnemyStates.HOSTILE;
                        }
                }

                //Exit Condition > Passive
                ////Double check the use of the > in this line, might be a type
                else if (eyeball.canCurrentlySeePlayer == false && eyeball.susLevel == 0)
                {
                    //Using transform.position in order to translate Vector3 data to Transform
                    //Setting the target back to the guard's waypoints for it's passive behavior
                    target = waypoints[waypointIndex].position;

                    //setting the destination to the now waypoints target
                    SetAIDestination(target);

                    //Returns the guard to it's patrolling behavior
                    stateMachine = EnemyStates.PASSIVE;

                }

                FaceTarget(target);

                break;
            #endregion Suspicious Behavior

            #region Hostile Behavior
            case EnemyStates.HOSTILE:

                stateText.text = stateMachine.ToString();

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

                FaceTarget(target);

                break;
            #endregion Hostile Behavior

            #region Attack Behavior
            //AI Attack state
            case EnemyStates.ATTACK:

                stateText.text = stateMachine.ToString();

                if (distanceToPlayer > attackRadius)
                {

                    // ATTACK >> HOSTILE
                    stateMachine = EnemyStates.HOSTILE;
                }
                
                //rework the timer method
                if (Timer(5f) == false)
                {

                    // ATTACK >> SUSPICIOUS
                    stateMachine = EnemyStates.SUSPICIOUS;
                }

                //Temp lose condition
                //Refine to take lack of player input from struggle QTE
                if (distanceToPlayer <= attackRadius)
                {
                    loseText.text = "Game Over";
                    SceneManager.LoadScene(3);
                }

                FaceTarget(target);

                break;
            #endregion Attack Behavior

            #region Ranged Attack Behavior
            case EnemyStates.RANGEDATTACK:

                stateText.text = stateMachine.ToString();

                //Insert ranged attack code

                break;
            #endregion Ranged Attack Behavior

            #region Stunned Behavior
            case EnemyStates.STUNNED:

                stateText.text = stateMachine.ToString();

                SetAiSpeed(stunSpeed);

                //experimenting with Time.fixedDeltaTime & Time.deltaTime
                stunTime -= Time.fixedDeltaTime;

                #region Don't Touch
                //agent.Move(new Vector3(transform.forward.x, 0 , transform.forward.z).normalized);
                //agent.Move(new Vector3((-transform.forward.x), 0, (transform.forward.z)).normalized);
                //agent.Move(new Vector3((transform.InverseTransformDirection(Vector3.forward).x), 0, (transform.InverseTransformDirection(Vector3.forward).z)));


                //Note: Currently sending the guard backward
                //Ideal force mode: Impulse
                //()
                //m_Rigidbody.AddForce((transform.InverseTransformDirection(Vector3.forward)) * (guardKnockbackForce), ForceMode.Impulse);
                //m_Rigidbody.AddForce(transform.up, ForceMode.Force);
                #endregion Don't Touch

                if (stunTime <= 0)
                {
                    //STUNNED >>>> PREVIOUS STATE (SUSPICIOS for now)
                    stateMachine = EnemyStates.SUSPICIOUS;

                    //after changing states, the stun time returns to the initially recorded time
                    stunTime = stunTimeReset;
                }
                break;
            #endregion Stunned Behavior

            #region Default Behavior / Bug Catcher
            default:

                stateText.text = ("ERROR: State not found");

                targetText.text = $"Target = {targetText}";

                FaceTarget(target);

                break;
            #endregion Default Behavior / Bug Catcher

        }

        suspicionManager.testInt = 1;

    }//End Update
    #endregion Update

    #endregion Awake & Update

    #region AI Functions

    //---------------------------------//
    //Called on Awake and initializes everything that is finalized and needs to be done at awake
    private void Init()
    {
        //Stores the user generated wait time
        waitTimeReset = waitTime;

        //Stores the user generated stun time
        stunTimeReset = stunTime;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        stateMachine = EnemyStates.STUNNED;

        //Checks to see if there is no value for the player object reference
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }

        #region Waypoints Check / Initial Start
        //checks to see if there are any objects in the waypoints list
        if (waypoints.Count > 0)
        {
            target = waypoints[waypointIndex].position;
        }
        else
        {
            print("No waypoints added to guard instance");
        }
        #endregion Waypoints Check / Initial Start

        FaceTarget(target);

        loseText.text = "";

        waitTimeReset = waitTime;

        stunTimeReset = stunTime;

        m_Rigidbody = GetComponent<Rigidbody>();

    }//End Init


    //---------------------------------//
    //Alert's the guard
    public void Alert(Vector3 alertLoc)
    {
        eyeball.susLevel = 6;

        //target = alertLoc;
        eyeball.lastKnownLocation = alertLoc;
    }//End Alert


    //---------------------------------//
    // Function for facing the player when the AI is withing stopping distance of the player
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

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


    //---------------------------------//
    //Draws shapes only visible in the editor
    private void OnDrawGizmos()
    {
        //Gizmo color
        Gizmos.color = Color.red;
        //Gizmo type
        Gizmos.DrawWireSphere(transform.position + Vector3.up, attackRadius);
    }//End OnDrawGizmos


    //---------------------------------//
    //Used as a timer, insert a float for the time and it returns when the time is over
    private bool Timer(float feedTime)
    {
        //Delete this method, it's god awful
        feedTime -= Time.deltaTime;

        if (feedTime <= 0)
        {
            return false;
        }
        else
        {
            //print($"{feedTime}");

            return true;
        }
    }//End Timer


    //---------------------------------//
    // Revive mechanic set for certain AI prefabs
    void Revive()
    {

    }//End Revive


    //---------------------------------//
    // Raises the security level for the area
    void RaiseSecurityLevel()
    {

    }//End RaiseSecurityLevel


    //---------------------------------//
    //Alerts other guards to let them know the player's location
    //Notes:
    //  - This will probably be done using a radius that rapidly expands and shrink
    //    guards that are caught within the radius of that rapid expansion / shrinking will have a condition met that runs another function / puts them in a suspicious state
    //  - The lastKnownLocation variable (literally printed as eyeball.lastKnownLocation in this case) will be set to the messaging guards' player report location
    //  - Guards will converge on this location
    //  - When this is completed ask Kevin if the behavior should be tweaked
    //
    //  - Consider renaming this function to CallForHelp()
    void AlertLocation()
    {

    }

    #endregion AI Functions

    //finding and eating donuts
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BaitItemScript>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}