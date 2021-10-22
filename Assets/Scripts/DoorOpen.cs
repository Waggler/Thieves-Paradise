using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class DoorOpen : MonoBehaviour
{
    [SerializeField]

    public bool inArea = false;
    public GameObject door;
    public GameObject message;
    public bool buttonPressed = false;
    public bool doorOpens = false;
    public PlayerMovement pm;
    public InputManager im;
    // private InputAction action;
    
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

        else
        {
            maxAngle = 0.0f;
        }

        if(im.isSprinting == true)
        {
            maxAngle = -151.55f;
        }

        if(pm.IsCrouching == true)
        {
            maxAngle = 75.44f;
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

        if(Input.GetButtonDown("e"))
        {
            buttonPressed = true;
        }

        if(inArea == true && buttonPressed == true && im.isSprinting == false)
        {
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            transform.eulerAngles = new Vector3(0, angle, 0);
            door.transform.position = new Vector3(8.34f, 2.1f, 7.17f);
            
            
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
            transform.eulerAngles = new Vector3(0, angle, 0);
            door.transform.position = new Vector3(7.3f, 2.1f, 7.16f);
            message.SetActive(false);
        }
    }

    #region LeaveDoor
    private void OnTriggerExit(Collider collider)
    {
        // if(inArea == true)
        // {
            float angle = Mathf.LerpAngle(maxAngle, minAngle, Time.time);
            transform.eulerAngles = new Vector3(0, angle, 0);
            inArea = false;
            // door.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            door.transform.position = new Vector3(9.334243f, 2.1f, 6.08f);
            buttonPressed = false;
            doorOpens = false;
        // }
        message.SetActive(false);
    }   //END LEAVE DOOR TRIGGER
    #endregion
}
