using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SettingsMenuManager settingsManager;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject missionSelectMenu;
    [SerializeField] private AudioManager audioManager;

    [Header("Buttons")]
    public Button playFirstButton;
    public Button playClosedButton;

    public Button settingsFirstButton;

    public Button creditsFirstButton;
    public Button creditsCloseButton;


    #region Methods


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
        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        audioManager.PlayMainMenuMusic();

        if (PlayerPrefs.GetInt("endCredits") == 1)
        {
            mainMenu.SetActive(false);
            creditsScreen.SetActive(true);

            PlayerPrefs.SetInt("endCredits", 0);
        }

        if(PlayerPrefs.GetInt("thirtyFPSON") == 1)
        {
            Application.targetFrameRate = 30;

            settingsManager.thirtyFPSButton.interactable = false;
            settingsManager.sixtyFPSButton.interactable = true;
        }
        else
        {
            Application.targetFrameRate = 60;

            settingsManager.thirtyFPSButton.interactable = true;
            settingsManager.sixtyFPSButton.interactable = false;
        }


    }//END Init


    //-----------------------//
    public void ChangeScreen(int screenValue)
    //-----------------------//
    {
        if(screenValue == 0)
        {
            mainMenu.SetActive(false);
            missionSelectMenu.SetActive(true);

            // Have to null before reset
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(playFirstButton.gameObject);

        } 
        else if (screenValue == 1)
        {
            SceneManager.LoadScene("Collectibles");


        }
        else if(screenValue == 2)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);

            // Have to null before reset
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(settingsFirstButton.gameObject);

        }
        else if (screenValue == 3)
        {
            mainMenu.SetActive(false);
            creditsScreen.SetActive(true);

            // Have to null before reset
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(creditsFirstButton.gameObject);


        }

    }//END ChangeScreen

    //-----------------------//
    public void QuitGame()
    //-----------------------//
    {

        Debug.Log("Quit Game!");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
                Application.Quit();
    #endif

    }//END QuitGame

    //Delete After Prototype

    //-----------------------//
    public void PlayGame()
    //-----------------------//
    {
        SceneManager.LoadScene(1);


    }//END PlayPrototype


    #endregion Methods


}//END MainMenuManager
