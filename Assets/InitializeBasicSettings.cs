using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBasicSettings : MonoBehaviour
{

    //-------------------------//
    void Awake()
    //-------------------------//
    {
        SetDefaultVolume();
        SetDefaultSettings();

    }//END Awake

    //-------------------------//
    void SetDefaultVolume()
    //-------------------------//
    {
        PlayerPrefs.SetFloat("MasterAudio", 0);
        PlayerPrefs.SetFloat("MusicAudio", 0);
        PlayerPrefs.SetFloat("SFXAudio", 0);

    }//END SetDefaultVolume

    //-------------------------//
    void SetDefaultSettings()
    //-------------------------//
    {
        PlayerPrefs.SetInt("CrouchToggle", 1);
        PlayerPrefs.SetInt("SprintToggle", 0);
        PlayerPrefs.SetInt("InvertVerticalToggle", 0);
        PlayerPrefs.SetInt("InvertHorizontalToggle", 0);
        PlayerPrefs.SetInt("FlipUI", 0);
        PlayerPrefs.SetInt("thirtyFPSON", 0);


    }//END SetDefaultVolume

}//END InitializeAudio
