using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private PhotoScreenManager photoScreenManager;

    [Header("Screens")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject loadMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject photoScreen;


    public static bool isGamePaused;

    #endregion Components


    #region Methods


    //-----------------------//
    private void Update()
    //-----------------------//
    {
       /*               
        if (Input.GetButtonDown("Pause"))       //Remove this code once inputs are set up
        {
            if (isGamePaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        */
    }//END Update

    //-----------------------//
    void PauseGame()
    //-----------------------//
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);

        Time.timeScale = 0f;

        isGamePaused = true;

    }//END PauseGame


    //-----------------------//
    public void ResumeGame()
    //-----------------------//
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        loadMenu.SetActive(false);

        Time.timeScale = 1f;

        isGamePaused = false;

    }//END ResumeGame

    //-----------------------//
    public void ChangeScreen(int screenValue)
    //-----------------------//
    {
        if (screenValue == 0)
        {
            pauseMenu.SetActive(false);
            loadMenu.SetActive(true);

        }
        else if (screenValue == 1)
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        else if (screenValue == 2)
        {
            SceneManager.LoadScene(0);

        }
        else if (screenValue == 3)
        {
            pauseMenu.SetActive(false);

            photoScreenManager.RandomizeTag();
            photoScreen.SetActive(true);
        }


    }//END ChangeScreen


    #endregion Methods


}//END PauseMenuManager
