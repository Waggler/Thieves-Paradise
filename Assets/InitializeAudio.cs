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
        PlayerPrefs.SetFloat("MasterAudio", 0);
        PlayerPrefs.SetFloat("MusicAudio", 0);
        PlayerPrefs.SetFloat("SFXAudio", 0);

        this.gameObject.SetActive(false);

    }//END SetDefaultVolume

}//END InitializeAudio
