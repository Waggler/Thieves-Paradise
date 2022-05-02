using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private PhotoScreenManager photoScreenManager;

    [Header("Screens")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject photoScreen;


    [SerializeField] private Button pausedFirstButton;
    [SerializeField] private Button settingsFirstButton;
    [SerializeField] private Button photoModeFirstButton;

    private PlayerMovement playerMovement;
    private ScoreKeeper scoreKeeper;

    public static bool isGamePaused;

    #endregion Components

    void Start()
    {
        
    }

    #region Methods


    //-----------------------//
    public void PauseGame(InputAction.CallbackContext context)
    //-----------------------//
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (context.started)
        {
            if(playerMovement.hp <= 0 || scoreKeeper.gameisOver)
            {
                ResumeGame();
                return;
            }
            if (isGamePaused)
            {
                ResumeGame();
                return;
            }
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pausedFirstButton.gameObject);

            pauseMenu.SetActive(true);
            settingsMenu.SetActive(false);
            photoScreen.SetActive(false);

            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            isGamePaused = true;

            
        }

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

        }
        else if (screenValue == 1)
        {
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(settingsFirstButton.gameObject);
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

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(photoModeFirstButton.gameObject);
        }


    }//END ChangeScreen


    #endregion Methods


}//END PauseMenuManager
