using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool inArea = false;
    public GameObject message;
    public bool buttonPressed = false;
    public DoorTrigger trigger1;
    public DoorTrigger trigger2;
    public GameObject door;
    public InputManager im;

    float minAngle = 0.0f;
    float maxAngle = 0.0f;
    
    private void OnTriggerStay(Collider collider)
    {
        inArea = true;
        message.SetActive(true);

        if(Input.GetButtonDown("e") && inArea == true)
        {
            buttonPressed = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(im.isSprinting == true)
        {
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
            door.transform.eulerAngles = new Vector3(0, angle, 0);
            door.transform.position = new Vector3(7.3f, 2.1f, 7.16f);
            message.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        inArea = false;
        message.SetActive(false);
        buttonPressed = false;
        float angle = Mathf.LerpAngle(maxAngle, minAngle, Time.time);
        door.transform.eulerAngles = new Vector3(0, angle, 0);
    }
}
