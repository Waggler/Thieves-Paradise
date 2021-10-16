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
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Self;

    [Header("Debug Text")]
    [SerializeField] private Text stateText;
    [SerializeField] private Text targetText;

    [Header("Camera Rotation Variables")]
    [SerializeField] private float lookRadius;
    [SerializeField] [Range(0, 60)] private float degreesPerSec;
    [SerializeField] private Vector3 rotationMax;
    [SerializeField] private Vector3 rotationMin;

    [Header("Suspicion Variables")] 
    [SerializeField] [Range(0, 10)] private float suspicion;
    [SerializeField] private float suspicionIncrement = 0.01f;
    [SerializeField] private float suspicionDecriment = 0.01f;

    [Header("Camera Raycast Layermask Selection")]
    //[SerializeField] [Range(0,8)] private int layerMask;

    [Header("Debug Variables (Be warned some of these won't do anything most of the time)")]
    [SerializeField] private Quaternion originalRotation;
    [SerializeField] private bool susFlag = false;
    [SerializeField] private Vector3 rotationRecord;

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
        stateText.text = "";
        targetText.text = "";

       rotationMax = new Vector3(0, 90, 0);
       rotationMin = new Vector3(0, -rotationMax.y, 0);

        rotationRecord = new Vector3(0, 0, 0);
        
        //Remnant from having two angles for the cam to cycle between
        //Leave this in in the event that current method breaks and two hard angles are needed
        //rotationMin = new Vector3(0, -60, 0);

        //Set's the CameraAI's state to MONITORING on awake
        cameraStateMachine = CamStates.MONITORING;

        originalRotation = Self.transform.rotation;
    }//End Awake

    #endregion

    #region Update
    void Update()
    {
        #region Update Specific Variables
        //Can change this variable type to Transform if need be
        //The ? at the end of Vector3 just means that it is now a nullable Vector3 variable
        //By default Vector3 variables cannot have a null value outside of being null on Awake() / Start
        Vector3? playerReport;

        //Calculates the distance to the player in Unity Units
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

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

                playerReport = null;

                //Rotating at degreesPerSec relative to the World Space
                transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime, Space.Self);

                //Comparing the Y-axis rotation between the camera and it's Maximum allowed Y rotation

                if (transform.localRotation.eulerAngles.y >= rotationMax.y)
                {
                    //print("Inverting turn rate");

                    degreesPerSec = -degreesPerSec;
                }

                //Do not delete this snippet please
                //if (transform.localRotation.eulerAngles.y <= rotationMin.y )
                //{
                //    print("Going over minimum allowed rotation / NEGATIVE");

                //    degreesPerSec = -degreesPerSec;

                //}

                if (distanceToPlayer <= lookRadius)
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

                //Calling for raycast in order to get "Visual Confirmation"
                VisionCheck();

                if (VisionCheck() == true)
                {

                    stateText.text = $"{cameraStateMachine}";

                    target = player.transform;

                    targetText.text = $"{target}";


                    //Insert check for if the cam rotation is in it's degree bounds

                    FaceTarget();

                    playerReport = player.transform.position;

                    susFlag = true;

                }
                else if (VisionCheck() == false)
                {
                    print($"VisionCheck = {VisionCheck()}");

                    //find a more effective method than this
                    cameraStateMachine = CamStates.MONITORING;

                    susFlag = false;

                }

                if (distanceToPlayer >= lookRadius)
                {
                    //print($"Resetting to {originalRotation}");

                    //Reset's the camera's X & Z rotation
                    rotationRecord.x = 0;
                    rotationRecord.z = 0;

                    transform.localEulerAngles = new Vector3(0, rotationRecord.y, 0);

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

    //---------------------------------//
    //Function calls a raycast to check for line of sight with target
    //Using bool instead of void so that the call of the function simply returns a value of true or false (this is modified by what is set for the return value)
    bool VisionCheck()
    {
        //Player layer mask
        int layerMask = 1 >> 1;
        //int layerMask = 2;

        RaycastHit hit;

        //Physics.Raycast will return a bool of either true or false, if true it will register as if the statement was if(bool == true) => Do this
        if (Physics.Raycast(transform.position, transform.forward, out hit, lookRadius, layerMask))
        {
            //Draws the ray if the raycast is true
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance , Color.magenta);
            
            //print("Hit.");

            return true;
        }
        else
        {
            //print("No hit.");

            return false;
        }

    }//End VisionCheck

    //---------------------------------//
    //Draws a debug object known as a "Gizmo"
    private void OnDrawGizmos()
    {
        //Drawing a magenta sphere
        Gizmos.color = Color.magenta;
        //Sphere radius tied to Look Radius variable
        Gizmos.DrawWireSphere(Self.transform.position, lookRadius);

        //Using this raycast to determine the actual forward vector of the camera object in scene
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Self.transform.position, Self.transform.forward * lookRadius);

    }//End OnDrawGizmos

    #endregion
}