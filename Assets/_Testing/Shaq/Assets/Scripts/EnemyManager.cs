using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;

using Unity.Profiling;

/*To Do:
    - Remove Wary state (?)

    - Add Melee attack state (May not need to be a whole state and can just be a reaction to the player being to close to the guard)
        - Regardless it needs to be a reaction done from the HOSTILE State

    - Add Delay between Passive and Suspicious states

*/


public class EnemyManager : MonoBehaviour
{
    //Unity Profiler Goofery

    //static readonly Unity.Profiling.ProfilerMarker s_MyProfilerMarker = new Unity.Profiling.ProfilerMarker("Guard Profile Marker *Shaq Made This");
    //public UnityEvent EVENT_OnNearestStation;

    [Header("Boss Check")]
    [Space(20)]
    [SerializeField] private bool isBoss = false;
    [Space(20)]

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

            default:
                print("Cycling method not found \a");
                break;
        }
    }
    #endregion Waypoints Functions

    #region Enumerations

    #region AI State Machine

    public enum EnemyStates
    {
        PASSIVE,
        SUSPICIOUS,
        HOSTILE,
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

    #region Coroutines

    IEnumerator ITimer(float time, bool timerBool)
    {
        //Acts as a sort of blocking call
        while (time > 0)
        {
            yield return new WaitForSeconds(1);

            time--;
        }

        timerBool = true;
        print("Timer is done");
    }

    #endregion Coroutines

    #region Variables

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

    [Tooltip("References the player object (automatically generated)")]
    [SerializeField] private GameObject player;

    [Tooltip("References the guard's eyeball prefab / object")]
    [SerializeField] public EyeballScript eyeball;

    [Tooltip("References the guard's animator script")]
    [SerializeField] public GuardAnimatorScript guardAnim;

    
    [SerializeField] private GameObject surpriseVFX;

    [SerializeField] private GameObject confusedVFX;

    [Tooltip("Actual taser model")]
    [SerializeField] private GameObject taserPrefab;

    [Tooltip("Object reference to the security station / suspicion manager")]
    [SerializeField] private GameObject securityStationObjRef;

    [Tooltip("Script reference to the security station / suspicion manager")]
    [SerializeField] private SuspicionManager securityStationScriptRef;

    private PlayerMovement playerMovenemtRef;


    [Tooltip("List of Security Stations in the level")]
    private List<GameObject> securityStations;

    [SerializeField] private Collider smackBox;

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

    [SerializeField] private int passiveSusMax = 4;

    [SerializeField] private int sussySusMax = 5;

    [SerializeField] private int hostileSusMax = 6;


    //---------------------------------------------------------------------------------------------------//

    [Header("Guard Movement Speeds")]

    [Space(20)]

    [Tooltip("The speed that the AI moves at in the PASSIVE state")]
    [SerializeField] [Range(0, 10)] public float passiveSpeed = 1f;

    //[Tooltip("The speed that the AI moves at in the WARY state")]
    //[SerializeField] [Range(0, 10)] public float warySpeed = 1.5f;

    [Tooltip("The speed that the AI moves at in the SUSPICIOS state")]
    [SerializeField] [Range(0, 10)] public float susSpeed = 1f;

    [Tooltip("The speed that the AI moves at in the STUNNED state")]
    [HideInInspector] [Range(0, 10)] public float stunSpeed = 0f;

    [Tooltip("The speed that the AI moves at in the HOSTILE state")]
    [SerializeField] [Range(0, 10)] public float hostileSpeed = 4f;

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
    [SerializeField] [Range(1, 2)] private float randWaitMin = 1f;

    [Tooltip("The maximum generated value for the wait time")]
    [SerializeField] [Range(2, 3)] private float randWaitMax = 3f;

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
    [SerializeField] [Range(0, 10)] private float taserEntryRadius;

    //Acts as the exit radius for the ranged attack state
    private float taserExitRadius;

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

    [Tooltip("Maximum distance the player has to be from INSERT REST OF TOOLTIP")]
    private float playerEyeballMaxDist = 2;




    private GameObject playerVisTarget;

    [HideInInspector] public bool ceaseFire = false;

    [HideInInspector] private float fireRate;

    private Coroutine coroutine;

    private float oneTimeUseTimer = 0f;

    private float oneTimeUseTimerReset;

    private float eyeballSightRangeRecord;

    private int susSteps;

    private bool spotPlayerBool = false;

    private float susLevelDecreaseRecord;
    
    private bool guardStunned = false;

    private bool isShooting = false;

    private float targetVsPlayerDistance;

    private float targetSnapDistance = 3f;

    #endregion

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

        if (playerVisTarget == null)
        {
            playerVisTarget = GameObject.Find("VisionTarget");
        }

        if (playerMovenemtRef == null)
        {
            playerMovenemtRef = player.GetComponent<PlayerMovement>();
        }

        if (guardAnim == null)
        {
            guardAnim = GetComponent<GuardAnimatorScript>();
        }

        if (securityStationObjRef == null)
        {
            securityStationObjRef = GameObject.Find("Suspicion Manager");
        }

        if (guardAudioScript == null)
        {
            guardAudioScript = GetComponentInChildren < GuardAudio>();
        }
        #endregion Null Checks

        //Stores the user generated random direction time
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = true;

        //Starts the guard in the Passive State
        StateChange(EnemyStates.PASSIVE);

        SetAiSpeed(passiveSpeed);

        FaceTarget(target); 

        target = waypoints[waypointIndex].position;

        path = new NavMeshPath();     

        //Stores the user generated stun time
        stunTimeReset = stunTime;

        oneTimeUseTimerReset = oneTimeUseTimer;

        eyeballSightRangeRecord = eyeball.sightRange;

        fireRate = fireRateReset;

        securityStations = new List<GameObject>(GameObject.FindGameObjectsWithTag("SecurityStation"));

        taserExitRadius = taserEntryRadius + 10f;

        susLevelDecreaseRecord = eyeball.susDecreaseMultiplier;

    }//End Init


    public void StateChange(EnemyStates enemyStates) => stateMachine = enemyStates; //End StateChange


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

        //VerifyAlertLoc(alertLoc);

        //Add alert location verification here
        if (true)
        {
            if (stateMachine != EnemyStates.RANGEDATTACK)
            {
                eyeball.susLevel = hostileSusMax;

                stateMachine = EnemyStates.HOSTILE;

                target = alertLoc;

                eyeball.lastKnownLocation = alertLoc;

                agent.SetDestination(alertLoc);

                //Debug.Log("Alert has been called");
            }
        }
    }//End Alert


    //---------------------------------//
    private void VerifyAlertLoc(Vector3 fedLocation)
    {
        if (NavMesh.SamplePosition(fedLocation, out NavMeshHit hit, 0, 1) == false)
        {
            Debug.Log("Not a valid point on the navmesh");
        }
        else
        {
            Debug.Log("Valid point on navmesh");
        }
    }//End VerifyAlertLoc


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
            guardAudioScript.Chew();

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Slippery")
        {
            stateMachine = EnemyStates.STUNNED;
            other.GetComponent<PuddleAppearScript>().StartCoroutine("Disappear");
        }

    }//End OnTriggerEnter
    

    //---------------------------------//
    // Generates a random point for the guard to go to
    private Vector3 GenerateRandomPoint()
    {
        //Generates the initial random point
        Vector3 randpoint = Random.insideUnitSphere * randPointRad;

        //Returns a bool
        //First portion tests the randomly generated point to see if it's on the navmesh.
        //Second portion tests the path to the genreated point and to see if it's possible to reach that point
        if (NavMesh.SamplePosition(randpoint + transform.position, out NavMeshHit hit, randPointRad, 1) && NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path))
        {
            searchLoc = hit.position;

            susSteps++;

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
        Gizmos.DrawWireSphere(transform.position, taserEntryRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, taserExitRadius);

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
    }//End IBreakFreeDelay

    #endregion Methods

    #region Awake
    //---------------------------------//
    //  Using Awake() instead of Start() so that when spawning is functional, the AI won't break
    void Awake()
    {
        Init();

        taserPrefab.SetActive(false);
        //StartCoroutine(ITimer(15, shitBool));
    }//End Awake
    #endregion

    #region Update
    //Method called every frame
    void Update()
    {
        UpdateDebugText();

        #region Edge Case Stuff
        if (stateMachine == EnemyStates.RANGEDATTACK)
        {
            agent.stoppingDistance = taserExitRadius;
        }
        else if (stateMachine == EnemyStates.HOSTILE)
        {
            agent.stoppingDistance = 2f;
        }
        else
        {
            agent.stoppingDistance = .5f;
        }

        if (eyeball.canCurrentlySeePlayer == true && spotPlayerBool == false)
        {
            guardAudioScript.SpotPlayer();

            spotPlayerBool = true;
        }

        #endregion Edge Case Stuff

        //At all times be sure that there is a condition to at least ENTER and EXIT the state that the AI is being put into
        switch (stateMachine)
        {
            #region Passive Behavior
            //Patrol state for guard
            case EnemyStates.PASSIVE:

                //guardAudioScript.Idle();

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

                #region Exit Condition
                //Checking to see if the player is visible
                if (eyeball.susLevel > passiveSusMax)
                {
                    if (!isBoss)
                    {
                        // PASSIVE >>>> SUSPICIOUS
                        StateChange(EnemyStates.SUSPICIOUS);
                    }
                    else
                    {
                        // PASSIVE >>>> HOSTILE (ONLY HAPPENS IF ITS THE BOSS GUARD)
                        eyeball.susLevel = hostileSusMax;

                        StateChange(EnemyStates.HOSTILE);
                    }
                }
                #endregion Exit Condition

                break;
            #endregion Passive Behavior

            #region Suspicious Behavior
            //Finding random points in a set radius for the guard to go to
            //Also records the player's last known location and appends it to the waypoints list of the guard instance
            case EnemyStates.SUSPICIOUS:

                guardAnim.EnterSusAnim();


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
                        //Plays guard audio
                        guardAudioScript.Suspicious();

                        randWaitTime = Random.Range(randWaitMin, randWaitMax);

                        target = GenerateRandomPoint();

                        FaceTarget(target);

                        guardAnim.EnterSearchingAnim();
                    }
                }

                //Better off staying here after the target var has been messed around with accordingly
                SetSpeedAndDest(susSpeed, target);

                #region Exit Conditions

                if (eyeball.susLevel < passiveSusMax && susSteps == 5)
                {
                    guardAnim.ExitSearchingAnim();

                    oneTimeUseTimer = oneTimeUseTimerReset;

                    //SUSPICIOUS >> WARY
                    StateChange(EnemyStates.PASSIVE);

                    susSteps = 0;
                }

                if (eyeball.susLevel > sussySusMax)
                {
                    guardAnim.ExitSearchingAnim();

                    oneTimeUseTimer = oneTimeUseTimerReset;

                    //SUSPICIOUS >> HOSTILE
                    StateChange(EnemyStates.HOSTILE);

                    susSteps = 0;
                }
                #endregion Exit Conditions

                break;

            #endregion Suspicious Behavior

            #region Hostile Behavior
            //State for the guard to chase the player in
            case EnemyStates.HOSTILE:


                // Compared distance between actual player loc and last known location and if it is too small then the guard stays hostile as if they still saw the player

                targetVsPlayerDistance = Vector3.Distance(eyeball.lastKnownLocation, player.transform.position);

                //Animation Change
                guardAnim.EnterHostileAnim();

                SetAiSpeed(hostileSpeed);

                SetAIDestination(target);

                //Faces the target / player when they are visible
                //  - Added with the intention of the guard spending less time awkwardly facing seemingly random direcations

                // Also prevents the player from running circles around the guard
                if (eyeball.canCurrentlySeePlayer == true || agent.remainingDistance < targetSnapDistance)
                {
                    FaceTarget(target);
                }

                #region Exit Conditions
                //Conditionds needed for ranged attack / taser
                if (eyeball.canCurrentlySeePlayer == true && agent.remainingDistance < taserEntryRadius)
                {

                    guardAnim.EnterUnholster();

                    //HOSTILE >> RANGED ATTACK
                    StateChange(EnemyStates.RANGEDATTACK);
                }

                //Used for calling the LostPlayer() method from GuardAudio.cs
                else if (eyeball.susLevel < sussySusMax /*&& targetVsPlayerDistance < playerEyeballMaxDist*/)
                {
                    if (!isBoss)
                    {
                        //HOSTILE >> SUSPICIOUS
                        StateChange(EnemyStates.SUSPICIOUS);
                    }
                    else
                    {
                        eyeball.susLevel = 0f;

                        // HOSTILE >> PASSIVE
                        StateChange(EnemyStates.PASSIVE);
                    }

                    //The only way the guard can go to sus from hostile is if they lost the player
                    guardAudioScript.LostPlayer();
                }

                //When the guard is being baited by a thrown bottle or other noise making item
                if (eyeball.canCurrentlySeePlayer == false && agent.remainingDistance < .5f)
                {
                    StateChange(EnemyStates.SUSPICIOUS);
                }

                
                #endregion Exit Conditions

                break;
            #endregion Hostile Behavior

            #region Ranged Attack Behavior
            case EnemyStates.RANGEDATTACK:
                //Add secondary section to this state that changes the guard's behaviour from run / stop & gun to run & gun

                taserPrefab.SetActive(true);

                SetAiSpeed(0);

                target = eyeball.lastKnownLocation;

                FaceTarget(target);

                //guardAnim.ExitUnholster();

                if (!isShooting)
                {
                    guardAnim.EnterShoot();

                    isShooting = true;
                }

                SetAIDestination(target);


                //Eventually move this to the player as an event (make a listener / Unity event for this)
                //In the future make a better solution for the time scale, this is here because Patrick's superior intelligence saved your ass
                if (playerMovenemtRef.IsStunned == true || Time.timeScale != 1)
                {
                    guardAnim.EnterReload();
                    ceaseFire = true;
                }
                else
                {
                    guardAnim.ExitReload();
                    guardAnim.ExitSmack();
                    guardAnim.EnterShoot();
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

                        guardAudioScript.TaserFired();

                        guardAudioScript.ReloadTaser();
                    }
                }

                #region Exit Conditions
                if (eyeball.canCurrentlySeePlayer == false || agent.remainingDistance > taserExitRadius)   
                {
                    isShooting = false;

                    taserPrefab.SetActive(false);

                    //RANGED ATTACK >> HOSTILE
                    StateChange(EnemyStates.HOSTILE);
                }
                #endregion Exit Conditions

                break;
            #endregion Ranged Attack Behavior

            #region Stunned Behavior
            case EnemyStates.STUNNED:

                guardAnim.EnterStunAnim();

                if (!guardStunned)
                {
                    guardAudioScript.Fall();
                }
                guardStunned = true;

                SetAiSpeed(stunSpeed);

                SetAIDestination(target);

                eyeball.susLevel = 0;

                eyeball.sightRange = 0;

                if (stunTime > 0)
                {
                    stunTime -= Time.fixedDeltaTime;
                }
                //Exit Condition
                else if (stunTime < 0)
                {
                    guardAnim.ExitStunAnim();

                    isStunned = false;

                    guardStunned = false;

                    eyeball.susLevel = sussySusMax;
                    
                    //STUNNED >>>> PREVIOUS STATE (SUSPICIOUS for now)
                    StateChange(EnemyStates.SUSPICIOUS);
                    
                    //after changing states, the stun time returns to the initially recorded time
                    stunTime = stunTimeReset;

                    eyeball.sightRange = eyeballSightRangeRecord;
                }
                break;
            #endregion Stunned Behavior

            #region Default Behavior / Bug Catcher
            default:

                Debug.Log("State not found, initiating spin");

                transform.Rotate(new Vector3(0, 30f, 0) * Time.fixedDeltaTime, Space.Self);

                break;
            #endregion Default Behavior / Bug Catcher

        }//End State Machine
    }//End Update
    #endregion Update
}