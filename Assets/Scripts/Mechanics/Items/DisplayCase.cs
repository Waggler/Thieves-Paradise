using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCase : MonoBehaviour
{
    public GameObject caseDoor;
    public Key key;
    public GameObject lockedMessage;
    public GameObject openMessage;
    public bool isLocked = true;
    public bool isOpened = false;
    public bool buttonPressed = false;
    public PlayerMovement pm;
    public InputManager im;
    public bool inArea = false;
    public bool leaveCase = false;
    public float doorOpen;
    public float doorClose;
    public float Timer = 0f;
    public bool reachedTimer = false;
    float minAngle = 0.0f;
    float maxAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(key.gotKey == true)
        {
            isLocked = false;
        }

        if(isOpened == true)
        {
            maxAngle = doorOpen;
        }

        if(buttonPressed == true)
        {
            Timer = Timer + Time.deltaTime;
            openMessage.SetActive(false);

            if(Timer <= 1.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                caseDoor.transform.eulerAngles = new Vector3(0, angle, 0);
                isOpened = true;
            }

            if(Timer >= 1.05f)
            {
                Timer = 1.05f;
            }
        }

        if(leaveCase == true)
        {
            maxAngle = doorClose;
        }

        if(leaveCase == true)
        {
            Timer = Timer - Time.deltaTime;
            minAngle = 0f;
            maxAngle = doorOpen;

            if(Timer >= -0.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                caseDoor.transform.eulerAngles = new Vector3(0, angle, 0);
            }

            if(Timer <= -0.05f)
            {
                Timer = -0.05f;
            }
        }
    }

    #region OpenDoor
    public void OnTriggerStay(Collider collider)
    {
        inArea = true;
        leaveCase = false;

        if(Input.GetKey("e") && isLocked == false)
        {
            buttonPressed = true;
        }
    }
    #endregion

    private void OnTriggerEnter(Collider collider)
    {
        if(isLocked == true)
        {
            lockedMessage.SetActive(true);
        }
        if(isLocked == false)
        {
            openMessage.SetActive(true);
        }
    }

    #region CloseCase
    private void OnTriggerExit(Collider collider)
    {
        if(isLocked == true)
        {
            lockedMessage.SetActive(false);
        }
        if(isLocked == false)
        {
            openMessage.SetActive(false);
        }

        inArea = false;
        isOpened = false;
        leaveCase = true;
        buttonPressed = false;
    }
    #endregion

    public void ShutCase()
    {
        leaveCase = true;
    }
}
