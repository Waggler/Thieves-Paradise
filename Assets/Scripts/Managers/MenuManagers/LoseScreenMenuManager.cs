using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class LoseScreenMenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator loseAnimator;
    [SerializeField] private AudioSource loseSource;
    [SerializeField] private AudioMixer mainMixer;

    [SerializeField] private Button firstButton;

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
        loseAnimator.SetBool("loseOpen", false);
        Time.timeScale = 1;
        if (mainMixer == null)
        {
            mainMixer = FindObjectOfType<AudioMixer>();
        }

    }//END Init

    //-----------------------//
    public void LoseGame()
    //-----------------------//
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        loseAnimator.SetBool("loseOpen", true);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StartCoroutine(IPauseWorld());
        mainMixer.SetFloat("sfxVolume", -80);


    }//END LoseGame

    //-----------------------//
    public void LoadCheckpoint() //Remove after VS
    //-----------------------//
    {
        GameController.gameControllerInstance.LoadCheckPoint();

    }//END LoadCheckpoint

    IEnumerator IPauseWorld()
    {
        loseSource.Play();
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0;

    }


}//END LoseScreenMenuManager
