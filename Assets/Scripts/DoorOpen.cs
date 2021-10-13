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
            maxAngle = 76.942f;
        }

        else
        {
            maxAngle = 0.0f;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        inArea = true;
        message.SetActive(true);

        if(Input.GetButtonDown("e"))
        {
            buttonPressed = true;
        }

        if(inArea == true && buttonPressed == true)
        {
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            transform.eulerAngles = new Vector3(0, angle, 0);
            // door.transform.eulerAngles = new Vector3(0f, -76.942f, 0f);
            door.transform.position = new Vector3(8.57f, 2.1f, 6.456132f);
            message.SetActive(false);
            doorOpens = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // if(inArea == true)
        // {
            float angle = Mathf.LerpAngle(maxAngle, minAngle, Time.time);
            transform.eulerAngles = new Vector3(0, angle, 0);
            inArea = false;
            // door.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            door.transform.position = new Vector3(9.334243f, 2.1f, 6.456132f);
            buttonPressed = false;
            doorOpens = false;
        // }
        message.SetActive(false);
    }
}
