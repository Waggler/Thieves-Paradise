using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;

using Unity.Profiling;

/*To Do:
    - Look into what it takes to have a node approach to AI behaviour
        - Basically what was done with Zork where there's multiple scripts being used with class inheritance (how monobehaviour works)
    
    - Replace any instances of Vector3.Distance with NavMeshAgent.remainingDistance / agent.remainingDistance where it is being used to calculate the guard's distance from a target
        - Do not do this when it is being used to calculate the distance from an unrelated object.

*/


public class EnemyManager : MonoBehaviour
{
    //Unity Profiler Goofery
    //static readonly Unity.Profiling.ProfilerMarker s_MyProfilerMarker = new Unity.Profiling.ProfilerMarker("Guard Profile Marker *Shaq Made This");
    //public UnityEvent EVENT_OnNearestStation;



    #region Enumerations

    #region AI State Machine

    public enum EnemyStates
    {
        PASSIVE,
        WARY,
        SUSPICIOUS,
        HOSTILE,
        REPORT,
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
        Reverse,
        Random
    }

    [Header("Waypoint Cycling")]

    [SerializeField] CycleMethods waypointMethod;

    #endregion AI Cycle Methods

    #endregion Enumerations

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

            case CycleMethods.Random:
                print("Current cycling method is random");

                break;

            default:
                print("Cycling method not found \a");
                break;
        }
    }
    #endregion Waypoints Functions

    #region EventHandlers (?)

    #endregion EventHandlers (?)

    #region Coroutines
    
    //---------------------------------//
    // Handles the firerate of the taser
    //  - Also acts as the testing ground for adding coroutines to the EnemyManager
    private IEnumerator ITaserFirerate()
    {
        //Spawn projectile in the forward direction of the guard
        //later on change the spawn position to be the tip of the taser(assuming conditions and animations are already in place)
        yield return new WaitForSecondsRealtime(5);

        var taserPrefab = Instantiate(taserProjectile, taserSpawnLoc.transform.position, transform.rotation);

        print("''I need every bad bitch up in Equinox. I need to know right now if you're a freak or not.''  Bob Saget");

    }//End ITaserFireRate

    #endregion Coroutines

    #region Variables

    //---------------------------------------------------------------------------------------------------//


    [Tooltip("The guard's target")]
    [HideInInspector] public Vector3 target;

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
    [SerializeField] public EyeballScript eyeball;

    [Tooltip("References the guard's animator script")]
    [SerializeField] GuardAnimatorScript guardAnim;

    [SerializeField] private GameObject surpriseVFX;

    [SerializeField] private GameObject confusedVFX;

    [Tooltip("Object reference to the security station / suspicion manager")]
    [SerializeField] private GameObject securityStationObjRef;

    [Tooltip("Script reference to the security station / suspicion manager")]
    [SerializeField] private SuspicionManager securityStationScriptRef;

    [Tooltip("List of Security Stations in the level")]
    [SerializeField] private List<GameObject> securityStations;

    //---------------------------------------------------------------------------------------------------//

    [Header("Diagnostic Text")]

    [Tooltip("References the state text (displays the state the guard is in)")]
    [SerializeField] private Text stateText;

    [Tooltip("References the target text (displays the guard's current target)")]
    [SerializeField] private Text targetText;

    //---------------------------------------------------------------------------------------------------//

    [Header("Minimum Sus Levels for States")]

    [Tooltip("Minimum Suspicion level to enter this state")]
    //Implied Min Value of 0
    [SerializeField] private float passiveSusMax = 3;

    [Tooltip("Minimum Suspicion level to enter this state")]
    [SerializeField] public float warySusMin = 3.1f;

    [SerializeField] private float warySusMax = 4;

    [Tooltip("Minimum Suspicion level to enter this state")]
    [SerializeField] private float sussySusMin = 4.1f;

    [SerializeField] private float sussySusMax = 5;

    [Tooltip("Minimum Suspicion level to enter this state")]
    //Implied Max Value of eyeball.susLevel max
    [SerializeField] private float hostileSusMin = 5.1f;

    //---------------------------------------------------------------------------------------------------//

    [Header("Guard Movement Speeds")]

    [Tooltip("The speed that the AI moves at in the PASSIVE state")]
    [SerializeField] [Range(0, 5)] public float passiveSpeed = 1f;

    [Tooltip("The speed that the AI moves at in the WARY state")]
    [SerializeField] [Range(0, 5)] public float warySpeed = 1.5f;

    [Tooltip("The speed that the AI moves at in the SUSPICIOS state")]
    [SerializeField] [Range(0, 5)] public float susSpeed = 1f;

    [Tooltip("The speed that the AI moves at in the STUNNED state")]
    [SerializeField] [Range(0, 5)] public float stunSpeed = 0f;

    [Tooltip("The speed that the AI moves at in the HOSTILE state")]
    [SerializeField] [Range(0, 5)] public float hostileSpeed = 4f;

    [Tooltip("The speed that the AI moves at in the REPORT state")]
    [SerializeField] [Range(0, 5)] private float reportSpeed = 3.5f;

    [Tooltip("The speed that the AI moves at in the ATTACK state")]
    [SerializeField] [Range(0, 5)] public float attackSpeed = 0f;

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
    [SerializeField] [Range(1, 3)] private float randPointRad = 1f;

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
    [SerializeField] [Range(0, 50)] private float guardKnockbackForce;

    [Tooltip("Duration of the guard's Attack state duration")]
    [SerializeField] private float attackTime;
    private float attackTimeReset;

    //---------------------------------------------------------------------------------------------------//

    [Header("Misc. Variables")]

    [Tooltip("The distance the guard needs to be from the target/player before it attacks them")]
    [SerializeField] [Range(0, 2)] private float attackRadius = 10f;

    [SerializeField] [Range(3, 10)] private float taserRadius;

    [Tooltip("The distance the guards is from it's waypoint before it get's it's next waypoint")]
    [SerializeField] private float waypointNextDistance = 2f;

    [SerializeField] private GameObject playerCaptureTeleportLoc;

    [SerializeField] private GameObject playerReleaseTeleportLoc;

    private float oneTimeUseTimer = 2f;

    private float oneTimeUseTimerReset;

    //private bool surpriseVFXBoolCheck;

    private float eyeballSightRangeRecord;

    //Delete this in the future
    //private System.Threading.Timer timer;

    //---------------------------------------------------------------------------------------------------//
    
    //Extremely temporary timer variables

    [Header("Extremely temporary timer variables")]

    [SerializeField] [Range(0, 10)] private float tempTaserTimer;

    [Tooltip("References the taser prefab for the guard to spawn")]
    [SerializeField] private GameObject taserProjectile;

    [Tooltip("References the spawn location of the taser prefeab")]
    [SerializeField] private GameObject taserSpawnLoc;

    [Tooltip("Guard's stopping distance from the security station")]
    [SerializeField] private float stoppingDistance = 2f;
     
    private bool SussyWaypointMade;



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
        //Replace with NavMeshAgent.remainingDistance
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position + Vector3.up);

        //Setting up a navmesh agent stoppingDistance that only takes place in the REPORT state (SUPER temporary)
        if (stateMachine == EnemyStates.REPORT)
        {
            agent.stoppingDistance = stoppingDistance;
        }
        else
        {
            agent.stoppingDistance = 0;
        }

        UpdateDebugText();

        //At all times be sure that there is a condition to at least ENTER and EXIT the state that the AI is being put into
        switch (stateMachine)
        {
            #region Passive Behavior
            //Patrol state for guard
            case EnemyStates.PASSIVE:

                guardAnim.EnterPassiveAnim();

                //stateText.text = stateMachine.ToString();

                target = waypoints[waypointIndex].position;

                SetAiSpeed(passiveSpeed);

                //Reseting alert related variables
                if (isAudioSourcePlaying == true)
                {
                    audioSource.Stop();

                    isAudioSourcePlaying = false;
                }

                //Checks to see if it is at specified distance for getting it's next waypoint
                //if (Vector3.Distance(target, transform.position) <= waypointNextDistance)
                if (agent.remainingDistance <= waypointNextDistance)
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

                //transform.position is being used because you cannot use Vector3 data when Transform is being called
                SetAIDestination(target);

                //Less confusing when showing the target on the debug canvas
                //targetText.text = $"{waypoints[waypointIndex]}";

                FaceTarget(target);

                //Exit condition
                //Checking to see if the player is visible
                if (eyeball.susLevel > passiveSusMax)
                {
                    // PASSIVE >>>> SUSPICIOUS
                    stateMachine = EnemyStates.WARY;
                }

                break;
            #endregion Passive Behavior

            #region Wary Behavior
                //Identical to Passive state
            case EnemyStates.WARY:

                guardAnim.EnterPassiveAnim();

                SetAiSpeed(warySpeed);

                FaceTarget(target);

                //Reseting alert related variables
                if (isAudioSourcePlaying == true)
                {
                    audioSource.Stop();

                    isAudioSourcePlaying = false;
                }

                //Checks to see if it is at specified distance for getting it's next waypoint
                //if (Vector3.Distance(target, transform.position) <= waypointNextDistance)
                if (agent.remainingDistance <= waypointNextDistance)
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
            //Finding random points in a set radius for the guard to go to
            //Also records the player's last known location and appends it to the waypoints list of the guard instance
            case EnemyStates.SUSPICIOUS:

                guardAnim.EnterSusAnim();

                SetAiSpeed(susSpeed);

                //Records and adds the player's last known location as a part of the waypoints list for patrollling.
                if (SussyWaypointMade == false && eyeball.canCurrentlySeePlayer == true)
                {
                    waypoints.Add(GameObjectContructor("SussyPatrolLoc", transform.position).transform);

                    GameObject.Find("SussyPatrolLoc").transform.position = eyeball.lastKnownLocation;

                    SussyWaypointMade = true;
                }


                //Used to send the guard to random locations in a set radius, meant to emulate confusion from the unit
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
                    guardAnim.ExitSearchingAnim();

                    oneTimeUseTimer = oneTimeUseTimerReset;

                    //SUSPICIOUS >> WARY
                    stateMachine = EnemyStates.WARY;
                }

                if (eyeball.susLevel > sussySusMax)
                {
                    guardAnim.ExitSearchingAnim();

                    oneTimeUseTimer = oneTimeUseTimerReset;

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
            //State for the guard to chase the player in
            case EnemyStates.HOSTILE:

                guardAnim.EnterHostileAnim();

                SetAiSpeed(hostileSpeed);

                FaceTarget(target);

                //Checking if the player can be seen by the guard
                if (eyeball.canCurrentlySeePlayer == true || eyeball.susLevel > hostileSusMin)
                {

                    SetAiSpeed(hostileSpeed);

                    target = eyeball.lastKnownLocation;

                    //transform.position is being used because you cannot use Vector3 data when Transform is being called
                    SetAIDestination(target);



                    //Soon going to hell along with the ATTACK behaviour / state
                    if (Vector3.Distance(player.transform.position, transform.position) < attackRadius)
                    {
                        guardAnim.EnterAttackAnim();
                        player.GetComponent<PlayerMovement>().IsStunned = true;
                        player.transform.position = playerCaptureTeleportLoc.transform.position;
                        SetAIDestination(this.transform.position);

                        //HOSTILE >> ATTACK
                        stateMachine = EnemyStates.ATTACK;
                    }



                    //Conditionds needed for ranged attack / taser
                    else if (eyeball.canCurrentlySeePlayer == true && agent.CalculatePath(target, agent.path) == false && distanceToPlayer <= taserRadius)
                    {
                        //HOSTILE >> RANGED ATTACK
                        stateMachine = EnemyStates.RANGEDATTACK;
                    }

                    //Playing Alert Audio
                    if (isAudioSourcePlaying == false)
                    {
                        audioSource.Play();

                        isAudioSourcePlaying = true;
                    }
                }

                //Exit Conditions
                else if (eyeball.canCurrentlySeePlayer == false || eyeball.susLevel < hostileSusMin)
                {
                    var AmConfuse = Instantiate(confusedVFX, transform.position, transform.rotation);

                    AmConfuse.transform.parent = gameObject.transform;

                    //HOSTILE >> SUSPICIOUS
                    stateMachine = EnemyStates.SUSPICIOUS;
                }



                break;
            #endregion Hostile Behavior

            #region Report Behaviour
            case EnemyStates.REPORT:

                targetText.text = "Security Station";

                //Do a check to see if there is a valid path to this nearest security station as well
                target = NearestStation().transform.position;

                eyeball.susLevel = 3.5f;

                SetAiSpeed(reportSpeed);

                SetAIDestination(target);

                if (Vector3.Distance(transform.position, target) <= stoppingDistance)
                {
                    //print("DOOR STUCK");

                    NearestStation().GetComponent<SuspicionManager>().DummyMethod();

                    stateMachine = EnemyStates.WARY;
                }

                break;
            #endregion Report Behaviour


            //Getting sent to hell soon
            #region Attack Behavior
            //AI Attack state
            case EnemyStates.ATTACK:

                SetAiSpeed(attackSpeed);

                FaceTarget(target);

                //Exit Conditions
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
                break;
            #endregion Attack Behavior



            #region Ranged Attack Behavior
            case EnemyStates.RANGEDATTACK:

                SetAiSpeed(0);

                FaceTarget(target);

                StartCoroutine(ITaserFirerate());


                //Exit Condition(s)
                //Take another look at this for any improvements / optimiazations that could be made 02/16/1999
                if (eyeball.canCurrentlySeePlayer == false || agent.CalculatePath(target, agent.path) == true || distanceToPlayer >= taserRadius)
                {
                    //RANGED ATTACK >> HOSTILE
                    stateMachine = EnemyStates.HOSTILE;

                    StopCoroutine(ITaserFirerate());
                }
                break;
            #endregion Ranged Attack Behavior

            #region Stunned Behavior
            case EnemyStates.STUNNED:

                guardAnim.EnterStunAnim();

                SetAiSpeed(stunSpeed);

                SetAIDestination(target);

                eyeball.susLevel = 0;

                eyeball.sightRange = 0;

                stunTime -= Time.fixedDeltaTime;

                //Exit Condition
                if (stunTime <= 0)
                {
                    guardAnim.ExitStunAnim();
                    isStunned = false;

                    eyeball.susLevel = sussySusMax;

                    //the cool lil MGS thing
                    var MGSsurprise = Instantiate(surpriseVFX, transform.position, transform.rotation);
                    MGSsurprise.transform.parent = gameObject.transform;

                    //STUNNED >>>> PREVIOUS STATE (SUSPICIOUS for now)
                    stateMachine = EnemyStates.REPORT;

                    //after changing states, the stun time returns to the initially recorded time
                    stunTime = stunTimeReset;

                    eyeball.sightRange = eyeballSightRangeRecord;
                }
                break;
            #endregion Stunned Behavior

            #region Default Behavior / Bug Catcher
            default:

                //Probably fine to have this print once a frame since it would be a recognizable way to show that something is borked with the state machine
                print("Shitter's clogged");

                break;
            #endregion Default Behavior / Bug Catcher

        }//End State Machine
    }//End Update
    #endregion Update

    #endregion Awake & Update

    #region AI Methods

    //---------------------------------//
    // Called on Awake and initializes everything that is finalized and needs to be done at awake
    private void Init()
    {
        //FIX THIS WHEN TELLING PEOPLE ITS FINE TO FUCK WITH GUARD
        stateMachine = EnemyStates.PASSIVE;

        isAudioSourcePlaying = false;

        //Stores the user generated stun time
        stunTimeReset = stunTime;

        //Stores the user generated random direction time
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = true;

        SetAiSpeed(passiveSpeed);

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

        stunTimeReset = stunTime;

        path = new NavMeshPath();

        if (guardAnim == null)
        {
            guardAnim = GetComponent<GuardAnimatorScript>();
        }

        oneTimeUseTimerReset = oneTimeUseTimer;

        eyeballSightRangeRecord = eyeball.sightRange;

        if (securityStationObjRef == null)
        {
            securityStationObjRef = GameObject.Find("Suspicion Manager");
        }

        securityStations = new List<GameObject>(GameObject.FindGameObjectsWithTag("SecurityStation"));

        NearestStation();

    }//End Init


    //---------------------------------//
    // Updates the debug text above the guard's head
    private void UpdateDebugText()
    {
        string methodStateText;

        methodStateText = stateMachine.ToString();

        stateText.text = methodStateText;


        string methodTargetText;

        methodTargetText = target.ToString();

        targetText.text = methodTargetText;
    }//End UpdateDebugText


    //---------------------------------//
    // Finds nearest security station
    private GameObject NearestStation()
    {
        //Credit goes to Patrick for this code

        float currentDistance;
        float minDistance = Mathf.Infinity;

        GameObject closestStation = null;

        foreach (GameObject station in securityStations)
        {
            currentDistance = Vector3.Distance(station.transform.position, transform.position);

            if (currentDistance < minDistance)
            {
                minDistance = currentDistance;

                closestStation = station;
            }
        }
        return closestStation;
    }//End NearestStation


    //---------------------------------//
    // Constructs an empty game object with a transform component
    //  - The constructed object is intended to be used as a waypoint for the guard
    private GameObject GameObjectContructor(string objName, Vector3 objSpawnLoc)
    {
        GameObject go1 = new GameObject();
        go1.name = objName;
        go1.AddComponent<Transform>();
        go1.transform.position = objSpawnLoc;

        return go1;
    }//End GameObjectConstructor


    //---------------------------------//
    // Alert's the guard
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
    // Method for facing the player when the AI is withing stopping distance of the player
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

        Quaternion lookRotation = Quaternion.identity;
        if (direction.x != 0 && direction.z != 0)
        {
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }

        //The float at the end is arbitrarily high so that the guard properly faces the player / target when stationary or making a tight corner
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 6000f);
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
    void SetAIDestination(Vector3 point)
    {
        agent.SetDestination(point); //End SetAIDestination
    }


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
    // Generates a random point for the guard to go to
    private Vector3 GenerateRandomPoint()
    {
        //Generates the initial random point
        Vector3 randpoint = Random.insideUnitSphere * randPointRad;

        //Returns a bool
        //First portion tests the randomly generated point to see if it can be reached.
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
    // Draws shapes only visible in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(transform.position, randPointRad);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, taserRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(searchLoc, .5f);

#if UNITY_EDITOR
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(target, 0.75f);
#endif

    }//End OnDrawGizmos


    public IEnumerator IBreakFreeDelay()
    {
        yield return new WaitForSeconds(2);
    }

    #endregion AI Methods
}