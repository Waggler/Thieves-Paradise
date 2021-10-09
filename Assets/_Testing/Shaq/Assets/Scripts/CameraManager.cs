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
    [SerializeField] private float trackSpeed;
    [SerializeField] private Vector3 rotationMaxY;
    [SerializeField] private Vector3 rotationMinY;
    [SerializeField] private Vector3 rotationMaxX;
    [SerializeField] private Vector3 rotationMinX;




    #endregion

    #region Enumerations
    private enum CamStates
    {
        MONITORING,
        FOCUSED
    }

    [SerializeField] CamStates cameraStateMachine;

    #endregion

    #region Awake & Update

    void Awake()
    {
        stateText.text = "";
        targetText.text = "";

    }//End Awake

    void Update()
    {
        //Can change this variable type to Transform if need be
        //The ? at the end of Vector3 just means that it is now a nullable Vector3 variable
        //By default Vector3 variables cannot have a null value outside of being null on Awake() / Start
        Vector3? playerReport;

        transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime);

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= lookRadius)
        {
            target = player.transform;

            stateText.text = "TARGET FOUND";

            targetText.text = $"{target}";

            playerReport = player.transform.position;
            
            FaceTarget();
        }
        else
        {
            target = null;

            stateText.text = "MONITORING";

            targetText.text = $"{target}";

            playerReport = null;

            //Rotating at degreesPerSec relative to the World Space
            Self.transform.Rotate(new Vector3(0, degreesPerSec, 0) * Time.deltaTime, Space.World);

            //Comparing the Y-axis rotation between the camera and it's Maximum allowed Y rotation
            if (transform.rotation.eulerAngles.y <= rotationMaxY.y)
            {
                print("Going over maximum allowed rotation / POSITIVE");
            }
            else if (transform.rotation.eulerAngles.y <= rotationMinY.y)
            {
                print("Going over minimum allowed rotation / NEGATIVE");
            }
        }

        switch (cameraStateMachine)
        {
            case CamStates.MONITORING:
                stateText.text = $"{cameraStateMachine}";

                break;
            case CamStates.FOCUSED:
                stateText.text = $"{cameraStateMachine}";

                break;
            default:
                stateText.text = "State Not Found";

                break;
        }
    }//End Update
    #endregion


    #region General Functions
    //---------------------------------//
    //Function that makes the object face it's target
    void FaceTarget()
    {
        //Space.World
        Quaternion rotationTarget;

        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, direction.y, direction.z));

        rotationTarget = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * trackSpeed);

        //try not to add Time.deltaTime to this as it will result in the Camera needing to use it's rotational Z-axis
        // this will result in an unusual appearance in the final game
        transform.rotation = lookRotation;




        //Use this if you want the camera to spin like hell
        //transform.Rotate(rotationTarget.eulerAngles, Space.World);

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
