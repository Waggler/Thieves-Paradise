using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsScreen;

    
    #region Methods


    //-----------------------//
    public void ChangeScreen(int screenValue)
    //-----------------------//
    {
        if (screenValue == 1)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);

        }
        else if(screenValue == 2)
        {
            mainMenu.SetActive(false);
            creditsScreen.SetActive(true);
        }

    }//END ChangeScreen

    //-----------------------//
    public void QuitGame()
    //-----------------------//
    {
        Application.Quit();

        Debug.Log("Quit Game!");

    }//END QuitGame

    //-----------------------//
    public void PlayGame()
    //-----------------------//
    {
        Debug.Log("Play was clicked.");

    }//END QuitGame


    #endregion Methods


}//END MainMenuManager
