using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenMenuManager : MonoBehaviour
{

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
        Time.timeScale = 1;



    }//END Init

    //-----------------------//
    public void ChangeScreen(int screenValue)
    //-----------------------//
    {

        if (screenValue == 0)
        {
            SceneManager.LoadScene(0);

        }
        else if (screenValue == 1)
        {
            Debug.Log("Quit Game");
            Application.Quit();

        }

    }//END ChangeScreen

}//END WinScreenMenuManager
