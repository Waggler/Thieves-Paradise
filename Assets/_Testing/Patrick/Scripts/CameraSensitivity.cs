using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSensitivity : MonoBehaviour
{
    public CinemachineFreeLook cameraFreeLook;
    public InputManager im;

    
    void Awake()
    {
        cameraFreeLook = (CinemachineFreeLook)FindObjectOfType(typeof(CinemachineFreeLook));
        im = (InputManager)FindObjectOfType(typeof(InputManager));
    }
    public void ChangeSensitivity(float newSense)
    {
        //newSense should range from 0.5 to 2
        cameraFreeLook.m_XAxis.m_MaxSpeed = 300 * newSense;
        cameraFreeLook.m_YAxis.m_MaxSpeed = 2 * newSense;
    }

    public void ChangeThrowLook(float newSense)
    {
        im.ChangeZoomLookSensitivity(newSense);
    }
}
