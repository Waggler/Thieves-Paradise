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

    // Update is called once per frame
    void Update()
    {
       
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
            door.transform.eulerAngles = new Vector3(0f, -76.942f, 0f);
            door.transform.position = new Vector3(8.57f, 2.1f, 6.456132f);
            message.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        // if(inArea == true)
        // {
            inArea = false;
            door.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            door.transform.position = new Vector3(9.334243f, 2.1f, 6.456132f);
            buttonPressed = false;
        // }
        message.SetActive(false);
    }
}
