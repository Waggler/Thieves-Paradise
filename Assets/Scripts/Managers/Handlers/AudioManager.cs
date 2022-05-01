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
            combatMusicSource.volume += ((Time.deltaTime / 5) * 0.001f);
            musicSource.volume -= ((Time.deltaTime / 5) * 0.001f);
        }

        //float elapsedTime = 0;
        //float duration = 5;

        //while (elapsedTime < duration)
        //{
        //    combatMusicSource.volume = Mathf.Lerp(0, 1, elapsedTime / 5);
        //    musicSource.volume = Mathf.Lerp(1, 0, elapsedTime / 5);

        //    elapsedTime += Time.deltaTime;

        //}
        yield return null;

    }

    //-------------------------//
    IEnumerator IEndCombatMusic()
    //-------------------------//
    {
        while (musicSource.volume < 1)
        {
            combatMusicSource.volume -= ((Time.deltaTime / 5) * 0.001f);
            musicSource.volume += ((Time.deltaTime / 5) * 0.001f);
        }
        yield return null;

        //if (musicSource.volume > 1)
        //{
        //    musicSource.volume = 1;
        //}

        //if (combatMusicSource.volume < 0)
        //{
        //    combatMusicSource.volume = 0;
        //}
    }


    #endregion Coroutines


}//END AudioManager
