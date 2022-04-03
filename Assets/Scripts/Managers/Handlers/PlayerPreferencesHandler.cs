using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferencesHandler : MonoBehaviour
{


    #region Methods


    #region Startup


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    //-----------------------//
    private void Init()
    //-----------------------//
    {


    }//END Init



    #endregion Startup


    #region Audio


    //-----------------------//
    public void SetMasterAudioPref(float value)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("MasterAudio", value);

    }//END SetMasterAudioPref

    //-----------------------//
    public void SetSFXAudioPref(float value)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("SFXAudio", value);

    }//END SetSFXAudioPref

    //-----------------------//
    public void SetMusicAudioPref(float value)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("MusicAudio", value);

    }//END SetMusicAudioPref


    #endregion Audio


    #region Video


    //-----------------------//
    public void SetGammaPref(float value)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("Gamma", value);

    }//END SetGammaPref

    //-----------------------//
    public void SetResolutionPref(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("Resolution", value);

    }//END SetResolutionPref

    //-----------------------//
    public void SetQualityPref(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("Quality", value);

    }//END SetQualityPref

    //-----------------------//
    public void SetFullScreenPref(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("Fullscreen", value);

    }//END SetFullScreenPref


    #endregion Video


    #region Preferences


    //-----------------------//
    public void SetCrouchToggle(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("CrouchToggle", value);

    }//END SetCrouchToggle

    //-----------------------//
    public void SetSprintToggle(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("SprintToggle", value);

    }//END SetSprintToggle

    //-----------------------//
    public void SetVerticalToggle(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("InvertVerticalToggle", value);

    }//END SetVerticalToggle

    //-----------------------//
    public void SetHorizontalToggle(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("InvertHorizontalToggle", value);

    }//END SetHorizontalToggle

    //-----------------------//
    public void SetUIToggle(int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt("FlipUI", value);

    }//END SetUIToggle

    //-----------------------//
    public void SetRadioHue(float value)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("RadioHue", value);

    }//END SetRadioHue

    //-----------------------//
    public void SetRadioSaturation(float value)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("RadioSaturation", value);

    }//END SetRadioSaturation

    //-----------------------//
    public void SetFPS(int value)
    //-----------------------//
    {

        if (value == 0)
        {
            PlayerPrefs.SetInt("thirtyFPSON", 1);
        }
        if (value == 1)
        {
            PlayerPrefs.SetInt("thirtyFPSON", 0);

        }
    }//END SetFPS

    //-----------------------//
    public void SetLookSensitivity(float sensitivity)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("CamSensitivity", sensitivity);

    }//END LookSensitivity

    //-----------------------//
    public void SetThrowSensitivity(float sensitivity)
    //-----------------------//
    {
        PlayerPrefs.SetFloat("ThrowSensitivity", sensitivity);

    }//END LookSensitivity


    #endregion Preferences


    #endregion Methods


}//END PlayerPreferencesHandler
