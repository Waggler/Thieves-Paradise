using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent onPress;
    public bool isLocked;
    public string keyItem;

    [SerializeField] private AudioClip confirm;
    [SerializeField] private AudioClip deny;
    public void PressButton()
    {
        if(!isLocked)
        {
            //play confirm sound
            onPress.Invoke();
        }
    }

    public void Unlock()
    {
        isLocked = false;
        //put logic for changing visuals here
        //play confirm sound
        //PressButton();
    }

    public void Deny()
    {
        //play deny sound

    }
}
