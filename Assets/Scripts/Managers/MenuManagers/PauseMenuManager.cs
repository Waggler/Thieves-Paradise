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
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject photoScreen;


    public static bool isGamePaused;

    #endregion Components


    #region Methods


    //-----------------------//
    public void PauseGame()
    //-----------------------//
    {
        if (isGamePaused)
        {
            ResumeGame();
            return;
        }
        
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        photoScreen.SetActive(false);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        isGamePaused = true;

    }//END PauseGame


    //-----------------------//
    public void ResumeGame()
    //-----------------------//
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        photoScreen.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        isGamePaused = false;

    }//END ResumeGame

    //-----------------------//
    public void ChangeScreen(int screenValue)
    //-----------------------//
    {
        if (screenValue == 0)
        {
            pauseMenu.SetActive(false);
            //loadMenu.SetActive(true);

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
