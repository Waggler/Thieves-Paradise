using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent onPress;
    public bool isLocked;
    public string keyItem;

    [SerializeField] private AudioClip confirmClip;
    [SerializeField] private AudioClip denyClip;
    [SerializeField] private AudioSource aSource;
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
        aSource.PlayOneShot(confirmClip);
        //PressButton();
    }

    public void Deny()
    {
        aSource.PlayOneShot(denyClip);

    }
}
