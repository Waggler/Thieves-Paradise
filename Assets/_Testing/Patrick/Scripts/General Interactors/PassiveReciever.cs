using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveReciever : MonoBehaviour
{
    public UnityEvent triggeredEvent;
    [Tooltip("Don't have to assign this one, just use if you need an effect on exit")]
    public UnityEvent triggeredEventEnd;

    void Awake()
    {
        if (GetComponent<Rigidbody>() == null)
        {
            this.gameObject.AddComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void Activate()
    {
        print("Activating Functions");
        triggeredEvent.Invoke();
    }
    public void Deactivate()
    {
        triggeredEventEnd.Invoke();
    }
}
