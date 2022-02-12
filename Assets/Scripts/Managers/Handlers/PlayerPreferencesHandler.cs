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


    #region General


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


    #endregion General


    #region Mouse and Keyboard


    //-----------------------//
    public void SetForwardMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardForward", value);

    }//END SetForwardMK


    //-----------------------//
    public void SetBackwardMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardBackward", value);

    }//END SetBackwardMK


    //-----------------------//
    public void SetRightMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardRight", value);

    }//END SetRightMK


    //-----------------------//
    public void SetLeftMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardLeft", value);

    }//END SetLeftMK


    //-----------------------//
    public void SetJumpMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardJump", value);

    }//END SetJumpMK


    //-----------------------//
    public void SetSprintMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardSprint", value);

    }//END SetSprintMK


    //-----------------------//
    public void SetCrouchMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardCrouch", value);

    }//END SetCrouchMK


    //-----------------------//
    public void SetInteractMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardInteract", value);

    }//END SetInteractMK


    //-----------------------//
    public void SetInventoryOneMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardInventoryOne", value);

    }//END SetInventoryOneMK


    //-----------------------//
    public void SetInventoryTwoMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardInventoryTwo", value);

    }//END SetInventoryTwoMK


    //-----------------------//
    public void SetInventoryThreeMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardInventoryThree", value);

    }//END SetInventoryThreeMK


    //-----------------------//
    public void SetInventoryFourMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardInventoryFour", value);

    }//END SetInventoryFourMK


    //-----------------------//
    public void SetPrimaryMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardPrimary", value);

    }//END SetPrimaryMK


    //-----------------------//
    public void SetSecondaryMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardSecondary", value);

    }//END SetSecondaryMK


    //-----------------------//
    public void SetRotateCamMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardRotateCam", value);

    }//END SetRotateCamMK


    //-----------------------//
    public void SetCenterCamMK(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("KeyboardCenterCam", value);

    }//END SetCenterCamMK


    #endregion Mouse and Keyboard


    #region Controller


    //-----------------------//
    public void SetForwardXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Forward", value);

    }//END SetForwardXC


    //-----------------------//
    public void SetBackwardXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Backward", value);

    }//END SetBackwardXC


    //-----------------------//
    public void SetRightXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Right", value);

    }//END SetRightXC


    //-----------------------//
    public void SetLeftXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Left", value);

    }//END SetLeftXC


    //-----------------------//
    public void SetJumpXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Jump", value);

    }//END SetJumpXC


    //-----------------------//
    public void SetSprintXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Sprint", value);

    }//END SetSprintXC


    //-----------------------//
    public void SetCrouchXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Crouch", value);

    }//END SetCrouchXC


    //-----------------------//
    public void SetInteractXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("Controller Interact", value);

    }//END SetInteractXC


    //-----------------------//
    public void SetInventoryOneXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerInventoryOne", value);

    }//END SetInventoryOneXC


    //-----------------------//
    public void SetInventoryTwoXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerInventoryTwo", value);

    }//END SetInventoryTwoXC


    //-----------------------//
    public void SetInventoryThreeXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerInventoryThree", value);

    }//END SetInventoryThreeXC


    //-----------------------//
    public void SetInventoryFourXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerInventoryFour", value);

    }//END SetInventoryFourXC


    //-----------------------//
    public void SetPrimaryXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerPrimary", value);

    }//END SetPrimaryXC


    //-----------------------//
    public void SetSecondaryXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerSecondary", value);

    }//END SetSecondaryXC


    //-----------------------//
    public void SetRotateCamXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerRotateCam", value);

    }//END SetRotateCamXC


    //-----------------------//
    public void SetCenterCamXC(string value)
    //-----------------------//
    {
        PlayerPrefs.SetString("ControllerCenterCam", value);

    }//END SetCenterCamXC


    #endregion Controller


    #endregion Preferences


    #endregion Methods


}//END PlayerPreferencesHandler
