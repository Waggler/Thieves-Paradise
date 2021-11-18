using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource uIAudio;

    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip mainMenuClip;
    [SerializeField] private AudioClip gameSceneClip;


    #endregion Components


    #region Methods


    //-------------------------//
    public void PlayButtonClick()
    //-------------------------//

    {
        uIAudio.PlayOneShot(buttonClip);

    }//END PlayButtonClick

    //-------------------------//
    public void PlayMainMenuMusic()
    //-------------------------//

    {


    }//END PlayMainMenuMusic


    #endregion Methods


}//END AudioManager
