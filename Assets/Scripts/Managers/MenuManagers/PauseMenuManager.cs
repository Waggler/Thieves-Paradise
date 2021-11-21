using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [Header("Screens")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject loadMenu;
    [SerializeField] private GameObject settingsMenu;

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


    }//END ChangeScreen


    /*      
//-------------------------//
public void IncreaseRosterSize()        //In case we want to make the save list hella big
//-------------------------//
{
    if (totalSaves >= 4)
    {
        saveListTransform.sizeDelta = new Vector2(saveListTransform.sizeDelta.x, saveListTransform.sizeDelta.y + 220f);
    }
    else if (totalSaves >= 7)
    {
        saveListTransform.sizeDelta = new Vector2(saveListTransform.sizeDelta.x, saveListTransform.sizeDelta.y + 220f);

    }
    else if (totalSaves >= 10)
    {
        saveListTransform.sizeDelta = new Vector2(saveListTransform.sizeDelta.x, saveListTransform.sizeDelta.y + 220f);

    }

}//END IncreaseRosterSize
*/


    #endregion Methods


}//END PauseMenuManager
