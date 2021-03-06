using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen2 : MonoBehaviour
{
    //Put this script on box trigger on other side of door from other trigger
    
   [SerializeField]

    public bool inArea = false;
    public GameObject door;
    public GameObject message;
    public bool button1Pressed = false;
    public bool button2Pressed = false;
    public bool button3Pressed = false;
    public bool doorOpens = false;
    public PlayerMovement pm;
    public InputManager im;
    // private InputAction action;
    public bool crouchOpen1 = false;
    public bool crouchOpen2 = false;
    public bool crouchOpen3 = false;
    public int buttonPressed = 0;
    public int buttonPressed2 = 0;
    public int buttonPressed3 = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(false);
        // action.performed += _ => OpenDoor();
    }

    // private void OnEnable()
    // {
    //     action.Enable();
    // }

    // private void OnDisable()
    // {
    //     action.Disable();
    // }

    // private void OpenDoor()
    // {
        
    //     else
    //     {
            
    //     }
    // }

    float minAngle = 0.0f;
    float maxAngle = 0.0f;


    // Update is called once per frame
    void Update()
    {
        if(doorOpens == true)
        {
            maxAngle = 90f;
        }

        // else
        // {
        //     maxAngle = 0.0f;
        // }

        if(im.isSprinting == true)
        {
            maxAngle = 151.55f;
        }

        // if(im.isCrouching == true)
        // {
        //     maxAngle = -75.44f;
        // } 

        if(crouchOpen1 == true)
        {
            buttonPressed++;
            message.SetActive(false);
        }

        if(buttonPressed > 1)
        {
            buttonPressed = 1;
        }

        if(buttonPressed == 1)
        {
            maxAngle = 37.06f;
            // buttonPressed = 0;
        }

        if(crouchOpen2 == true)
        {
            buttonPressed2++;
        }

        if(buttonPressed2 > 1)
        {
            buttonPressed2 = 1;
        }

        if(buttonPressed2 == 1)
        {
            maxAngle = 48.15f;
        }

        if(crouchOpen3 == true)
        {
            buttonPressed3++;
        }

        if(buttonPressed3 > 1)
        {
            buttonPressed3 = 1;
        }

        if(buttonPressed3 == 1)
        {
            maxAngle = 71.3f;
        }
    }

    #region OpenDoor
    private void OnTriggerStay(Collider collider)
    {
        inArea = true;
        message.SetActive(true);

        if(im.isSprinting == true)
        {
            message.SetActive(false);
        }

        if(Input.GetKey("e") && im.isCrouching == true)
        {
            crouchOpen1 = true;
        }

        if(Input.GetButtonDown("e"))
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

        if(inArea == true && button1Pressed == true && im.isSprinting == false)
        {
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            door.transform.eulerAngles = new Vector3(0, angle, 0);
            // door.transform.position = new Vector3(8.34f, 2.1f, 5.13f);
            
            
            // door.transform.eulerAngles = new Vector3(0f, -76.942f, 0f);
            message.SetActive(false);
            doorOpens = true;
        }
    } //END NORMAL DOOR OPEN
    #endregion

    private void OnTriggerEnter(Collider collider)
    {
        if(im.isSprinting == true)
        {
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            door.transform.eulerAngles = new Vector3(0, angle, 0);
            // door.transform.position = new Vector3(7.3f, 2.1f, 5.63f);
            message.SetActive(false);
        }
    }

    #region LeaveDoor
    private void OnTriggerExit(Collider collider)
    {
        // if(inArea == true)
        // {
            float angle = Mathf.LerpAngle(maxAngle, minAngle, Time.time);
            door.transform.eulerAngles = new Vector3(0, angle, 0);
            inArea = false;
            // door.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            // door.transform.position = new Vector3(9.334243f, 2.1f, 6.08f);
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
        // }
        message.SetActive(false);
    }   //END LEAVE DOOR TRIGGER
    #endregion
}