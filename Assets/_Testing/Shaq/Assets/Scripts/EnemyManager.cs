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

    public Coroutine coroutine;

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

    [Space(20)]

    [Tooltip("References the state text (displays the state the guard is in)")]
    [SerializeField] private Text stateText;

    [Tooltip("References the target text (displays the guard's current target)")]
    [SerializeField] private Text targetText;

    //---------------------------------------------------------------------------------------------------//

    [Header("Minimum Sus Levels for States")]

    [Space(20)]

    //Minimum Suspicion level to enter this state
    [HideInInspector] private float passiveSusMax = 3;

    //Minimum Suspicion level to enter this state
    [HideInInspector] public float warySusMin = 3.1f;

    [HideInInspector] private float warySusMax = 4;

    //Minimum Suspicion level to enter this state
    [HideInInspector] private float sussySusMin = 4.1f;

    [HideInInspector] private float sussySusMax = 5;

    //Minimum Suspicion level to enter this state
    [HideInInspector] private float hostileSusMin = 5.1f;

    //---------------------------------------------------------------------------------------------------//

    [Header("Guard Movement Speeds")]

    [Space(20)]

    [Tooltip("The speed that the AI moves at in the PASSIVE state")]
    [SerializeField] [Range(0, 10)] public float passiveSpeed = 1f;

    [Tooltip("The speed that the AI moves at in the WARY state")]
    [SerializeField] [Range(0, 10)] public float warySpeed = 1.5f;

    [Tooltip("The speed that the AI moves at in the SUSPICIOS state")]
    [SerializeField] [Range(0, 10)] public float susSpeed = 1f;

    [Tooltip("The speed that the AI moves at in the STUNNED state")]
    [SerializeField] [Range(0, 10)] public float stunSpeed = 0f;

    [Tooltip("The speed that the AI moves at in the HOSTILE state")]
    [SerializeField] [Range(0, 10)] public float hostileSpeed = 4f;

    [Tooltip("The speed that the AI moves at in the REPORT state")]
    [SerializeField] [Range(0, 10)] private float reportSpeed = 3.5f;

    [Tooltip("The speed that the AI moves at in the ATTACK state")]
    [SerializeField] [Range(0, 10)] public float attackSpeed = 0f;

    //---------------------------------------------------------------------------------------------------//
    [Header("Patrol Wait Variables")]

    [Space(20)]

    [Tooltip("When enabled, the guard will wait when it reaches it's 'waypointNextDistance'")]
    [SerializeField] private bool isPatrolWait;

    [Tooltip("The amount of time that the guard waits when 'isWait' is enabled")]
    [SerializeField] public float patrolWaitTime;

    [Tooltip("The minimum generated value for the wait time")]
    [SerializeField] [Range(1, 3)] private float patrolWaitMin = 1f;

    [Tooltip("The maximum generated value for the wait time")]
    [SerializeField] [Range(3, 5)] private float patrolWaitMax = 5f;


    //---------------------------------------------------------------------------------------------------//
    [Header("Suspicious State Variables")]

    [Space(20)]

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

    [Space(20)]

    [SerializeField] private GuardAudio guardAudioScript;

    //---------------------------------------------------------------------------------------------------//

    [Header("Debug / Testing Variables")]

    [Space(20)]

    [HideInInspector] public bool isStunned;

    //Variable may need to be renamed in the future based on further implementations with Charlie
    [Tooltip("Duration of the guard's Stun state duration")]
    [SerializeField] private float stunTime;

    [Tooltip("")]
    [HideInInspector] private float stunTimeReset;

    //---------------------------------------------------------------------------------------------------//

    [Header("Taser Variables")]

    [Space(20)]
    
    [Tooltip("The radius in which the guard tases the player")]
    [SerializeField] [Range(0, 10)] private float taserShotRadius;

    [Tooltip("Basically the fire rate for the guard's taser")]
    [SerializeField] [Range (0f, 10f)] private float fireRateReset;
    
    [Tooltip("References the taser prefab for the guard to spawn")]
    [SerializeField] private GameObject taserProjectile;

    [Tooltip("References the spawn location of the taser prefeab")]
    [SerializeField] private GameObject taserSpawnLoc;

    [Tooltip ("The higher the float the lower the accuracy of the guard's taser")]
    [SerializeField] [Range (0f, 1f)] public float accuracy = .3f;
    
    //---------------------------------------------------------------------------------------------------//

    [Header("Misc. Variables")]

    [Space(20)]


    [Tooltip("The distance the guards is from it's waypoint before it get's it's next waypoint")]
    [SerializeField] private float waypointNextDistance = 2f;

    private float oneTimeUseTimer = 2f;

    private float oneTimeUseTimerReset;

    //private bool surpriseVFXBoolCheck;

    private float eyeballSightRangeRecord;

    //Delete this in the future
    //private System.Threading.Timer timer;

    //---------------------------------------------------------------------------------------------------//

    //Extremely temporary timer variables

    [Header("Dev variables")]

    [Space(20)]

    [Tooltip("Guard's stopping distance from the security station")]
    [SerializeField] private float stoppingDistance = 2f;

    private bool SussyWaypointMade;

    [HideInInspector] private float fireRate;

    [SerializeField] PlayerMovement playerMovenemtRef;

    private bool ceaseFire = false;

    [SerializeField] public int floorNumber;

    [SerializeField] private GameObject playerVisTarget;

    #endregion

    #region Enumerations

    #region AI State Machine

    public enum EnemyStates
    {
        PASSIVE,
        WARY,
        SUSPICIOUS,
        HOSTILE,
        REPORT,
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
                //Probably going to get rid of this
                print("Current cycling method is random");

                break;

            default:
                print("Cycling method not found \a");
                break;
        }
    }
    #endregion Waypoints Functions

    #region Coroutines

    //Intended functionality:
    //  - Feed in the duration of the timer, when the duration is up the code proceeds to break out of the coroutine, hopefully stopping it. Something checks to see if the coroutine has been stopped, in which case an actino will proceed that
    IEnumerator ITimer(float setTime)
    {
        //Showing that the coroutine's timer is done
        yield return new WaitForSeconds(setTime);

        Debug.Log("Coroutine ITimer is over");

        //Functions in the same fashion as StopCoroutine()
        yield break;
    }

    #endregion Coroutines

    #region Methods

    //---------------------------------//
    // Called on Awake and initializes everything that is finalized and needs to be done at awake
    public void Init()
    {
        #region Null Checks
        //Checks to see if there is no value for the player object reference
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }

        if (guardAnim == null)
        {
            guardAnim = GetComponent<GuardAnimatorScript>();
        }

        if (securityStationObjRef == null)
        {
            securityStationObjRef = GameObject.Find("Suspicion Manager");
        }

        if (playerMovenemtRef == null)
        {
            playerMovenemtRef = player.GetComponent<PlayerMovement>();
        }

        if (guardAudioScript == null)
        {
            guardAudioScript = GetComponentInChildren < GuardAudio>();
        }

        if (playerVisTarget == null)
        {
            playerVisTarget = GameObject.Find("VisionTarget");
        }
        #endregion Null Checks

        //Stores the user generated random direction time
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = true;

        //Starts the guard in the Passive State
        stateChange(EnemyStates.PASSIVE);

        SetAiSpeed(passiveSpeed);

        target = waypoints[waypointIndex].position;

        FaceTarget(target);

        path = new NavMeshPath();     

        //Stores the user generated stun time
        stunTimeReset = stunTime;

        oneTimeUseTimerReset = oneTimeUseTimer;

        eyeballSightRangeRecord = eyeball.sightRange;

        fireRate = fireRateReset;

        securityStations = new List<GameObject>(GameObject.FindGameObjectsWithTag("SecurityStation"));

        NearestStation();
    }//End Init


    public void stateChange(EnemyStates enemyStates)
    {
        stateMachine = enemyStates;
    }


    //---------------------------------//
    // Updates the debug text above the guard's head
    public void UpdateDebugText()
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
    public GameObject NearestStation()
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
    public GameObject GameObjectContructor(string objName, Vector3 objSpawnLoc)
    {
        GameObject go1 = new GameObject();
        go1.name = objName;
        go1.transform.position = objSpawnLoc;

        return go1;
    }//End GameObjectConstructor

    //---------------------------------//
    // Alert's the guard
    public void Alert(Vector3 alertLoc)
    {
        if (stateMachine != EnemyStates.RANGEDATTACK && stateMachine != EnemyStates.HOSTILE)
        {
            eyeball.susLevel = 5.5f;

            stateMachine = EnemyStates.HOSTILE;

            target = alertLoc;

            eyeball.lastKnownLocation = alertLoc;

            agent.SetDestination(alertLoc);

            Debug.Log("Alert has been called");
        }
    }//End Alert


    //---------------------------------//
    // Method for facing the player when the AI is withing stopping distance of the player
    public void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

        Quaternion lookRotation = Quaternion.identity;
        if (direction.x != 0 && direction.z != 0)
        {
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }

        //The float at the end is arbitrarily high so that the guard properly faces the player / target when stationary or making a tight corner
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * Mathf.Infinity);
    }//End FaceTarget


    //---------------------------------//
    // Changes the guard's speed and destination in one go, just me being lazy to be honest [Come back and change this to be more professional before Kevin & Patrick comb through the code]
    public void SetSpeedAndDest(float speed, Vector3 point)
    {
        SetAiSpeed(speed);

        SetAIDestination(point);
    }//End SetSpeedAndDest


    //---------------------------------//
    // Sets the AI speed
    // Needs to be reworked / improved
    public void SetAiSpeed(float speed)
    {
        agent.speed = Mathf.Lerp(agent.speed, speed, 1);//End SetSpeed

        guardAnim.SetAgentSpeed(speed);
    }//End SetAiSpeed


    //---------------------------------//
    // Function for setting AI destination
    public void SetAIDestination(Vector3 point) => agent.SetDestination(point);//End SetAIDestination


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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, randPointRad);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, taserShotRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(searchLoc, .5f);

#if UNITY_EDITOR
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(target, 0.75f);
#endif

    }//End OnDrawGizmosSelected

    public IEnumerator IBreakFreeDelay()
    {
        yield return new WaitForSeconds(2);
    }

    #endregion Methods

    #region Awake
    //---------------------------------//
    //  Using Awake() instead of Start() so that when spawning is functional, the AI won't break
    void Awake()
    {
        Init();
    }//End Awake
    #endregion

    #region Update
    //Method called every frame
    void Update()
    {
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

                target = waypoints[waypointIndex].position;

                SetAiSpeed(passiveSpeed);

                //Checks to see if it is at specified distance for getting it's next waypoint
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

                //Checks to see if it is at specified distance for getting it's next waypoint
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
                    //var AmConfuse = Instantiate(confusedVFX, transform.position, transform.rotation);

                    //AmConfuse.transform.parent = gameObject.transform;

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

                ////Records and adds the player's last known location as a part of the waypoints list for patrollling.
                //if (SussyWaypointMade == false && eyeball.canCurrentlySeePlayer == true)
                //{
                //    waypoints.Add(GameObjectContructor("SussyPatrolLoc", transform.position).transform);

                //    GameObject.Find("SussyPatrolLoc").transform.position = eyeball.lastKnownLocation;

                //    SussyWaypointMade = true;
                //}

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

                //Better off staying here after the target var has been messed around with accordingly
                SetSpeedAndDest(susSpeed, target);

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

                    //var MGSsurprise = Instantiate(surpriseVFX, transform.position, transform.rotation);

                    //MGSsurprise.transform.parent = gameObject.transform;

                    //SUSPICIOUS >> HOSTILE
                    stateMachine = EnemyStates.HOSTILE;
                }
                #endregion Exit Conditions

                break;

            #endregion Suspicious Behavior

            #region Hostile Behavior
            //State for the guard to chase the player in
            case EnemyStates.HOSTILE:

                //Animation Change
                guardAnim.EnterHostileAnim();

                SetAiSpeed(hostileSpeed);

                FaceTarget(target);

                SetAIDestination(target);
                
                //Conditionds needed for ranged attack / taser
                if (eyeball.canCurrentlySeePlayer == true && agent.remainingDistance < taserShotRadius)
                {
                    //HOSTILE >> RANGED ATTACK
                    stateMachine = EnemyStates.RANGEDATTACK;
                }

                //Exit Conditions
                else if (eyeball.canCurrentlySeePlayer == false || eyeball.susLevel < hostileSusMin)
                {
                    //var AmConfuse = Instantiate(confusedVFX, transform.position, transform.rotation);

                    //AmConfuse.transform.parent = gameObject.transform;

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

                SetSpeedAndDest(reportSpeed, target);

                if (Vector3.Distance(transform.position, target) <= stoppingDistance)
                {
                    //print("DOOR STUCK");

                    NearestStation().GetComponent<SuspicionManager>().DummyMethod();

                    stateMachine = EnemyStates.WARY;
                }

                break;
            #endregion Report Behaviour

            #region Ranged Attack Behavior
            case EnemyStates.RANGEDATTACK:
                //Add secondary section to this state that changes the guard's behaviour from run / stop & gun to run & gun

                SetAiSpeed(0);

                target = eyeball.lastKnownLocation;

                FaceTarget(target);

                guardAnim.EnterShoot();

                SetAIDestination(target);

                //Eventually move this to the player as an event (make a listener / Unity event for this)
                //In the future make a better solution for the time scale, this is here because Patrick's superior intelligence saved your ass
                if (playerMovenemtRef.IsStunned == true || Time.timeScale != 1)
                {
                    ceaseFire = true;
                }
                else
                {
                    ceaseFire = false;
                }

                if (ceaseFire == false)
                {
                    if (fireRate > 0)
                    {
                        fireRate -= Time.fixedDeltaTime;
                    }
                    else if (fireRate < 0)
                    {
                        fireRate = fireRateReset;

                        var taserPrefab = Instantiate(taserProjectile, taserSpawnLoc.transform.position, transform.rotation);

                        taserPrefab.GetComponent<TaserManager>().accuracy = accuracy;

                        taserPrefab.transform.LookAt(playerVisTarget.transform);

                        taserPrefab.GetComponent<TaserManager>().Init();
                    }
                }

                //Exit Condition(s)
                if (eyeball.canCurrentlySeePlayer == false || agent.remainingDistance > taserShotRadius)   
                {
                    //Debug.Log("Target is outside of firing range");

                    //RANGED ATTACK >> HOSTILE
                    stateMachine = EnemyStates.HOSTILE;
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
}