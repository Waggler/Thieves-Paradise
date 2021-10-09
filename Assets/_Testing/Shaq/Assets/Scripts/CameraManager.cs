using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//Current Bugs:
//    - Added a functional speed for camera's rotation when docile
//    - Ability for the Camera AI to report the player's position to the surrounding AI
//    - With current methods, when the player enters and then exits the look radius of the camera, the camera will spin "weird"
//    - 

//Things to add:
//    - Functional method that moves the camera back and forth
//    - Way to measure objects rotation in degree instead of the Quaternion method
//    - State Machine for the Camera AI
//    - 
//    - USE THIS FOR THE LOVE OF FUCKING CHRIST 
//    - >>>>>>>>>>>>>>>>>  transform.rotation.eulerAngles <<<<<<<<<<<<<<<<<<<<<<<
//    - ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//    - THIS SHIT RIGHT HERE IS THEEEEEEEEEEEEEEEEEE FUCKING ANSWER
//    - YOU HEAR THAT? DON'T FUCKING LOSE IT YOU DIPSHIT
//    - 

//Things to look at for reference:
//    - https://answers.unity.com/questions/709535/clamp-camera-quaternion-so-you-cant-look-up-or-dow.html
//    - https://docs.unity3d.com/ScriptReference/Transform-eulerAngles.html
//    - https://docs.unity3d.com/ScriptReference/Quaternion.LookRotation.html
//    - https://docs.unity3d.com/ScriptReference/Space.World.html
//    - 


//Done:
//    - Condition for the camera to look at the player
//    - Condition for the player to go back to it's "Monitoring state"
//    - Added a nullable variable that can report the position of the player's position
//    - 


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
    [SerializeField] private float degreesPerSec;
    [SerializeField] private Vector3 rotationMax;
    [SerializeField] private Vector3 rotationMin;

    [Header("Debug Variables (Be warned some of these won't do anything most of the time)")]
    [SerializeField] private Quaternion originalRotation;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Vector3 selfRotation;

    [SerializeField] private bool @bool = false;


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

    #region Awake / Start
    private void Start()
    {
    }

    void Awake()
    {
        stateText.text = "";
        targetText.text = "";

        originalRotation = Self.transform.rotation;

    }//End Awake

    #endregion

    #region Update
    void Update()
    {
        //Can change this variable type to Transform if need be
        //The ? at the end of Vector3 just means that it is now a nullable Vector3 variable
        //By default Vector3 variables cannot have a null value outside of being null on Awake() / Start
        Vector3? playerReport;

        transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime, Space.Self);

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        #region Cam State Machine
        switch (cameraStateMachine)
        {
            #region Monitoring State
            //When the camera does not see the player / MONITORING
            case CamStates.MONITORING:
                stateText.text = $"{cameraStateMachine}";


                target = null;

                stateText.text = "MONITORING";

                targetText.text = $"{target}";

                playerReport = null;

                //Rotating at degreesPerSec relative to the World Space
                Self.transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime, Space.Self);

                //Comparing the Y-axis rotation between the camera and it's Maximum allowed Y rotation

                if (transform.rotation.eulerAngles.y <= rotationMax.y && @bool == true)
                {
                    print("Going over maximum allowed rotation / POSITIVE");

                    degreesPerSec = -degreesPerSec;
                }
                else if (transform.rotation.eulerAngles.y <= rotationMin.y && @bool == true)
                {
                    print("Going over minimum allowed rotation / NEGATIVE");

                    //degreesPerSec = -degreesPerSec;
                }

                else if (distanceToPlayer <= lookRadius)
                {
                    cameraStateMachine = CamStates.FOCUSED;
                }

                break;
            #endregion

            #region Focused State
            //When the camera sees the player / FOCUSED
            case CamStates.FOCUSED:
                stateText.text = $"{cameraStateMachine}";



                target = player.transform;

                stateText.text = "TARGET FOUND";

                targetText.text = $"{target}";

                playerReport = player.transform.position;

                FaceTarget();

                //if (distanceToPlayer >= lookRadius)
                //{
                //    print($"Resetting to {originalRotation}");

                //    transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * rotationSpeed);

                //    cameraStateMachine = CamStates.MONITORING;
                //}

                break;
            #endregion

            #region Defualt state
            default:
                stateText.text = "State Not Found";

                break;
            #endregion
        }
        #endregion

        #endregion


    #region Old Code
        //if (distanceToPlayer <= lookRadius)
        //{
        //    target = player.transform;

        //    stateText.text = "TARGET FOUND";

        //    targetText.text = $"{target}";

        //    playerReport = player.transform.position;

        //    FaceTarget();
        //}
        //else
        //{
        //target = null;

        //stateText.text = "MONITORING";

        //targetText.text = $"{target}";

        //playerReport = null;

        ////Rotating at degreesPerSec relative to the World Space
        //Self.transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime, Space.World);

        ////Comparing the Y-axis rotation between the camera and it's Maximum allowed Y rotation
        //if (transform.rotation.eulerAngles.y <= rotationMaxY.y)
        //{
        //    print("Going over maximum allowed rotation / POSITIVE");
        //}
        //else if (transform.rotation.eulerAngles.y <= rotationMinY.y)
        //{
        //    print("Going over minimum allowed rotation / NEGATIVE");
        //}
        //}

    }//End Update
    #endregion

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

    private void OnDrawGizmos()
    {
        //Drawing a magenta sphere
        Gizmos.color = Color.magenta;
        //Sphere radius tied to Look Radius variable
        Gizmos.DrawWireSphere(Self.transform.position, lookRadius);

        //Gizmos.color = Color.red;
        //Gizmos.DrawFrustum(transform.position, fov, maxRange, minRange, aspect);

        //Using this raycast to determine the actual forward vector of the camera object in scene
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Self.transform.position, Self.transform.forward * lookRadius);
    }//End OnDrawGizmos

    #endregion
}
