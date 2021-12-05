using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    //message1 is the message that talks about the locked door
    public GameObject door;
    public bool isLocked = true;
    public bool inArea = false;
    public GameObject message1;
    public GameObject message2;
    public Key key;
    public bool button1Pressed = false;
    public bool button2Pressed = false;
    public bool button3Pressed = false;
    public bool doorOpens = false;
    public PlayerMovement pm;
    public InputManager im;
    public bool crouchOpen1 = false;
    public bool crouchOpen2 = false;
    public bool crouchOpen3 = false;
    public int buttonPressed = 0;
    public int buttonPressed2 = 0;
    public int buttonPressed3 = 0;
     //this is the value for the angle the door will open when approached normally
    public float doorWalkOpen;
    //this is the value for the angle the door will open when approached while sprinting
    public float doorSprintOpen;
    //these values are for the angles that the door opens while the player is crouching
    public float doorCrouchOpen1, doorCrouchOpen2, doorCrouchOpen3;
    public float leaveDoorClose;
    public bool normalOpen = false;
    public bool SprintOpen = false;
    public bool crouch1Open = false;
    public bool crouch2Open = false;
    public bool crouch3Open = false;
    public bool leaveDoor = false;
    public float Timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        message1.SetActive(false);
        message2.SetActive(false);
    }

    float minAngle = 0.0f;
    float maxAngle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(key.gotKey == true)
        {
            isLocked = false;
        }

        if(doorOpens == true)
        {
            maxAngle = doorWalkOpen;
            //-90f
        }

        if(im.isSprinting == true)
        {
            maxAngle = doorSprintOpen;
            //-151.55f
        }

        if(leaveDoor == true)
        {
            maxAngle = leaveDoorClose;
        }

        if(crouchOpen1 == true)
        {
            buttonPressed++;
            message2.SetActive(false);
            crouch1Open = true;
            crouch2Open = false;
            crouch3Open = false;
        }

        if(buttonPressed > 1)
        {
            buttonPressed = 1;
        }

        if(buttonPressed == 1)
        {
            maxAngle = doorCrouchOpen1;
            //-37.06f
            // buttonPressed = 0;
        }

        if(crouchOpen2 == true)
        {
            buttonPressed2++;
            crouch2Open = true;
            crouch1Open = false;
            crouch3Open = false;
        }

        if(buttonPressed2 > 1)
        {
            buttonPressed2 = 1;
        }

        if(buttonPressed2 == 1)
        {
            maxAngle = doorCrouchOpen2;
            //-48.15f
        }

        if(crouchOpen3 == true)
        {
            buttonPressed3++;
            crouch3Open = true;
            crouch1Open = false;
            crouch2Open = false;
        }

        if(buttonPressed3 > 1)
        {
            buttonPressed3 = 1;
        }

        if(buttonPressed3 == 1)
        {
            maxAngle = doorCrouchOpen3;
            //-71.3f
        }

        if(normalOpen == true)
        {
            
            Timer = Timer + Time.deltaTime;

            if(Timer <= 1.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                door.transform.eulerAngles = new Vector3(0, angle, 0);
                doorOpens = true;
            }
            if(Timer >= 1.05f)
            {
                Timer = 1.05f;
            }
        }

        if(SprintOpen == true)
        {
            Timer = Timer + Time.deltaTime;
            Timer = Timer + Time.deltaTime;

            if(Timer <= 1.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                door.transform.eulerAngles = new Vector3(0, angle, 0);
                doorOpens = true;
            }
            if(Timer >= 1.05f)
            {
                Timer = 1.05f;
            }
        }

        if(leaveDoor == true)
        {
            Timer = Timer - Time.deltaTime;
            minAngle = 0f;
            maxAngle = doorWalkOpen;
            
            // if(normalOpen == true)
            // {
            //     maxAngle = doorWalkOpen;
            // }
            
            if(Timer >= -0.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                door.transform.eulerAngles = new Vector3(0, angle, 0);
            }

            if(Timer <= -0.05f)
            {
                Timer = -0.05f;
            }
        }

        if(crouch1Open == true)
        {
            Timer = Timer + Time.deltaTime;

            if(Timer <= 1.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                door.transform.eulerAngles = new Vector3(0, angle, 0);
                doorOpens = true;
            }
            if(Timer >= 1.05f)
            {
                Timer = 1.05f;
            }
        }

        if(crouch2Open == true && Timer == 1.05f)
        {
            // if(Timer == 1.05f)
            // {
            //     Timer = 0f;
            // }
            Timer = Timer + Time.deltaTime;

            if(Timer <= 1.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                door.transform.eulerAngles = new Vector3(0, angle, 0);
                doorOpens = true;
            }
            if(Timer >= 1.05f)
            {
                Timer = 1.05f;
            }
        }

        if(crouch3Open == true && Timer == 1.05f)
        {
            Timer = Timer + Time.deltaTime;

            if(Timer <= 1.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                door.transform.eulerAngles = new Vector3(0, angle, 0);
                doorOpens = true;
            }
            if(Timer >= 1.05f)
            {
                Timer = 1.05f;
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        inArea = true;
        leaveDoor = false;

        if(im.isSprinting == true)
        {
            message2.SetActive(false);
        }

        if(Input.GetKey("e") && im.isCrouching == true && isLocked == false)
        {
            crouchOpen1 = true;
            normalOpen = false;
        }

        if(Input.GetKey("e"))
        {
            button1Pressed = true;
        }

        if(Input.GetKey("e") && buttonPressed == 1)
        {
            crouchOpen2 = true;
        }

        if(Input.GetKey("e") && buttonPressed2 == 1)
        {
            crouchOpen3 = true;
        }

        if(inArea == true && button1Pressed == true && im.isSprinting == false && isLocked == false)
        {
            // float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            // door.transform.eulerAngles = new Vector3(0, angle, 0);
            message2.SetActive(false);
            doorOpens = true;
            normalOpen = true;
        }
    }

    void OnTriggerEnter()
    {
        if(isLocked == true)
        {
            message1.SetActive(true);
        }
        if(isLocked == false)
        {
            message2.SetActive(true);
        }

        if(im.isSprinting == true && isLocked == false)
        {
            // float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            // door.transform.eulerAngles = new Vector3(0, angle, 0);
            // door.transform.position = new Vector3(7.3f, 2.1f, 7.16f);
            message2.SetActive(false);
            SprintOpen = true;
        }
    }

    void OnTriggerExit()
    {
        if(isLocked == true)
        {
            message1.SetActive(false);
        }
        if(isLocked == false)
        {
            message2.SetActive(false);
        }

        float angle = Mathf.LerpAngle(maxAngle, minAngle, Time.time);
        door.transform.eulerAngles = new Vector3(0, angle, 0);
        inArea = false;
        button1Pressed = false;
        button2Pressed = false;
        button3Pressed = false;
        doorOpens = false;
        crouchOpen1 = false;
        crouchOpen2 = false;
        crouchOpen3 = false;
        buttonPressed = 0;
        buttonPressed2 = 0;
        buttonPressed3 = 0;
        normalOpen = false;
        SprintOpen = false;
        leaveDoor = true;
    }
}
