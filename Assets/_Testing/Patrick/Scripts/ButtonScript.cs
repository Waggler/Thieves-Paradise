using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent onPress;
    public bool isLocked;
    public void PressButton()
    {
        if(!isLocked)
        {
            onPress.Invoke();
        }
    }

    public void Unlock()
    {
        isLocked = false;
        //put logic for changing visuals here
    }
}
