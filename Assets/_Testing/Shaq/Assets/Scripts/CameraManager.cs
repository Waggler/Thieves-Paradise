using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//Current Bugs:
//    - Camera can rotate outside of it's intended monitoring range in FOCUSED state
//      - When returning to MONITORING state, the camera will freeze due to being outside of it's intended rotational bounds
//    - 

//Things to add:
//    - Fix layermask issues with raycast so that it:
//      - Hits regular ("Default" layer) objects
//      - Ignores "Glass" ("IgnoreRaycast" layer) objects
//      - Seeks out the player ("Player" layer)
//    - Add rotational bounds to camera

//Things to look at for reference:
//    - 

//Done:
//    - Overall Camera functionality


public class CameraManager : MonoBehaviour
{
    #region Variables
    [Header("Camera Target / Trigger")]
    private Transform target;

    [Header("Object References")]
    [SerializeField] private Transform player;

    [Header("Debug Text")]
    [SerializeField] private Text stateText;
    [SerializeField] private Text targetText;

    [Header("Camera Rotation Variables")]
    [SerializeField] [Range(0, 60)] private float degreesPerSec;
    [SerializeField] private Vector3 rotationMax;
    [SerializeField] private Vector3 rotationRecord;

    [Header("Suspicion Variables")] 
    [SerializeField] [Range(0, 10)] private float suspicion;
    [SerializeField] private float suspicionIncrement = 0.01f;
    [SerializeField] private float suspicionDecriment = 0.01f;

    [Header("Eyeball Integration")]
    [SerializeField] private EyeballScript eyeballScript;

    [Header("Debug Variables (May bite)")]
    [SerializeField] private bool susFlag = false;

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

    #region Awake

    void Awake()
    {
        Init();
    }//End Awake

    #endregion

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

                target = null;

                targetText.text = $"{target}";

                //Rotating at degreesPerSec relative to the World Space
                transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime, Space.World);

                //Comparing the Y-axis rotation between the camera and it's Maximum allowed Y rotation

                if (transform.localRotation.eulerAngles.y >= rotationMax.y)
                {
                    //print("Inverting turn rate");

                    degreesPerSec = -degreesPerSec;
                }

                //if (distanceToPlayer <= lookRadius)
                if (eyeballScript.canCurrentlySeePlayer == true)
                {
                    //MONITORING >>> FOCUSED
                    cameraStateMachine = CamStates.FOCUSED;
                }

                //Reducing suspicion level
                if (suspicion > 0 && cameraStateMachine == CamStates.MONITORING)
                {
                    print("SUSPICION LOWERING");
                    suspicion -= suspicionDecriment;
                }
                else if (suspicion < 0)
                {
                    suspicion = 0;
                }

                break;
            #endregion

            #region Focused State
            //When the camera sees the player / FOCUSED
            case CamStates.FOCUSED:

                ////Manual clamping of Camera rotation within rotation min & max values
                //if (transform.localRotation.eulerAngles.y >= rotationMax.y)
                //{
                //    print("Bro, what in the FUCK are you doing rotating like that");
                //    transform.localEulerAngles = new Vector3(rotationMax.x, rotationMax.y, rotationMax.z);
                //}

                stateText.text = $"{cameraStateMachine}";

                target = player.transform;

                targetText.text = $"{target}";

                FaceTarget();

                susFlag = true;

                //if (distanceToPlayer >= lookRadius)
                if (eyeballScript.canCurrentlySeePlayer == false)
                {
                    //find a more effective method than this
                    cameraStateMachine = CamStates.MONITORING;

                    susFlag = false;

                    //Reset's the camera's X & Z rotation
                    rotationRecord.x = 0;
                    rotationRecord.z = 0;

                    transform.localEulerAngles = new Vector3(rotationRecord.x, rotationRecord.y, rotationRecord.z);

                    //FOCUSED >>> MONITORING
                    cameraStateMachine = CamStates.MONITORING;
                }

                //Raising suspicion level
                //Use of the susFlag bool ensures that the suspicion level can only go up if there is visual confirmation
                //Consider just adding this into the if(VisionCheck() == true) statement
                if (susFlag == true && suspicion < 10)
                {
                    print("SUSPICION RISING");
                    suspicion += suspicionIncrement;
                }
                else if (suspicion > 10)
                {
                    suspicion = 10;
                }

                break;
            #endregion

            #region Defualt state
            default:
                stateText.text = "State Not Found";
                targetText.text = "Null";

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
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

        //try not to add Time.deltaTime to this as it will result in the Camera needing to use it's rotational Z-axis
        // this will result in an unusual appearance in the final game
        transform.rotation = lookRotation;

    }//End FaceTarget

    private void Init()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }

        stateText.text = "";
        targetText.text = "";

        rotationMax = new Vector3(0, 90, 0);

        rotationRecord = new Vector3(0, 0, 0);

        //Remnant from having two angles for the cam to cycle between
        //Leave this in in the event that current method breaks and two hard angles are needed
        //rotationMin = new Vector3(0, -60, 0);

        //Set's the CameraAI's state to MONITORING on awake
        cameraStateMachine = CamStates.MONITORING;
    }

    #endregion
}