using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private AudioMixer mainMixer;

    [SerializeField] private SuspicionManager susManager;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource uIAudio;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip buttonHoverClip;
    [SerializeField] private AudioClip buttonClickClip;

    [Header("Dynamic Audio")]
    [SerializeField] private bool levelHasGuards;
    [SerializeField] private AudioClip levelClip;
    [SerializeField] private AudioClip spottedClip;



    #endregion Components


    #region Methods


    #region Startup


    //-------------------------//
    void Start()
    //-------------------------//
    {
        Init();

    }//END Start

    //-------------------------//
    void Init()
    //-------------------------//
    {
        float masterVolume = PlayerPrefs.GetFloat("MasterAudio");
        float musicVolume = PlayerPrefs.GetFloat("MusicAudio");
        float sfxVolume = PlayerPrefs.GetFloat("SFXAudio");

        mainMixer.SetFloat("masterVolume", masterVolume);
        mainMixer.SetFloat("musicVolume", musicVolume);
        mainMixer.SetFloat("sfxVolume", sfxVolume);

        if (levelHasGuards == true)
        {
            if(susManager == null)
            {
                susManager = FindObjectOfType<SuspicionManager>();
            }



        }

    }//END Init


    #endregion Startup


    #region SFX


    //-------------------------//
    public void PlayButtonHover()
    //-------------------------//
    {
        uIAudio.PlayOneShot(buttonHoverClip);

    }//END PlayButtonHover

    //-------------------------//
    public void PlayButtonClick()
    //-------------------------//
    {
        uIAudio.PlayOneShot(buttonClickClip);

    }//END PlayButtonClick

    //-------------------------//

    #endregion SFX


    #region Dynamic Music





    #endregion Dynamic Music


    #endregion Methods


}//END AudioManager
