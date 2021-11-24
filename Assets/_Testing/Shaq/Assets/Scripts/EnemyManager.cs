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
//    - Start adding event handlers for frequent things like changing target and speed


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

    #region EventHandlers?

    #endregion EventHandlers?

    #region Variables

    //---------------------------------------------------------------------------------------------------//

    [Header("Private Variables")]

    private Vector3 target;
    private NavMeshAgent agent;
    //DO not delete
    private bool autoBraking = true;

    //---------------------------------------------------------------------------------------------------//

    [Header("Object References")]

    [Tooltip("References the player object")]
    [SerializeField] private GameObject player;

    [Tooltip("References the guard's eyeball prefab / object")]
    [SerializeField] private EyeballScript eyeball;

    //---------------------------------------------------------------------------------------------------//

    [Header("Diagnostic Text")]

    [Tooltip("References the state text (displays the state the guard is in)")]
    [SerializeField] private Text stateText;

    [Tooltip("References the target text (displays the guard's current target)")]
    [SerializeField] private Text targetText;

    [Tooltip("References the lose text for the game (this is NOT permanent)")]
    [SerializeField] private Text loseText;

    //---------------------------------------------------------------------------------------------------//

    [Header("Guard Movement Speed")]

    [Tooltip("The speed that the AI moves at in the PATROL state")]
    [SerializeField] [Range(0, 10)] private float patrolSpeed = 5f;

    [Tooltip("The speed that the AI moves at in the SUSPICIOS state")]
    [SerializeField] [Range(0, 10)] private float susSpeed = 6.5f;

    [Tooltip("The speed that the AI moves at in the STUNNED state")]
    [SerializeField] [Range(0, 10)] private float stunSpeed = 0f;

    [Tooltip("The speed that the AI moves at in the HOSTILE state")]
    [SerializeField] [Range(0, 10)] private float hostileSpeed = 8f;

    [Tooltip("The speed that the AI moves at in the ATTACK state")]
    [SerializeField] [Range(0, 10)] private float attackSpeed = 0f;

    //---------------------------------------------------------------------------------------------------//
    [Header("Patrol Wait Time")]

    [Tooltip("When enabled, the guard will wait when it reaches it's 'waypointNextDistance'")]
    [SerializeField] private bool isWait;

    [Tooltip("The amount of time that the guard waits when 'isWait' is enabled")]
    [SerializeField] public float waitTime;

    [Tooltip("The minimum generated value for the wait time")]
    [SerializeField] [Range (1, 3)] private float waitMin = 1f;

    [Tooltip("The maximum generated value for the wait time")]
    [SerializeField] [Range (3, 5)]  private float waitMax = 5f;


    //---------------------------------------------------------------------------------------------------//
    [Header("Suspicious Wait Time")]

    [Tooltip("The minimum generated value for the wait time")]
    [SerializeField] [Range(1, 3)] private float randWaitMin = 1f;

    [Tooltip("The maximum generated value for the wait time")]
    [SerializeField] [Range(3, 5)] private float randWaitMax = 5f;

    private float randWaitTime = 3f;

    //---------------------------------------------------------------------------------------------------//

    [Header("Global Suspicion Manager Ref")]

    [Tooltip("Reference to the suspicion manager")]

    [SerializeField] private SuspicionManager suspicionManager;

    //---------------------------------------------------------------------------------------------------//

    [Header("Audio Variables")]

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private bool isAudioSourcePlaying;

    //---------------------------------------------------------------------------------------------------//

    [Header("Debug / Testing Variables")]

    //Variable may need to be renamed in the future based on further implementations with Charlie
    [Tooltip("Duration of the guard's Stun state duration")]
    [SerializeField] private float stunTime;

    private float stunTimeReset;

    //Save implementation for next sprint
    [SerializeField] [Range (0, 50)] private float guardKnockbackForce;

    [Tooltip("Duration of the guard's Attack state duration")]
    [SerializeField] private float attackTime;
    private float attackTimeReset;

    //---------------------------------------------------------------------------------------------------//

    [Header("Misc. Variables")]

    [Tooltip("The distance the guard needs to be from the target/player before it attacks them")]
    [SerializeField] private float attackRadius = 10f;

    [Tooltip("The distance the guards is from it's waypoint before it get's it's next waypoint")]
    [SerializeField] private float waypointNextDistance = 2f;

    [Tooltip("The speed at which the guard turns to face a target (functionality varies)")]
    [SerializeField] [Range (0, 50)]private float rotateSpeed;

    private float randWaitTimeReset;

    private Vector3 searchLoc;
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

                //Reseting alert related variables
                if (isAudioSourcePlaying == true)
                {
                    audioSource.Stop();

                    isAudioSourcePlaying = false;
                }

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
                                //waitTime = waitTimeReset;
                                waitTime = Random.Range(waitMin, waitMax);

                                //Figure out why this function is being called twice
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
                            stateMachine = EnemyStates.HOSTILE;
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
                            stateMachine = EnemyStates.HOSTILE;
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


                SetAiSpeed(susSpeed);

                #region New Suspicious Behaviour

                //New Sus Guard Behaviour:
                //-When eyeball suslevel reaches threshold:
                //  -Guard stops
                //  - Goes in/ faces random directinos, enters searching anim, exits, goes in/ faces another random direction and repeats behaviour

                //  -Repeats these actions until eyeball.suslevel reaches 0 again

                //Add timer here

                //Add timer reset & function call at the end of timer

                //Think of better names for these variables, they are confusing as shit
                if (randWaitTime > 0)
                {
                    randWaitTime -= Time.fixedDeltaTime;

                }
                else if (randWaitTime <= 0)
                {

                    //randWaitTime = randWaitTimeReset;
                    randWaitTime = Random.Range(randWaitMin, randWaitMax);

                    FaceTarget(target);

                    targetText.text = ($"{target}");

                    SetAIDestination(GenerateRandomPoint());

                }

                #region Delete later
                ////Start of Method
                //float randPointRad = 5f;

                //float getNextPoint = 0.5f;

                //Vector3 randDirection = Random.insideUnitSphere * randPointRad;

                ////print(randDirection);

                //NavMeshHit hit;

                ////NavMesh.SamplePosition(transform.position, out hit, randPointRad, 1)
                ////Returns a bool
                ////Also generates a hit point via the HIT variable
                //if (NavMesh.SamplePosition(transform.position, out hit, randPointRad, 1))
                //{
                //    //Copying code for AI getting new waypoint
                //    if (Vector3.Distance(transform.position, hit.position) <= getNextPoint)
                //    {
                //        //Testing to see if conditino is met
                //        print("New Point Generated");

                //        //currently flawed
                //        target = hit.position;

                //        print(hit.position);
                //    }

                //    //target = randDirection;
                //}


                ////End of Method
                #endregion

                #endregion New Suspicious Behaviour
                
                FaceTarget(target);

                break;
            #endregion Suspicious Behavior

            #region Hostile Behavior
            case EnemyStates.HOSTILE:

                stateText.text = stateMachine.ToString();

                //SetAiSpeed(hostileSpeed);

                #region New Hostile Behaviour
                //Exit Condition > Hostile
                //Checking if the player is within the AI's look radius
                if (eyeball.canCurrentlySeePlayer == true || eyeball.susLevel > 5)
                {

                    SetAiSpeed(hostileSpeed);

                    target = eyeball.lastKnownLocation;

                    targetText.text = "Player";

                    //transform.position is being used because you cannot use Vector3 data when Transform is being called
                    SetAIDestination(target);

                    //Playing Alert Audio
                    if (isAudioSourcePlaying == false)
                    {
                        audioSource.Play();

                        isAudioSourcePlaying = true;
                    }

                    //Rework this so that it's based on the suspicion level instead of a generic radius
                    if (distanceToPlayer <= attackRadius)
                    {
                        // HOSTILE >> SUSPICIOUS
                        stateMachine = EnemyStates.SUSPICIOUS;
                        player.GetComponent<PlayerMovement>().IsStunned = true;
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

                    //Returns the guard to it's suspicious behaviour
                    stateMachine = EnemyStates.SUSPICIOUS;

                }
                #endregion New Hostile Behaviour

                #region Old Hostile Behaviour
                //if (distanceToPlayer <= attackRadius)
                //    {
                //        // HOSTILE >> ATTACK
                //        stateMachine = EnemyStates.ATTACK;



                //    }
                //else
                //    {
                //        // HOSTILE >> SUSPICIOUS
                //        stateMachine = EnemyStates.SUSPICIOUS;
                //    }
                #endregion Old Hostile Behaviour

                FaceTarget(target);

                break;
            #endregion Hostile Behavior

            #region Attack Behavior
            //AI Attack state
            case EnemyStates.ATTACK:

                stateText.text = stateMachine.ToString();

                SetAiSpeed(attackSpeed);

                #region Exit Condition(s)
                if (distanceToPlayer > attackRadius)
                {
                    // ATTACK >> HOSTILE
                    stateMachine = EnemyStates.SUSPICIOUS;
                }
                #endregion Exit Condition(s)



                ////Temp lose condition
                ////Refine to take lack of player input from struggle QTE
                //if (distanceToPlayer <= attackRadius)
                //{
                //    loseText.text = "Game Over";
                //    SceneManager.LoadScene(3);
                //}

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
        isAudioSourcePlaying = false;

        //Stores the user generated stun time
        stunTimeReset = stunTime;

        //Stores the user generated random direction time
        randWaitTimeReset = randWaitTime;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;
        stateMachine = EnemyStates.SUSPICIOUS;



        //REMOVE THIS WHEN TELLING PEOPLE ITS FINE TO FUCK WITH GUARD
        eyeball.susLevel = 10f;



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

        stunTimeReset = stunTime;
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
    // Finding and eating donuts
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BaitItemScript>() != null)
        {
            Destroy(other.gameObject);
        }
    }//End OnTriggerEnter


    private Vector3 GenerateRandomPoint()
    {
        float randPointRad = 1f;

        //Vector3 randDirection = Random.insideUnitCircle * randPointRad;
        Vector3 randDirection = (target - transform.position).normalized;

        //NavMesh.SamplePosition(transform.position, out hit, randPointRad, 1)
        //Returns a bool
        //Also generates a hit point via the HIT variable
        //Possible Bug: the out hit is hitting the guard's mesh and fucking with the suspicion behaviour
        if (NavMesh.SamplePosition(randDirection + transform.position, out NavMeshHit hit, randPointRad, 1) == true)
        {
                searchLoc = hit.position;
                return searchLoc;
        }
        else
        {
            print("Random location not found");
            return transform.position;
        }
    }


    //---------------------------------//
    //Draws shapes only visible in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, attackRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(searchLoc, .5f);
    }//End OnDrawGizmos


    //---------------------------------//
    // Raises the security level for the area
    void RaiseSecurityLevel()
    {

    }//End RaiseSecurityLevel

    #endregion AI Functions


}