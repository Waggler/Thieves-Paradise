using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InitializeBasicSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private InputManager inputManager;
    [SerializeField] CinemachineFreeLook freeLookCam;


    //-------------------------//
    void Awake()
    //-------------------------//
    {
        Init();

    }//END Awake

    //-------------------------//
    void Init()
    //-------------------------//
    {
        if (mainMixer == null)
        {
            mainMixer = FindObjectOfType<AudioMixer>();
        }
        if (inputManager == null)
        {
            inputManager = FindObjectOfType<InputManager>();
        }
        try
        {
            if (freeLookCam == null)
            {
                freeLookCam = FindObjectOfType<CinemachineFreeLook>();
            }
        }
        catch
        {
            Debug.Log("No Cinemachine cam in scene!");
        }

        if (PlayerPrefs.GetInt("isDefaultSettingsSet") == 0)
        {
            SetDefaultSettings();
            PlayerPrefs.SetInt("isDefaultSettingsSet", 1);
        }
        else
        {
            SetSettings();
        }

    }//END Init

    //-------------------------//
    void SetDefaultSettings()
    //-------------------------//
    {
        //Setting Prefs
        //Audio
        PlayerPrefs.SetFloat("MasterAudio", 0);
        PlayerPrefs.SetFloat("MusicAudio", 0);
        PlayerPrefs.SetFloat("SFXAudio", 0);

        //Video
        PlayerPrefs.SetInt("Quality", 0);
        PlayerPrefs.SetFloat("Gamma", 50);
        PlayerPrefs.SetInt("Fullscreen", 1);

        //Prefs
        PlayerPrefs.SetInt("CrouchToggle", 1);
        PlayerPrefs.SetInt("SprintToggle", 1);
        PlayerPrefs.SetInt("InvertVerticalToggle", 0);
        PlayerPrefs.SetInt("InvertHorizontalToggle", 0);
        PlayerPrefs.SetInt("FlipUI", 0);
        PlayerPrefs.SetInt("thirtyFPSON", 0);
        PlayerPrefs.SetFloat("RadioHue", 0);
        PlayerPrefs.SetFloat("RadioSaturation", 0);
        PlayerPrefs.SetFloat("CamSensitivity", 0.5f);
        PlayerPrefs.SetFloat("ThrowSensitivity", 0.5f);

        SetSettings();

    }//END SetDefaultVolume

    //-------------------------//
    void SetSettings()
    //-------------------------//
    {
        //Audio
        mainMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume"));
        mainMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
        mainMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));

        //Video
        Screen.brightness = PlayerPrefs.GetFloat("Gamma");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
        if (PlayerPrefs.GetInt("Fullscreen") == 1)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

        //Preferences
        ///Set Crouch to whatever
        ///Set Sprint to whatever

        if (freeLookCam != null)
        {
            if(PlayerPrefs.GetInt("InvertVerticalToggle") == 0)
            {
                freeLookCam.m_XAxis.m_InvertInput = false;
            }
            else
            {
                freeLookCam.m_XAxis.m_InvertInput = true;
            }
            if (PlayerPrefs.GetInt("InvertHorizontalToggle") == 0)
            {
                freeLookCam.m_YAxis.m_InvertInput = false;
            }
            else
            {
                freeLookCam.m_YAxis.m_InvertInput = true;
            }

            freeLookCam.m_XAxis.m_MaxSpeed = 300 * PlayerPrefs.GetFloat("CamSensitivity");
            freeLookCam.m_YAxis.m_MaxSpeed = 2 * PlayerPrefs.GetFloat("CamSensitivity");
        }

        try //maybe swap for ref like the cam?
        {
            if (PlayerPrefs.GetInt("FlipUI") == 0)
            {
                //UI on left side

            }
            else
            {
                //UI on right side

            }
        }
        catch
        {
            Debug.Log("No UI in scene!");
        }

        if (inputManager != null)
        {
            inputManager.ChangeZoomLookSensitivity(PlayerPrefs.GetFloat("ThrowSensitivity"));

        }


    }//END SetDefaultVolume

}//END InitializeAudio
