using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//To Do:
//  - State transitions are broken
//      - Transition from PASSIVE to HOSTILE is currently broken
//  - Continue testing improvements and redundancies that are removed from script
//  - Consider making a seperate section of script for managing states (seperation of State actions and State management)

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

    #endregion AI State Machine

    #region AI Cycle Methods

    [System.Serializable]
    private enum CycleMethods
    {
        Cycle,
        Reverse
    }

    [Header("Waypoint Cycling")]

    [SerializeField] CycleMethods waypointMethod;

    #endregion AI Cycle Methods

    #endregion Enumerations

    #region Waypoint Cycle Methods List
    [SerializeField] private static readonly List<CycleMethods> Methods = new List<CycleMethods>
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
    #endregion Waypoints Functions

    #region EventHandlers (?)

    #endregion EventHandlers (?)

    #region Variables

    //---------------------------------------------------------------------------------------------------//

    //Private Variables

    [Tooltip("The guard's target")]
    private Vector3 target;

    [Tooltip("The NavMesh Agent component the object that this script is attatched to")]
    private NavMeshAgent agent;

    [Tooltip("The path generated by the NavMesh Agent")]
    private NavMeshPath path;

    [Tooltip("The randomly generated point for guard to go to")]
    private Vector3 searchLoc;

    //---------------------------------------------------------------------------------------------------//

    [Header("Object References")]

    [Tooltip("References the player object")]
    [SerializeField] private GameObject player;

    [Tooltip("References the guard's eyeball prefab / object")]
    [SerializeField] private EyeballScript eyeball;

    [Tooltip("References the guard's animator script")]
    [SerializeField] GuardAnimatorScript guardAnim;

    [SerializeField] private GameObject surpriseVFX;

    [SerializeField] private GameObject confusedVFX;

    //[Tooltip("Reference to the suspicion manager")]
    //[SerializeField] private SuspicionManager suspicionManager;

    //---------------------------------------------------------------------------------------------------//

    [Header("Diagnostic Text")]

    [Tooltip("References the state text (displays the state the guard is in)")]
    [SerializeField] private Text stateText;

    [Tooltip("References the target text (displays the guard's current target)")]
    [SerializeField] private Text targetText;

    //[Tooltip("References the lose text for the game (this is NOT permanent)")]  TEMPE WUZ HERE
    //[SerializeField] private Text loseText;

    //---------------------------------------------------------------------------------------------------//

    [Header("Minimum Sus Levels for States")]

    [Tooltip("Minimum Suspicion level to enter this state")]
    //Implied Min Value of 0
    [SerializeField] private float passiveSusMax = 3;
                                            
    [Tooltip("Minimum Suspicion level to enter this state")]
    [SerializeField] private float warySusMin = 3.1f;
                                            
    [SerializeField] private float warySusMax = 4;
                                            
    [Tooltip("Minimum Suspicion level to enter this state")]
    [SerializeField] private float sussySusMin = 4.1f;
                                            
    [SerializeField] private float sussySusMax = 5;
                                            
    [Tooltip("Minimum Suspicion level to enter this state")]
    //Implied Max Value of eyeball.susLevel max
    [SerializeField]  private float hostileSusMin = 5.1f;

    //---------------------------------------------------------------------------------------------------//

    [Header("Guard Movement Speeds")]

    [Tooltip("The speed that the AI moves at in the PATROL state")]
    [SerializeField] [Range(0, 30)] public float patrolSpeed = 5f;

    [Tooltip("The speed that the AI moves at in the WARY state")]
    [SerializeField] [Range(0, 30)] public float warySpeed = 4f;

    [Tooltip("The speed that the AI moves at in the SUSPICIOS state")]
    [SerializeField] [Range(0, 30)] public float susSpeed = 6.5f;

    [Tooltip("The speed that the AI moves at in the STUNNED state")]
    [SerializeField] [Range(0, 30)] public float stunSpeed = 0f;

    [Tooltip("The speed that the AI moves at in the HOSTILE state")]
    [SerializeField] [Range(0, 30)] public float hostileSpeed = 8f;

    [Tooltip("The speed that the AI moves at in the ATTACK state")]
    [SerializeField] [Range(0, 30)] public float attackSpeed = 0f;

    //---------------------------------------------------------------------------------------------------//
    [Header("Patrol Wait Time")]

    [Tooltip("When enabled, the guard will wait when it reaches it's 'waypointNextDistance'")]
    [SerializeField] private bool isPatrolWait;

    [Tooltip("The amount of time that the guard waits when 'isWait' is enabled")]
    [SerializeField] public float patrolWaitTime;

    [Tooltip("The minimum generated value for the wait time")]
    [SerializeField] [Range(1, 3)] private float patrolWaitMin = 1f;

    [Tooltip("The maximum generated value for the wait time")]
    [SerializeField] [Range(3, 5)] private float patrolWaitMax = 5f;


    //---------------------------------------------------------------------------------------------------//
    [Header("Suspicious Wait Time")]

    [Tooltip("The minimum generated value for the wait time")]
    [SerializeField] [Range(1, 3)] private float randWaitMin = 1f;

    [Tooltip("The maximum generated value for the wait time")]
    [SerializeField] [Range(3, 5)] private float randWaitMax = 5f;

    [Tooltip("Randomly generated value inserted as the start of a timer (For guard SUS state waiting)")]
    private float randWaitTime = 3f;

    [Tooltip("The distance from the guard that a random point can be generated")]
    [SerializeField] [Range (1, 3)] private float randPointRad = 1f;

    //---------------------------------------------------------------------------------------------------//

    [Header("Audio Variables")]

    [SerializeField] private AudioSource audioSource;

    [SerializeField] private bool isAudioSourcePlaying;

    //---------------------------------------------------------------------------------------------------//

    [Header("Debug / Testing Variables")]

    public bool isStunned;

    //Variable may need to be renamed in the future based on further implementations with Charlie
    [Tooltip("Duration of the guard's Stun state duration")]
    [SerializeField] private float stunTime;

    [Tooltip("")]
    private float stunTimeReset;

    //Save implementation for next sprint
    [SerializeField] [Range (0, 50)] private float guardKnockbackForce;

    [Tooltip("Duration of the guard's Attack state duration")]
    [SerializeField] private float attackTime;
    private float attackTimeReset;

    //---------------------------------------------------------------------------------------------------//

    [Header("Misc. Variables")]

    [Tooltip("The distance the guard needs to be from the target/player before it attacks them")]
    [SerializeField] [Range (0, 2)]private float attackRadius = 10f;

    [Tooltip("The distance the guards is from it's waypoint before it get's it's next waypoint")]
    [SerializeField] private float waypointNextDistance = 2f;

    [SerializeField] private GameObject playerCaptureTeleportLoc;

    [SerializeField] private GameObject playerReleaseTeleportLoc;

    private float oneTimeUseTimer = 2f;

    private float oneTimeUseTimerReset;

    private bool surpriseVFXBoolCheck;

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

        if (stateMachine == EnemyStates.HOSTILE)
        {
            surpriseVFXBoolCheck = true;
        }


        //eyeball.susLevel = warySusMin;

        //At all times be sure that there is a condition to at least ENTER and EXIT the state that the AI is being put into
        switch (stateMachine)
        {
            #region Passive Behavior
            //Testing case guards
            //NOTE: Case Guards MUST BE A BOOL OR BOOL EXPRESSION
            case EnemyStates.PASSIVE /*when eyeball.susLevel <= passiveSusMin*/:

                guardAnim.EnterPassiveAnim();

                stateText.text = stateMachine.ToString();

                SetAiSpeed(patrolSpeed);

                //Reseting alert related variables
                if (isAudioSourcePlaying == true)
                {
                    audioSource.Stop();

                    isAudioSourcePlaying = false;
                }

                //Checks to see if it is at specified distance for getting it's next waypoint
                if (Vector3.Distance(target, transform.position) <= waypointNextDistance)
                {
                    //Checks to see if the isWait bool is true or not
                    if (isPatrolWait == true)
                    {

                        if (patrolWaitTime > 0)
                        {
                            patrolWaitTime -= Time.fixedDeltaTime;

                        }
                        else if (patrolWaitTime <= 0)
                        {
                            //waitTime = waitTimeReset;
                            patrolWaitTime = Random.Range(patrolWaitMin, patrolWaitMax);

                            //Figure out why this function is being called twice
                            SetNextWaypoint();
                        }
                    }
                    else
                    {
                        SetNextWaypoint();
                    }
                }


                target = waypoints[waypointIndex].position;

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(target);

                //Less confusing when showing the target on the debug canvas
                targetText.text = $"{waypoints[waypointIndex]}";

                FaceTarget(target);

                //Exit condition
                //Checking to see if the player is visible
                if (eyeball.susLevel > passiveSusMax)
                {
                    // PASSIVE >>>> SUSPICIOUS
                    stateMachine = EnemyStates.WARY;
                }


                //Psuedo Code for when a wary worth action occurs
                //if (something that would make the guard wary happens == true)
                //{
                //    stateMachine = EnemyStates.WARY;
                //}

                break;
            #endregion Passive Behavior

            #region Wary Behavior
            case EnemyStates.WARY:

                guardAnim.EnterPassiveAnim();

                stateText.text = stateMachine.ToString();

                SetAiSpeed(warySpeed);

                //Reseting alert related variables
                if (isAudioSourcePlaying == true)
                {
                    audioSource.Stop();

                    isAudioSourcePlaying = false;
                }

                //Checks to see if it is at specified distance for getting it's next waypoint
                if (Vector3.Distance(target, transform.position) <= waypointNextDistance)
                {
                    //Checks to see if the isWait bool is true or not
                    if (isPatrolWait == true)
                    {

                        if (patrolWaitTime > 0)
                        {
                            patrolWaitTime -= Time.fixedDeltaTime;

                        }
                        else if (patrolWaitTime <= 0)
                        {
                            //waitTime = waitTimeReset;
                            patrolWaitTime = Random.Range(patrolWaitMin, patrolWaitMax);

                            //Figure out why this function is being called twice
                            SetNextWaypoint();
                        }
                    }
                    else
                    {
                        SetNextWaypoint();
                    }
                }

                target = waypoints[waypointIndex].position;

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(target);

                //Less confusing when showing the target on the debug canvas
                targetText.text = $"{waypoints[waypointIndex]}";

                FaceTarget(target);

                //Exit condition
                //Checking to see if the player is visible
                if (eyeball.susLevel < warySusMin)
                {
                    // PASSIVE >>>> SUSPICIOUS
                    stateMachine = EnemyStates.PASSIVE;
                }

                if (eyeball.susLevel > warySusMax)
                {
                    var AmConfuse = Instantiate(confusedVFX, transform.position, transform.rotation);

                    AmConfuse.transform.parent = gameObject.transform;

                    // PASSIVE >>>> SUSPICIOUS
                    stateMachine = EnemyStates.SUSPICIOUS;
                }

                break;
            #endregion Wary Behavior

            #region Suspicious Behavior
            case EnemyStates.SUSPICIOUS:


                float searchLocCheck = .5f;

                guardAnim.EnterSusAnim();

                stateText.text = stateMachine.ToString();

                SetAiSpeed(susSpeed);

                #region New Behavior Notes
                //---------------------------------//
                //New Sus Guard Behaviour:
                //-When eyeball suslevel reaches threshold:
                //  -Guard stops
                //  - Goes in/ faces random directinos, enters searching anim, exits, goes in/ faces another random direction and repeats behaviour

                //  -Repeats these actions until eyeball.suslevel reaches 0 again

                //Add timer here

                //Add timer reset & function call at the end of timer
                //---------------------------------//

                #endregion New Behavior Notes

                //To Do: Add a distance buffer for entering the search animation
                //  - Might take a bit of refacotring


                //Buffer timer
                //Find a more efficient way of doing this
                if (oneTimeUseTimer > 0)
                {
                    oneTimeUseTimer -= Time.fixedDeltaTime;
                }
                else if (oneTimeUseTimer <= 0)
                {
                    //Think of better names for these variables, they are confusing as shit
                    if (randWaitTime > 0)
                    {
                        randWaitTime -= Time.fixedDeltaTime;

                        guardAnim.ExitSearchingAnim();
                    }
                    else if (randWaitTime <= 0)
                    {

                        randWaitTime = Random.Range(randWaitMin, randWaitMax);

                        targetText.text = ($"{target}");

                        target = GenerateRandomPoint();

                        FaceTarget(target);

                        guardAnim.EnterSearchingAnim();
                    }
                }

                SetAIDestination(target);

                #region Exit Conditions
                //Exit Condition
                if (eyeball.susLevel < sussySusMin)
                {
                    //SUSPICIOUS >> WARY
                    stateMachine = EnemyStates.WARY;

                    oneTimeUseTimer = oneTimeUseTimerReset;
                }

                if (eyeball.susLevel > sussySusMax)
                {
                    oneTimeUseTimer = oneTimeUseTimerReset;

                    guardAnim.ExitSearchingAnim();

                    //the cool lil MGS thing
                    var MGSsurprise = Instantiate(surpriseVFX, transform.position, transform.rotation);

                    MGSsurprise.transform.parent = gameObject.transform;

                    //SUSPICIOUS >> HOSTILE
                    stateMachine = EnemyStates.HOSTILE;
                }
                #endregion Exit Conditions

                break;
            #endregion Suspicious Behavior

            #region Hostile Behavior
            case EnemyStates.HOSTILE:

                guardAnim.EnterHostileAnim();

                stateText.text = stateMachine.ToString();

                SetAiSpeed(hostileSpeed);

                //Checking if the player is within the AI's look radius
                if (eyeball.canCurrentlySeePlayer == true || eyeball.susLevel > hostileSusMin)
                {

                    SetAiSpeed(hostileSpeed);

                    target = eyeball.lastKnownLocation;

                    targetText.text = "Player";

                    //transform.position is being used because you cannot use Vector3 data when Transform is being called
                    SetAIDestination(target);

                    if (Vector3.Distance(player.transform.position, transform.position) < attackRadius)
                    {
                        guardAnim.EnterAttackAnim();
                        player.GetComponent<PlayerMovement>().IsStunned = true;
                        player.transform.position = playerCaptureTeleportLoc.transform.position;
                        SetAIDestination(this.transform.position);


                        //player.transform.SetParent(playerCaptureTeleportLoc.transform, false);

                        //HOSTILE >> ATTACK
                        stateMachine = EnemyStates.ATTACK;
                    }

                    //Playing Alert Audio
                    if (isAudioSourcePlaying == false)
                    {
                        audioSource.Play();

                        isAudioSourcePlaying = true;
                    }
                }

                //Exit Condition
                else if (eyeball.canCurrentlySeePlayer == false || eyeball.susLevel < hostileSusMin)
                {
                    var AmConfuse = Instantiate(confusedVFX, transform.position, transform.rotation);

                    AmConfuse.transform.parent = gameObject.transform;

                    //HOSTILE >> SUSPICIOUS
                    stateMachine = EnemyStates.SUSPICIOUS;
                }

                FaceTarget(target);

                break;
            #endregion Hostile Behavior

            #region Attack Behavior
            //AI Attack state
            case EnemyStates.ATTACK:

                stateText.text = stateMachine.ToString();

                SetAiSpeed(attackSpeed);

                #region Exit Condition(s)
                
                if (isStunned == true)
                {
                    //player.transform.parent = null;
                    guardAnim.ExitAttackAnim();

                    guardAnim.EnterStunAnim();

                    // ATTACK >>  STUNNED
                    stateMachine = EnemyStates.STUNNED;

                    player.GetComponent<PlayerMovement>().IsStunned = false;
                    player.transform.position = playerReleaseTeleportLoc.transform.position;

                }
                else if (Vector3.Distance(target, transform.position) > attackRadius && !isStunned)
                {
                    //the cool lil MGS thing
                    var MGSsurprise = Instantiate(surpriseVFX, transform.position, transform.rotation);

                    MGSsurprise.transform.parent = gameObject.transform;

                    // ATTACK >> HOSTILE
                    stateMachine = EnemyStates.HOSTILE;
                }


                #endregion Exit Condition(s)

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
                stunTime -= Time.deltaTime;


                //Exit Condition
                if (stunTime <= 0)
                {
                    guardAnim.ExitStunAnim();
                    isStunned = false;

                    eyeball.susLevel = sussySusMax;

                    //the cool lil MGS thing
                    var MGSsurprise = Instantiate(surpriseVFX, transform.position, transform.rotation);

                    MGSsurprise.transform.parent = gameObject.transform;

                    //STUNNED >>>> PREVIOUS STATE (SUSPICIOS for now)
                    stateMachine = EnemyStates.HOSTILE;

                    //after changing states, the stun time returns to the initially recorded time
                    stunTime = stunTimeReset;
                }
                break;
            #endregion Stunned Behavior

            #region Default Behavior / Bug Catcher
            default:

                stateText.text = ("ERROR");

                targetText.text = $"Target = {targetText}";

                FaceTarget(target);

                break;
            #endregion Default Behavior / Bug Catcher

        }//End State Machine
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
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = true;

        SetAiSpeed(patrolSpeed);
        
        //FIX THIS WHEN TELLING PEOPLE ITS FINE TO FUCK WITH GUARD
        stateMachine = EnemyStates.PASSIVE;

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

        //loseText.text = "";

        stunTimeReset = stunTime;

        path = new NavMeshPath();

        if (guardAnim == null)
        {
            guardAnim = GetComponent<GuardAnimatorScript>();
        }

        oneTimeUseTimerReset = oneTimeUseTimer;

    }//End Init


    //---------------------------------//
    //Alert's the guard
    public void Alert(Vector3 alertLoc)
    {
        eyeball.susLevel = 10;

        stateMachine = EnemyStates.HOSTILE;

        if (eyeball.canCurrentlySeePlayer == false)
        {
            //target = alertLoc;
            eyeball.lastKnownLocation = alertLoc;
        }
        else
        {
            target = eyeball.lastKnownLocation;
        }
    }//End Alert


    //---------------------------------//
    // Function for facing the player when the AI is withing stopping distance of the player
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

        Quaternion lookRotation = Quaternion.identity;
        if (direction.x != 0 && direction.z != 0)
        {
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.1f);
    }//End FaceTarget


    //---------------------------------//
    // Sets the AI speed
    // Needs to be reworked / improved
    void SetAiSpeed(float speed)
    {
        agent.speed = Mathf.Lerp(agent.speed, speed, 1);//End SetSpeed

        guardAnim.SetAgentSpeed(speed);
    }//End SetAiSpeed


    //---------------------------------//
    // Function for setting AI destination
    void SetAIDestination(Vector3 point) => agent.SetDestination(point); //End SetAIDestination


    //---------------------------------//
    // Finding and eating donuts
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BaitItemScript>() != null)
        {
            Destroy(other.gameObject);
        }
    }//End OnTriggerEnter

    //---------------------------------//
    //
    private Vector3 GenerateRandomPoint()
    {
        //Generates the initial random point
        Vector3 randpoint = Random.insideUnitSphere * randPointRad;

        //Returns a bool
        //Tests the randomly generated point to see if it can be reached.
        //Second portion tests the path to the genreated point and to see if it's possible to reach that point
        if (NavMesh.SamplePosition(randpoint + transform.position, out NavMeshHit hit, randPointRad, 1) && NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path))
        {
            searchLoc = hit.position;
            return searchLoc;
        }
        else
        {
            print("Point or Path is invalid");
            return transform.position;
        }
    }//End GenerateRandomPoint


    //---------------------------------//
    //Draws shapes only visible in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(transform.position, randPointRad);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(searchLoc, .5f);
    }//End OnDrawGizmos


    //---------------------------------//
    // Raises the security level for the area
    void RaiseSecurityLevel()
    {
    }//End RaiseSecurityLevel

    public IEnumerator IBreakFreeDelay()
    {
        yield return new WaitForSeconds(2);
    }

    #endregion AI Functions
}