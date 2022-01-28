using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveActivator : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PassiveReciever>() != null)
        {
            other.GetComponent<PassiveReciever>().Activate();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PassiveReciever>() != null)
        {
            other.GetComponent<PassiveReciever>().Deactivate();
        }
    }
}
