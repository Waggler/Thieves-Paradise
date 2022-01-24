using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PassiveReciever : MonoBehaviour
{
    public UnityEvent triggeredEvent;

    public void Activate()
    {
        triggeredEvent.Invoke();
    }
}
