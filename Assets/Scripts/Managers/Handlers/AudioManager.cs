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
    [SerializeField] private AudioSource combatMusicSource;

    [SerializeField] private AudioSource uIAudio;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip buttonHoverClip;
    [SerializeField] private AudioClip buttonClickClip;

    [Header("Dynamic Audio")]
    [SerializeField] private float trackSwapTime;
    [SerializeField] private bool levelHasGuards;
    bool areGuardsHostile;


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
            areGuardsHostile = false;
            if(susManager == null)
            {
                susManager = FindObjectOfType<SuspicionManager>();
            }

            foreach (GameObject guard in susManager.guardsList)
            {
                EnemyManager temp;

                temp = guard.GetComponent<EnemyManager>();
                temp.guardHostile.AddListener(BeginCombatMusic);
                temp.guardNotHostile.AddListener(EndCombatMusic);
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


    void BeginCombatMusic()
    {
        StartCoroutine(IStartCombatMusic());
    }

    void EndCombatMusic()
    {
        List<EnemyManager.EnemyStates> guardStates = new List<EnemyManager.EnemyStates>();
        foreach (GameObject guard in susManager.guardsList)
        {
            EnemyManager temp;

            temp = guard.GetComponent<EnemyManager>();
            guardStates.Add(temp.stateMachine);
        }
        if (guardStates.Contains(EnemyManager.EnemyStates.HOSTILE) || 
            guardStates.Contains(EnemyManager.EnemyStates.RANGEDATTACK) || 
            guardStates.Contains(EnemyManager.EnemyStates.STUNNED) || 
            guardStates.Contains(EnemyManager.EnemyStates.SUSPICIOUS))
        {
            areGuardsHostile = true;
        }
        else
        {
            areGuardsHostile = false;
        }

        if (areGuardsHostile == false)
        {
            StartCoroutine(IEndCombatMusic());
        }

    }


    #endregion Dynamic Music


    #endregion Methods


    #region Coroutines


    //-------------------------//
    IEnumerator IStartCombatMusic()
    //-------------------------//
    {
        while (musicSource.volume > 0)
        {
            combatMusicSource.volume += (Time.deltaTime / trackSwapTime);
            musicSource.volume -= (Time.deltaTime / trackSwapTime);
             yield return new WaitForSeconds(Time.deltaTime);
        }


        if (musicSource.volume <= 0)
        {
            StopCoroutine(IStartCombatMusic());
        }
    }

    //-------------------------//
    IEnumerator IEndCombatMusic()
    //-------------------------//
    {
        while (musicSource.volume < 1)
        {
            combatMusicSource.volume -= (Time.deltaTime / trackSwapTime);
            musicSource.volume += (Time.deltaTime / trackSwapTime);

           yield return new WaitForSeconds(Time.deltaTime);
        }

        if (musicSource.volume >= 1)
        {
            StopCoroutine(IEndCombatMusic());
        }
    }


    #endregion Coroutines


}//END AudioManager
