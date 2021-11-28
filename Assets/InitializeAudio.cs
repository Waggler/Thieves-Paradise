using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeAudio : MonoBehaviour
{

    //-------------------------//
    void Awake()
    //-------------------------//
    {
        SetDefaultVolume();

    }//END Awake

    //-------------------------//
    void SetDefaultVolume()
    //-------------------------//
    {
        PlayerPrefs.SetFloat("MasterAudio", -25);
        PlayerPrefs.SetFloat("MusicAudio", -25);
        PlayerPrefs.SetFloat("SFXAudio", -25);

        this.gameObject.SetActive(false);

    }//END SetDefaultVolume

}//END InitializeAudio
