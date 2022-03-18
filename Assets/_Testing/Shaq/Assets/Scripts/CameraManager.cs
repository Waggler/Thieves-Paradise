using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{

    #region Enumerations
    public enum CamStates
    {
        MONITORING,
        FOCUSED,
        DISABLED
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
    [Header("Floor / Level")]
    public GameObject floor;


    [Header("Camera Target / Trigger")]
    [SerializeField] private Vector3 target;
    [Tooltip("References the player's vision target, auto generated")]
    [SerializeField] private GameObject player;


    [Header("Debug Text")]
    [Tooltip("This text shows what state the camera is in")]
    [SerializeField] private Text stateText;
    [Tooltip("This text shows what the camera's target is")]
    [SerializeField] private Text targetText;


    [Header("Camera Rotation Variables")]
    [Tooltip("Camera rotation speed, range of 0 to 60")]
    [SerializeField] /*[Range(0, 60)]*/ private float camSpeed;
    [Tooltip("Maximum rotation vector for the camera (edit the Y-axis value)")]
    [SerializeField] private float rotationMax;
    [Tooltip("Transition speed between original rotation and look rotation in FaceTarget() method")]


    [Header("Eyeball Integration")]

    [SerializeField] private EyeballScript eyeball;

    [SerializeField] private GameObject surpriseVFX;

    [SerializeField] private GameObject confusedVFX;



    [Header("Local Suspicion Manager")]
    [Tooltip("References the Local Suspicion Manager")]
    [SerializeField] private SuspicionManager suspicionManager;
    [Tooltip("Radius in which guards can be  'called'  by the camera")]
    [SerializeField] [Range(0, 50)] private float callRadius;


    [Header("Camera Light Variables")]
    [Tooltip("When the player is this far away, the spotlight becomes disabled")]
    [SerializeField] [Range(0, 500)] private float killRadius;
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


    [Header("Testing / Temp Variables")]

    [HideInInspector] private float distanceToCamera;

    private Quaternion initialRotation;

    private float disabledTime;

    [Tooltip("Amount of the time the camera will be disabled for")]
    [SerializeField] private float disabledTimeReset;


    #endregion Variables

    #region Awake & Update

    //---------------------------------//
    //Callled when the object is spawned. Used instead of Start() because the camera could be spawned after the game has started
    #region Awake
    void Awake()
    {
        Init();

        //LockedRotation();
    }//End Awake

    #endregion

    //---------------------------------//
    //Called every frame
    #region Update
    void FixedUpdate()
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

        UpdateDebugText();

        UpdateCamLightVars();

        #region Cam State Machine
        switch (cameraStateMachine)
        {
            #region Monitoring State
            //When the camera does not see the player / MONITORING
            case CamStates.MONITORING:
                stateText.text = $"{cameraStateMachine}";

                //Since there is no target when monitoring, the value is set to null

                targetText.text = $"{target}";

                //Rotating at degreesPerSec relative to the World Space
                transform.Rotate(new Vector3(0, camSpeed, 0) * Time.fixedDeltaTime, Space.Self);

                //Reseting alert related variables
                if (isAudioSourcePlaying == true)
                {
                    audioSource.Stop();

                    isAudioSourcePlaying = false;
                }

                Vector3 hiddenRotationMax = new Vector3(0, rotationMax, 0);

                hiddenRotationMax.y = Mathf.Clamp(transform.rotation.y, rotationMax, -rotationMax);

                //Technically this snippet of code shouldn't work yet it does, will likely break in the future and need to be fixed
                //Comparing the Y-axis rotation between the camera and it's Maximum allowed Y rotation
                if (transform.localRotation.eulerAngles.y > hiddenRotationMax.y)
                {
                    //Inverts the camera's turn speed
                    camSpeed = -camSpeed;
                }

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

                target = eyeball.lastKnownLocation;

                transform.LookAt(target);

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
                    transform.rotation = initialRotation;

                    //FOCUSED >>> MONITORING
                    cameraStateMachine = CamStates.MONITORING;
                }
                break;
            #endregion Focused State

            #region Disabled State
            case CamStates.DISABLED:

                if (disabledTime > 0)
                {
                    disabledTime -= Time.fixedDeltaTime;
                }
                else if (disabledTime < 0)
                {
                    disabledTime = disabledTimeReset;

                    cameraStateMachine = CamStates.MONITORING;
                }

                camLightRef.color = Color.yellow;

                break;
            #endregion Disabled State

            #region Default / Error state
            default:
                break;
            #endregion Default / Error State
        }
        #endregion Cam State Machine

    }//End Update
    #endregion Update

    #endregion Awake & Update

    #region General Methods


    //---------------------------------//
    //Used to preload all necessary variables or states in the Camera Manager script
    private void Init()
    {
        stateText.text = "";
        targetText.text = "";

        cameraStateMachine = CamStates.MONITORING;

        //Note: This method of referencing the suspicion manager is stupid and I should find a way to do it in one line
        //Creates a reference to the suspicion manager object
        susManagerOBJ = GameObject.FindGameObjectWithTag("SecurityStation");

        //creates a direct reference to the suspicion manager script
        susManagerRef = susManagerOBJ.GetComponent<SuspicionManager>();

        //Set's the CameraAI's state to MONITORING on awake

        player = GameObject.FindWithTag("PlayerVisionTarget");

        //Changes camera light color to green
        camLightRef.color = Color.green;

        initialRotation = transform.rotation;

        disabledTime = disabledTimeReset;

        UpdateCamLightVars();
    }//End Init


    //---------------------------------//
    // Updates the debug text above the guard's head
    private void UpdateDebugText()
    {
        string methodStateText;

        methodStateText = cameraStateMachine.ToString();

        stateText.text = methodStateText;


        string methodTargetText;

        methodTargetText = target.ToString();

        targetText.text = methodTargetText;
    }//End UpdateDebugText


    //---------------------------------//
    //Used to update all camera light related variables all at once
    private void UpdateCamLightVars()
    {
        //Sets the camera light's intensity
        camLightRef.intensity = camLightIntensity;

        //Sets the camera light's range
        camLightRef.range = eyeball.sightRange * camLightRange;

        //Sets the camera light's outer spot angle
        camLightRef.spotAngle = eyeball.maxVisionAngle * camLightMaxAngle;

        //Sets the camera light's inner spot angle
        camLightRef.innerSpotAngle = eyeball.maxVisionAngle * camLightMinAngle;

    }//End UpdateCamVars


    //---------------------------------//
    //Draws Gizmos / shapes in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, callRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killRadius);

    }//End OnDrawGizmosSelected
}
#endregion General Methods
