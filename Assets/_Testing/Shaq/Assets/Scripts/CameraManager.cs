using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

//Current Bugs:
//    - Camera can rotate outside of it's intended monitoring range in FOCUSED state
//      - When returning to MONITORING state, the camera will freeze due to being outside of it's intended rotational bounds
//    - 

//Things to add:
//    - (Backlog) Add rotational bounds to camera
//    - Take orders from the Security hub / global suspicion manager 
//    - 

//Notes:
//    - The script is looking for an object with the "PlayerVisionTarget" tag, so make sure the player has that or update accordingly
//    - Rotational bounds still need a bit of work before it's TRULY done
//    - DO NOT GET RID OF THE "rotationRecord" VARIABLE




public class CameraManager : MonoBehaviour
{

    #region Enumerations
    private enum CamStates
    {
        MONITORING,
        FOCUSED
    }

    [Header("Camera States")]
    [Tooltip("The current state that the camera is in")]
    [SerializeField] CamStates cameraStateMachine;

    #endregion Enumerations

    #region Lists & Arrays 

    //[Header("Guard Array")]
    //[Tooltip("Shows the list of guards")]
    //[SerializeField] private GameObject[] guardsArray;
    
    #endregion Lists & Arrays

    #region Variables
    [Header("Camera Target / Trigger")]
    private Vector3? target;
    [Tooltip("References the player's vision target, auto generated")]
    [SerializeField] private GameObject player;


    [Header("Debug Text")]
    [Tooltip("This text shows what state the camera is in")]
    [SerializeField] private Text stateText;
    [Tooltip("This text shows what the camera's target is")]
    [SerializeField] private Text targetText;


    [Header("Camera Rotation Variables")]
    [Tooltip("Camera rotation speed, range of 0 to 60")]
    [SerializeField] [Range(0, 60)] private float camSpeed;
    [Tooltip("Maximum rotation vector for the camera (edit the Y-axis value)")]
    [SerializeField] private Vector3 rotationMax;
    [HideInInspector] private Vector3 rotationRecord;
    [Tooltip("Transition speed between original rotation and look rotation in FaceTarget() method")]
    [SerializeField] private float snapSpeed;
    [HideInInspector] private Vector3 startRotation;


    [Header("Eyeball Integration")]
    [Tooltip("References the eyeball prefab attatched to the camera prefab [Not auto generated]")]
    [SerializeField] private EyeballScript eyeball;

    [SerializeField] private GameObject surpriseVFX;

    [SerializeField] private GameObject confusedVFX;



    [Header("Local Suspicion Manager")]
    [Tooltip("References the Local Suspicion Manager")]
    [SerializeField] private SuspicionManager localSuspicionManager;
    [Tooltip("Radius in which guards can be  'called'  by the camera")]
    [SerializeField] [Range(0, 50)] private float callRadius;


    [Header("Camera Light Variables")]
    [Tooltip("When the player is this far away, the spotlight becomes disabled")]
    [SerializeField] [Range(0, 500)]private float killRadius;
    [Tooltip("References the Spotlight attatched to the camera prefab")]
    [SerializeField] private Light camLightRef;
    [Tooltip("Spotlight Intensity")]
    [SerializeField] [Range(0, 100)] private float camLightIntensity;
    [Tooltip("Spotlight Range")]
    [SerializeField] [Range(0, 2)] private float camLightRange;
    [Tooltip("Spotlight Maximum Angle")]
    [SerializeField] [Range(0, 5)] private float camLightMaxAngle;
    [Tooltip("Spotlight Minimum Angle")]
    [SerializeField] [Range(0, 5)] private float camLightMinAngle;


    [Header("Debug Variables")]
    [Tooltip("Disable this when making a build to have the State & Target text not show up")]
    [SerializeField] private bool isDebug;
    //References the "Suspicion Manager" object in the scene
    [SerializeField] private GameObject susManagerOBJ;
    //References the "SuspcionManager.cs" script found on the "Suspicion Manager" object
    [SerializeField] private SuspicionManager susManagerRef;

    [Header("Audio Variables")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private bool isAudioSourcePlaying;


    [HideInInspector] private float distanceToCamera;

    #endregion Variables

    #region Start
    private void Start()
    {
        startRotation = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    #endregion Start

    #region Awake & Update

    //---------------------------------//
    //Callled when the object is spawned. Used instead of Start() because the camera could be spawned after the game has started
    #region Awake
    void Awake()
    {
        Init();

        //Use this space for debug variables
        camLightRef.color = Color.green;


    }//End Awake

    #endregion

    //---------------------------------//
    //Called every frame
    #region Update
    void Update()
    {
        #region Update Specific Variables
        //Records rotaion of the camera object
        rotationRecord = new Vector3 (transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z);

        //Local player reference || delete if it blocks progress on other things
        //player = GameObject.FindWithTag("PlayerVisionTarget");

        #endregion Update Specific Variables

        #region Cam State Machine
        switch (cameraStateMachine)
        {

            #region Monitoring State
            //When the camera does not see the player / MONITORING
            case CamStates.MONITORING:
                stateText.text = $"{cameraStateMachine}";

                //Since there is no target when monitoring, the value is set to null
                target = null;

                targetText.text = $"{target}";

                //Rotating at degreesPerSec relative to the World Space
                transform.Rotate(new Vector3(0, camSpeed, 0) * Time.deltaTime, Space.World);

                //Reseting alert related variables
                if (isAudioSourcePlaying == true)
                {
                    audioSource.Stop();

                    isAudioSourcePlaying = false;
                }

                //Technically this snippet of code shouldn't work yet it does, will likely break in the future and need to be fixed
                //Comparing the Y-axis rotation between the camera and it's Maximum allowed Y rotation
                if (transform.localRotation.eulerAngles.y >= rotationMax.y)
                {
                    //Inverts the camera's turn speed
                    camSpeed = -camSpeed;
                }

                //if (distanceToPlayer <= lookRadius)
                if (eyeball.canCurrentlySeePlayer == true)
                {
                    Instantiate(surpriseVFX, transform.position, transform.rotation);

                    //MONITORING >>> FOCUSED
                    cameraStateMachine = CamStates.FOCUSED;
                }

                camLightRef.color = Color.green;

                break;
            #endregion Monitoring State

            #region Focused State
            //When the camera sees the player / FOCUSED
            case CamStates.FOCUSED:

                float stopWatch = Time.deltaTime;

                stateText.text = $"{cameraStateMachine}";

                //referencing player variable from the eyeball script
                target = player.transform.position;

                targetText.text = $"{target}";

                FaceTarget();

                camLightRef.color = Color.red;

                susManagerRef.AlertGuards(eyeball.lastKnownLocation, transform.position, callRadius);




                //Playing Alert Audio
                if (isAudioSourcePlaying == false)
                {
                    audioSource.Play();

                    isAudioSourcePlaying = true;
                }




                //Exit condition for FOCUSED state
                if (eyeball.canCurrentlySeePlayer == false)
                {

                    //Reset's the camera's X & Z rotation
                    rotationRecord.x = 0;
                    rotationRecord.z = 0;

                    //While the X & Z rotation are reset, the Y rotation is preserved
                    transform.localEulerAngles = new Vector3(-startRotation.x, 0, 0);

                    //FOCUSED >>> MONITORING
                    cameraStateMachine = CamStates.MONITORING;
                }

                break;
            #endregion Focused State

            #region Default / Error state
            //Not exactly a state but acts as a net to catch any bugs that would prevent the game from running
            default:
                stateText.text = "State Not Found";
                targetText.text = "Null";

                break;
            #endregion Default / Error State
        }
        #endregion Cam State Machine


        UpdateCamLightVars();

    }//End Update
    #endregion Update

    #endregion Awake & Update

    #region General Methods

    //---------------------------------//
    //Function that makes the object face it's target
    void FaceTarget()
    {
        //generates the direction that the camera needs to face
        Vector3 direction = (Vector3)(target - transform.position);

        Vector3.Normalize(direction);

        //Creates a quaternion var and assings it a look rotation
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

        //Using Quaternion.Slerp instead of transform.rotation = lookRotation in order to keep camera snapping smooth
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * snapSpeed);

    }//End FaceTarget

    //---------------------------------//
    //Used to preload all necessary variables or states in the Camera Manager script
    private void Init()
    {
        stateText.text = "";
        targetText.text = "";

        //startRotation = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        //Note: This method of referencing the suspicion manager is stupid and I should find a way to do it in one line
        //Creates a reference to the suspicion manager object
        susManagerOBJ = GameObject.FindGameObjectWithTag("GameController");

        //creates a direct reference to the suspicion manager script
        susManagerRef = susManagerOBJ.GetComponent<SuspicionManager>();

        //print($"SuspicionManager instance = {susManagerRef}");

        rotationRecord = new Vector3(0, 0, 0);

        //Set's the CameraAI's state to MONITORING on awake
        cameraStateMachine = CamStates.MONITORING;

        player = GameObject.FindWithTag("PlayerVisionTarget");

        UpdateCamLightVars();

        susManagerRef.GenGuardList();

    }//End Init

    //---------------------------------//
    //Used to update all camera light related variables all at once
    private void UpdateCamLightVars()
    {
        //Camera Light Variables

        distanceToCamera = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToCamera >= killRadius)
        {
            //Comment out these two lines for me pls
            //camLightIntensity = Mathf.Lerp(camLightIntensity, 0, 5f);

            camLightRef.enabled = false;
        }
        else if (distanceToCamera <= killRadius)
        {
            //camLightIntensity = Mathf.Lerp(camLightIntensity, 50, 5f);

            camLightRef.enabled = true;
        }

        #region Individual Variables

        //Sets the camera light's intensity
        camLightRef.intensity = camLightIntensity;

        //Sets the camera light's range
        camLightRef.range = eyeball.sightRange * camLightRange;

        //Sets the camera light's outer spot angle
        camLightRef.spotAngle = eyeball.maxVisionAngle * camLightMaxAngle;

        //Sets the camera light's inner spot angle
        camLightRef.innerSpotAngle = eyeball.maxVisionAngle * camLightMinAngle;

        #endregion Individual Variables

    }//End UpdateCamVars

    //---------------------------------//
    //Draws Gizmos / shapes in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, callRadius);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killRadius);

    }//End OnDrawGizmos

}

#endregion General Methods
