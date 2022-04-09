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
    [SerializeField] private GameObject lightObj;
    private Material lightMat;

    void Start()
    {
        if (lightObj != null)
        {
            lightMat = lightObj.GetComponent<Renderer>().material;
            
            lightMat.EnableKeyword("_EMISSION");
            if(isLocked)
            {
                lightMat.color = Color.red;
                lightMat.SetColor("_EmissionColor", Color.red);
            }else
            {
                lightMat.color = Color.green;
                lightMat.SetColor("_EmissionColor", Color.green);
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
            lightMat.SetColor("_EmissionColor", Color.green);
        }
        PressButton();
    }

    public void Deny()
    {
        aSource.PlayOneShot(denyClip);

    }
}
