using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraToggler : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook vCam;

    [SerializeField] private Toggle xToggle;
    [SerializeField] private Toggle yToggle;

    void Awake()
    {
        vCam = (CinemachineFreeLook)FindObjectOfType(typeof(CinemachineFreeLook));
        //print(vCam.gameObject.name);

    }
    void Start()
    {
        if (PlayerPrefs.GetInt("InvertHorizontalToggle") == 0)
        {
            xToggle.isOn = false;

            vCam.m_XAxis.m_InvertInput = false;
        }
        else if (PlayerPrefs.GetInt("InvertHorizontalToggle") == 1)
        {
            xToggle.isOn = true;
            vCam.m_XAxis.m_InvertInput = true;

        }
        if (PlayerPrefs.GetInt("InvertVerticalToggle") == 0)
        {
            yToggle.isOn = false;
            vCam.m_YAxis.m_InvertInput = false;

        }
        else if (PlayerPrefs.GetInt("InvertHorizontalToggle") == 1)
        {
            yToggle.isOn = false;
            vCam.m_YAxis.m_InvertInput = true;

        }

        

    }

    public void InvertX()
    {
        vCam.m_XAxis.m_InvertInput = !vCam.m_XAxis.m_InvertInput;
        

        if (vCam.m_XAxis.m_InvertInput == false)
        {
            PlayerPrefs.SetInt("InvertHorizontalToggle", 0);
        }else
        {
            PlayerPrefs.SetInt("InvertHorizontalToggle", 1);
        }

        print("Toggling X Camera " + vCam.m_XAxis.m_InvertInput);
    }//ENDInvertX

    public void InvertY()
    {
        vCam.m_YAxis.m_InvertInput = !vCam.m_YAxis.m_InvertInput;

        if (vCam.m_YAxis.m_InvertInput == false)
        {
            PlayerPrefs.SetInt("InvertVerticalToggle", 0);
        }else
        {
            PlayerPrefs.SetInt("InvertVerticalToggle", 1);
        }
    }//ENDInvertX

}//END CameraToggler
