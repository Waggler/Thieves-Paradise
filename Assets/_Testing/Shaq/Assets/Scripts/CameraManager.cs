using UnityEngine;
using UnityEngine.UI;

//Current Bugs:
//    - Camera can rotate outside of it's intended monitoring range in FOCUSED state
//      - When returning to MONITORING state, the camera will freeze due to being outside of it's intended rotational bounds
//    - 

//Things to add:
//    - Add rotational bounds to camera
//      - Investigate using the eyeball visibility "cone" as rotational bounds for the camera
//          - The camera essentitally can't rotate to an "unsupported" angle if the eyeball keeps it from doing that
//    - Visual Indicator of where the Eye can currently see in-game

//Done:
//    - Basic Camera functionality
//    - 


public class CameraManager : MonoBehaviour
{
    #region Variables
    [Header("Camera Target / Trigger")]
    private Transform target;
    [SerializeField] private GameObject player;

    [Header("Debug Text")]
    [SerializeField] private Text stateText;
    [SerializeField] private Text targetText;

    [Header("Camera Rotation Variables")]
    [SerializeField] [Range(0, 60)] private float camSpeed;
    [SerializeField] private Vector3 rotationMax;
    [HideInInspector] private Vector3 rotationRecord;
    //Transition speed between original rotation and look rotation in FaceTarget() method
    [SerializeField] private float snapSpeed;

    [Header("Suspicion Variables")] 
    [SerializeField] [Range(0, 10)] private float susMeter;
    [SerializeField] private float susIncrem = 0.01f;
    [SerializeField] private float susDecrim = 0.01f;

    [Header("Eyeball Integration / Eyeball Related Variables")]
    [SerializeField] private EyeballScript eyeball;

    [Header("Global Suspicion Manager Ref")]
    [SerializeField] private SuspicionManager suspicionManager;

    [Header("Debug Variables (May bite)")]
    [HideInInspector] private bool susFlag = false;
    [SerializeField] private Renderer rend;

    [SerializeField] private bool isDebug;


    #endregion

    #region Enumerations
    private enum CamStates
    {
        MONITORING,
        FOCUSED
    }

    [Header("Camera States")]
    [SerializeField] CamStates cameraStateMachine;

    #endregion

    #region Awake & Update (Start added for debug)

    //---------------------------------//
    //Callled when the object is spawned. Used instead of Start() because the camera could be spawned after the game has started
    #region Awake
    void Awake()
    {
        Init();

        //Use this space for debug variables
        rend = GetComponent<Renderer>();
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

                //Reducing suspicion level
                if (susMeter > 0 && cameraStateMachine == CamStates.MONITORING)
                {
                    susMeter -= susDecrim;
                }
                else if (susMeter < 0)
                {
                    susMeter = 0;
                }

                if (isDebug == true)
                {
                    rend.material.color = Color.green;
                }
                else
                {
                    break;
                }


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

                susFlag = true;


                //Exit condition for FOCUSED state
                if (eyeball.canCurrentlySeePlayer == false)
                {
                    susFlag = false;

                    //Reset's the camera's X & Z rotation
                    rotationRecord.x = 0;
                    rotationRecord.z = 0;

                    //While the X & Z rotation are reset, the Y rotation is preserved
                    transform.localEulerAngles = new Vector3(rotationRecord.x, rotationRecord.y, rotationRecord.z);

                    //FOCUSED >>> MONITORING
                    cameraStateMachine = CamStates.MONITORING;
                }

                //Raising suspicion level
                //Use of the susFlag bool ensures that the suspicion level can only go up if there is visual confirmation
                if (susFlag == true && susMeter < 10)
                {
                    susMeter += susIncrem;
                }
                else if (susMeter > 10)
                {
                    susMeter = 10;
                }

                //eyeball.lastKnownLocation;


                if (isDebug == true)
                {
                    rend.material.color = Color.red;
                }
                else
                {
                    break;
                }

                break;
            #endregion

            #region Defualt state
            //Not exactly a state but acts as a net to catch any bugs that would prevent the game from running
            default:
                stateText.text = "State Not Found";
                targetText.text = "Null";

                if (isDebug == true)
                {
                    rend.material.color = Color.yellow;
                }
                else
                {
                    break;
                }

                break;
            #endregion
        }
        #endregion

        #endregion
    }//End Update
    #endregion

    #region General Functions
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
    }//End Init


    private Vector3 AlertGuards(Vector3 targetLoc)
    {
        //Record lastknownlocation from eyeball script

        //Get ping radius

        //Ping guards within this radius

        //???

        //profit


        return targetLoc;
    }

    #endregion
}