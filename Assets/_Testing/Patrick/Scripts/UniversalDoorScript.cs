using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalDoorScript : MonoBehaviour
{
    public enum doorStates
    {
        [Tooltip("Automatic: Opens without player interaction")]
        Automatic,
        [Tooltip("Manual: Must have a Button rigged to the OpenDoorFromButton Function")]
        Manual,
    }

    public doorStates doorMode;
    [SerializeField] private GameObject doorParent;
    [Range(80, 180)]
    [SerializeField] private float maxOpenAngle = 90;
    [Range(50, 500)]
    [SerializeField] private float openSpeed = 100;

    public bool debugMode = false;

    private Vector3 maxOpenRotation;
    private Vector3 otherPos;
    private bool isOpen;
    private bool moveDirection; //true = forwards, false = backwards
    private float t = 0.5f;
    private float curDoorAngle = 0;

    private Transform minTrans, maxTrans;

    void Start()
    {
        maxOpenRotation = new Vector3 (0, maxOpenAngle, 0);
    }
    
    void Update()
    {
        if (isOpen && (curDoorAngle > -maxOpenAngle && curDoorAngle < maxOpenAngle))
        {   //door needs to open and is not fully open in either direction
            if (moveDirection)
            {
                curDoorAngle += Time.deltaTime * openSpeed;
                MoveDoor();
            }else
            {
                curDoorAngle -= Time.deltaTime * openSpeed;
                MoveDoor();
            }
        } else if (!isOpen && (curDoorAngle < -1f || curDoorAngle > 1f))
        {   //door needs to close and is not at center
            if (curDoorAngle > 0f)
            {
                curDoorAngle -= Time.deltaTime * openSpeed;
                MoveDoor();
            }else
            {
                curDoorAngle += Time.deltaTime * openSpeed;
                MoveDoor();
            }
        }
        
        if(debugMode) print("Door T Value: " + t + " Open State: " + isOpen);
    }

    private void MoveDoor()
    {
        doorParent.transform.localRotation = Quaternion.AngleAxis(curDoorAngle, Vector3.up);
    }

    public void OpenDoorFromButton()
    {
        doorMode = doorStates.Automatic;
        isOpen = true;
    }
    private void DetermineDirection()
    {
        float direction = this.transform.InverseTransformPoint(otherPos).z;
        if (direction > 0)
        {
            moveDirection = true;
        }else
        {
            moveDirection = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(debugMode) print("Something Entered Door Area");

        if (doorMode == doorStates.Automatic)
        {
            if(debugMode) print("Door in Automatic Mode");
            //trigger on players or guards
            if (other.tag == "Player" || other.tag == "Guard")
            {
                if(debugMode) print("Found Player or Guard");
                //figure out what side of the door they're on
                otherPos = other.transform.position;
                DetermineDirection();
                isOpen = true;
            }
        }else if (doorMode == doorStates.Manual)
        {
            //still open automatically for the guard
            if (other.tag == "Guard")
            {
                otherPos = other.transform.position;
                DetermineDirection();
                isOpen = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //automatically close the door when the player or a guard leaves the area
        if (other.tag == "Player" || other.tag == "Guard")
        {
            if(debugMode) print("Player or Guard left Door");
            isOpen = false;
        }
    }

}
