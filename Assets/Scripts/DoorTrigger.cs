using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool inArea = false;
    public GameObject message;
    public bool buttonPressed = false;
    
    private void OnTriggerStay(Collider collider)
    {
        inArea = true;
        message.SetActive(true);

        if(Input.GetButtonDown("e") && inArea == true)
        {
            buttonPressed = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        inArea = false;
        message.SetActive(false);
    }
}
