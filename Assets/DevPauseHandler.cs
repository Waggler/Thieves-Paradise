using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPauseHandler : MonoBehaviour
{
    //-----------------------//
    private void Update()
    //-----------------------//
    {
        /*               
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
         */
    }//END Update

    //-----------------------//
    public void PauseGame()
    //-----------------------//
    {
        if (Time.timeScale == 0)
        {
            ResumeGame();
            return;
        }
        Time.timeScale = 0f;

    }//END PauseGame


    //-----------------------//
    public void ResumeGame()
    //-----------------------//
    {
        Time.timeScale = 1f;

    }//END ResumeGame
}
