using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public bool inArea = false;
    
    private void OnTriggerStay(Collider collider)
    {
        inArea = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        inArea = false;
    }
}
