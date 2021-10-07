using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [Header("Screens")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    public static bool isGamePaused;

    #endregion Components


    #region Methods


    //-----------------------//
    private void Update()
    //-----------------------//
    {
        if (Input.GetButtonDown("Pause"))
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

        Time.timeScale = 1f;

        isGamePaused = false;

    }//END ResumeGame

    //-----------------------//
    public void OpenSettings()
    //-----------------------//
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);

    }//END OpenSettings

    //-----------------------//
    public void MainMenu()
    //-----------------------//
    {
        Debug.Log("Back to Main Menu");

    }//END MainMenu


    #endregion Methods


}//END PauseMenuManager
