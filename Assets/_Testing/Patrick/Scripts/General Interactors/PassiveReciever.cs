using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveReciever : MonoBehaviour
{
    public UnityEvent triggeredEvent;
    [Tooltip("Don't have to assign this one, just use if you need an effect on exit")]
    public UnityEvent triggeredEventEnd;

    public void Activate()
    {
        triggeredEvent.Invoke();
    }
    public void Deactivate()
    {
        triggeredEventEnd.Invoke();
    }
}
