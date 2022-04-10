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
    [Tooltip("If you fill this make sure to give it materials for locked and unlocked state")]
    [SerializeField] private GameObject[] lightObj;
    [SerializeField] private Material lightMatLocked;
    [SerializeField] private Material lightMatUnlocked;
    private Material lightMat;

    void Start()
    {
        if (lightObj.Length > 0)
        {
            //lightMat = lightObj.GetComponent<Renderer>().material;
            foreach (GameObject i in lightObj)
            {
                if(isLocked)
                {
                    i.GetComponent<Renderer>().material = lightMatLocked;
                }else
                {
                    i.GetComponent<Renderer>().material = lightMatUnlocked;
                }
            }  
        }
    }
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

        if (lightObj != null)
        {
            foreach (GameObject i in lightObj)
            {
                i.GetComponent<Renderer>().material = lightMatUnlocked;
            } 
        }
        PressButton();
    }

    public void Deny()
    {
        aSource.PlayOneShot(denyClip);

    }
}
