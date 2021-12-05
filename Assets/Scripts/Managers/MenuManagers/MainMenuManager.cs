using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private GameObject missionSelectMenu;
    [SerializeField] private AudioManager audioManager;




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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        audioManager.PlayMainMenuMusic();

    }//END Init


    //-----------------------//
    public void ChangeScreen(int screenValue)
    //-----------------------//
    {
        if(screenValue == 0)
        {
            mainMenu.SetActive(false);
            missionSelectMenu.SetActive(true);

        } 
        else if (screenValue == 1)
        {
            mainMenu.SetActive(false);
            loadScreen.SetActive(true);

        }
        else if(screenValue == 2)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);

        }
        else if (screenValue == 3)
        {
            mainMenu.SetActive(false);
            creditsScreen.SetActive(true);


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
    public void PlayPrototype()
    //-----------------------//
    {
        SceneManager.LoadScene(1);
        

    }//END QuitGame


    #endregion Methods


}//END MainMenuManager
