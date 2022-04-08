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
    [SerializeField] private GameObject light;
    private Material lightMat;

    void Start()
    {
        if (light != null)
        {
            lightMat = light.GetComponent<Renderer>().material;
            lightMat.EnableKeyword("_Emission");

            if(isLocked)
            {
                lightMat.SetColor("_EmissionColor", Color.red);
            }else
            {
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

        if (light != null)
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
