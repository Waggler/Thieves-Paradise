using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveInteractiveReciever : MonoBehaviour
{
    public bool isLocked;

    //add in variable and logic to check for keys to unlock it.

    UnityEvent ActivatedEffect;
    
    public void RecievePlayerInput()
    {
        if (!isLocked)
        {
            ActivatedEffect.Invoke();
        }
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
