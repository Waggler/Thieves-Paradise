using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class DoorOpen : MonoBehaviour
{
    //Put this script on side away from other box trigger
    [SerializeField]

    public GameObject door;
    public GameObject message;
    public bool button1Pressed = false;
    public bool button2Pressed = false;
    public bool button3Pressed = false;
    public bool doorOpens = false;
    public PlayerMovement pm;
    public InputManager im;
    // public DoorTrigger trigger1;
    // public DoorTrigger trigger2;
    public bool inArea = false;
    // private InputAction action;
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
    public bool normalOpen = false;
    public bool SprintOpen = false;
    public bool crouch1Open = false;
    public bool crouch2Open = false;
    public bool crouch3Open = false;
    private Animation anim;
    public float Timer = 0f;
    //REMEMBER TP ATTEMPT TO PUT DOOR-OPENING CODE IN UPDATE AND TO USE A TIMER WITH TIME.DELTA TIME FOR OPENING DOOR
    
    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(false);
        // action.performed += _ => OpenDoor();
        anim = GetComponent<Animation>();
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
            maxAngle = doorWalkOpen;
            //-90
            //anim.Play("DoorOpening1");
        }

        // if(doorOpens == false)
        // {
        //     anim.Play("DoorClosing1");
        // }

        // else
        // {
        //     maxAngle = 0.0f;
        // }

        if(im.isSprinting == true)
        {
            maxAngle = doorSprintOpen;
            //preferred angle = -151.55f
        }

        // if(crouchOpen1 == true)
        // {
        //     maxAngle = -37.06f;
        // }
        // if(crouchOpen2 == true)
        // {
        //     maxAngle = -48.15f;
        // }
        // if(crouchOpen3 == true)
        // {
        //     maxAngle = -71.3f;
        // }

        // if(im.isCrouching == true)
        // {
        //     maxAngle = 75.44f;
        // }
     
        // if(trigger2.inArea == true)
        // {
        //     if(doorOpens == true)
        //     {
        //         maxAngle = -90f;
        //     }

        //     else
        //     {
        //         maxAngle = 0.0f;
        //     }

        //     if(im.isSprinting == true)
        //     {
        //         maxAngle = 151.55f;
        //     }

        //     if(im.isCrouching == true)
        //     {
        //         maxAngle = -75.44f;
        //     }
        // }

        // if(trigger1.inArea == false || trigger2.inArea == false)
        // {
        //     maxAngle = 0.0f;
        // }

        // if(trigger1.buttonPressed == true || trigger2.buttonPressed == true)
        // {
        //     buttonPressed = true;
        // }

        // if(trigger1.buttonPressed == false || trigger2.buttonPressed == false)
        // {
        //     buttonPressed = false;
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
            maxAngle = doorCrouchOpen1;
            //preferred angle = -37.06f
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
            maxAngle = doorCrouchOpen2;
            //preferred angle = -48.15f
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
            maxAngle = doorCrouchOpen3;
            //preferred angle = -71.3f
        }

        // OnTriggerStay(collider);
        // OnTriggerEnter(collider);
        // OnTriggerExit(collider);

        if(normalOpen == true)
        {
            
            Timer = Timer + Time.deltaTime;

            if(Timer < 1.00f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                door.transform.eulerAngles = new Vector3(0, angle, 0);
                doorOpens = true;
            }
            if(Timer >= 1.00f)
            {
                Timer = 1.00f;
            }
        }

        // if(normalOpen == false)
        // {
        //     Timer = Timer - Time.deltaTime;
        // }
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

        if(inArea == true && button1Pressed == true && im.isSprinting == false)
        {
            // door.transform.position = new Vector3(8.34f, 2.1f, 7.17f);
            
            
            // door.transform.eulerAngles = new Vector3(0f, -76.942f, 0f);
            message.SetActive(false);
            //doorOpens = true;
            normalOpen = true;

            // if(im.isCrouching == true && crouchOpen1 == true && crouchOpen2 == false && crouchOpen3 == false)
            // {
            //     crouchOpen2 = true;
            //     crouchOpen1 = false;
            //     crouchOpen3 = false;
            // }
            // if(im.isCrouching == true && crouchOpen1 == false && crouchOpen2 == true && crouchOpen3 == false)
            // {
            //     crouchOpen3 = true;
            //     crouchOpen2 = false;
            //     crouchOpen1 = false;
            // }
        }

        // if(inArea == true && button2Pressed == true && im.isSprinting == false)
        // {
        //     if(im.isCrouching == true && crouchOpen1 == true && crouchOpen2 == false && crouchOpen3 == false)
        //     {
        //         crouchOpen2 = true;
        //         crouchOpen1 = false;
        //         crouchOpen3 = false;
        //     }
        // }

        //  if(inArea == true && button3Pressed == true && im.isSprinting == false)
        // {
        //     if(im.isCrouching == true && crouchOpen1 == false && crouchOpen2 == true && crouchOpen3 == false)
        //     {
        //         crouchOpen3 = true;
        //         crouchOpen2 = false;
        //         crouchOpen1 = false;
        //     }
        // }

        // if(trigger2.inArea == true && buttonPressed == true && im.isSprinting == false)
        // {
        //     float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
        //     door.transform.eulerAngles = new Vector3(0, angle, 0);
        //     door.transform.position = new Vector3(8.34f, 2.1f, 5.13f);
            
            
        //     // door.transform.eulerAngles = new Vector3(0f, -76.942f, 0f);
        //     message.SetActive(false);
        //     doorOpens = true;
        // }
    } //END NORMAL DOOR OPEN
    #endregion

    private void OnTriggerEnter(Collider collider)
    {
        if(im.isSprinting == true)
        {
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            door.transform.eulerAngles = new Vector3(0, angle, 0);
            // door.transform.position = new Vector3(7.3f, 2.1f, 7.16f);
            message.SetActive(false);
        }
    }

    #region LeaveDoor
    private void OnTriggerExit(Collider collider)
    {
        // if(inArea == true)
        // {
            float angle = Mathf.LerpAngle(maxAngle, minAngle, Time.time);
            //door.transform.eulerAngles = new Vector3(0, angle, 0);
            inArea = false;
            door.transform.eulerAngles = new Vector3(0f, 0f, 0f);
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
            normalOpen = false;
        // }
        message.SetActive(false);
    }   //END LEAVE DOOR TRIGGER
    #endregion
}
