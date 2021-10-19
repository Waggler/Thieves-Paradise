using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenMenuManager : MonoBehaviour
{
    //-----------------------//
    public void ChangeScreen(int screenValue)
    //-----------------------//
    {

        if (screenValue == 0)
        {
            Debug.Log("Back to Main Menu");

        }
        else if (screenValue == 1)
        {
            Debug.Log("Quit Game");
            Application.Quit();

        }

    }//END ChangeScreen

}//END LoseScreenMenuManager
