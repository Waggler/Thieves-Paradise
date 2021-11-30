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

    public void InvertX(int value)
    {
        if (value == 0)
        {
            PlayerPrefs.SetInt("InvertHorizontalToggle", 0);
            vCam.m_XAxis.m_InvertInput = false;

        }
        else if (value == 1)
        {
            PlayerPrefs.SetInt("InvertHorizontalToggle", 1);
            vCam.m_XAxis.m_InvertInput = true;

        }

    }//ENDInvertX

    public void InvertY(int value)
    {
        if (value == 0)
        {
            PlayerPrefs.SetInt("InvertVerticalToggle", 0);
            vCam.m_YAxis.m_InvertInput = false;

        }
        else if (value == 1)
        {
            PlayerPrefs.SetInt("InvertVerticalToggle", 1);
            vCam.m_YAxis.m_InvertInput = true;

        }

    }//ENDInvertX

}//END CameraToggler
