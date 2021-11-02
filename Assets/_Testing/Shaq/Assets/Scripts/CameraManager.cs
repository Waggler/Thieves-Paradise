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
//    - Manipulate the susLevel of the guard instances
//    - 
//    - 

//Done:
//    - 
//    - 
//    - 


public class CameraManager : MonoBehaviour
{
    #region Variables
    [Header("Camera Target / Trigger")]
    private Transform target;
    [Tooltip("References the player object")]
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
    //Transition speed between original rotation and look rotation in FaceTarget() method
    [Tooltip("The at which the camera looks at it's target")]
    [SerializeField] private float snapSpeed;


    [Header("Eyeball Integration / Eyeball Related Variables")]
    [Tooltip("References the eyeball prefab attatched to the camera prefab")]
    [SerializeField] private EyeballScript eyeball;


    [Header("Global Suspicion Manager Ref")]
    [Tooltip("References to the Suspicion Manager in the level")]
    [SerializeField] private SuspicionManager suspicionManager;


    [Header("Camera Light Variables")]
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
    [Tooltip("References the Renderer component of the camera")]
    [SerializeField] private Renderer rend;
    [Tooltip("Disable this when making a build to have the State & Target text not show up")]
    [SerializeField] private bool isDebug;
    [Tooltip("Radius in which guards can be  'called'  by the camera")]
    [SerializeField] [Range(0, 50)] private float callRadius;

    [HideInInspector] private float distanceToCamera;

    #endregion

    #region Enumerations
    private enum CamStates
    {
        MONITORING,
        FOCUSED
    }

    [Header("Camera States")]
    [Tooltip("The current state that the camera is in")]
    [SerializeField] CamStates cameraStateMachine;

    #endregion

    #region Lists & Arrays 

    [Header("Guard Array")]
    [Tooltip("Shows the list of guards")]
    [SerializeField] private GameObject[] guardsArray;


    #endregion Lists

    #region Awake & Update (Start added for debug)

    //---------------------------------//
    //Callled when the object is spawned. Used instead of Start() because the camera could be spawned after the game has started
    #region Awake
    void Awake()
    {
        Init();

        //Use this space for debug variables
        rend = GetComponent<Renderer>();

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

        //Calculating the camera's distance from the player

        #endregion

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
                    //MONITORING >>> FOCUSED
                    cameraStateMachine = CamStates.FOCUSED;
                }

                camLightRef.color = Color.green;

                break;
            #endregion

            #region Focused State
            //When the camera sees the player / FOCUSED
            case CamStates.FOCUSED:

                stateText.text = $"{cameraStateMachine}";

                //referencing player variable from the eyeball script
                target = player.transform;

                targetText.text = $"{target}";

                FaceTarget();

                camLightRef.color = Color.red;

                AlertGuards(eyeball.lastKnownLocation);

                //Exit condition for FOCUSED state
                if (eyeball.canCurrentlySeePlayer == false)
                {

                    //Reset's the camera's X & Z rotation
                    rotationRecord.x = 0;
                    rotationRecord.z = 0;

                    //While the X & Z rotation are reset, the Y rotation is preserved
                    transform.localEulerAngles = new Vector3(rotationRecord.x, rotationRecord.y, rotationRecord.z);

                    //FOCUSED >>> MONITORING
                    cameraStateMachine = CamStates.MONITORING;
                }

                break;
            #endregion

            #region Defualt state
            //Not exactly a state but acts as a net to catch any bugs that would prevent the game from running
            default:
                stateText.text = "State Not Found";
                targetText.text = "Null";

                break;
            #endregion
        }
        #endregion


        UpdateCamLightVars();

        #endregion
    }//End Update
    #endregion

    #region General Functions

    //---------------------------------//
    //
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;

    //    //Vector3 direction1 = transform.TransformDirection(Vector3.forward);
    //    Vector3 direction2 = transform.TransformDirection(rotationMax);
    //    //Gizmos.DrawRay(transform.position, direction1);
    //    Gizmos.DrawRay(transform.position, direction2);
    //}//End OnDrawGizmos
    //---------------------------------//



    //---------------------------------//
    //Function that makes the object face it's target
    void FaceTarget()
    {
        //generates the direction that the camera needs to face
        Vector3 direction = (target.position - transform.position).normalized;

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

        //rotationMax = new Vector3(0, 90, 0);

        rotationRecord = new Vector3(0, 0, 0);

        //Set's the CameraAI's state to MONITORING on awake
        cameraStateMachine = CamStates.MONITORING;

        UpdateCamLightVars();

    }//End Init

    //---------------------------------//
    //Used to update all camera light related variables all at once
    private void UpdateCamLightVars()
    {
        //Camera Light Variables

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
    private void OnDrawGizmos()
    {
        //Gizmo color
        Gizmos.color = Color.yellow;

        //Gizmo type
        Gizmos.DrawWireSphere(transform.position/* + Vector3.up*/, callRadius);
    }//End OnDrawGizmos
    //---------------------------------//




    ////---------------------------------//
    ////
    //private void GenGuardArray(GameObject guard)
    //{
    //    print($"Generating Array with {guard}...");

    //    //generates array
    //    for (int i = 0; i < guardsArray.Length; i++)
    //    {
    //        guard = guardsArray[i];
    //    }

    //    print($"There are {guardsArray.Length} guards in the scene");
    //}//End GenGuardArray
    ////---------------------------------//

    ////---------------------------------//
    ////Probably going to delete this
    //private void ClearGuardArray()
    //{

    //}//End ClearGuardArray
    ////---------------------------------//





    ////---------------------------------//
    ////
    private void AlertGuards(Vector3 targetLoc)
    {
        //EnemyManager reference
        EnemyManager enemyManager;

        //Used to reference each guard
        foreach (GameObject guard in guardsArray)
        {
            distanceToCamera = Vector3.Distance(guard.transform.position, transform.position + Vector3.up);
            
            //Individual guard reference
            enemyManager = guard.GetComponent<EnemyManager>();

            if (distanceToCamera <= callRadius)
            {
                //Modifying the target location of the guard
                enemyManager.lastKnownLocation = eyeball.lastKnownLocation;
                
            }
            else
            {
                //Showing which guards are out of range (purely there for debug reasons)
                print($"{guard} is outside of camera range.");
            }
        }

        //GenGuardArray(gameObject);

        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, callRadius);
        //foreach (var hitcollider in hitColliders)
        //{
        //}

    }//End AlertGuards
}
    //---------------------------------//

    #endregion
