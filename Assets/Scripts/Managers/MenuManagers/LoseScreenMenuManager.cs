using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseScreenMenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator loseAnimator;

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
        loseAnimator.SetBool("loseOpen", false);
        Time.timeScale = 1;


    }//END Init

    //-----------------------//
    public void LoseGame()
    //-----------------------//
    {
        loseAnimator.SetBool("loseOpen", true);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }//END LoseGame

    //-----------------------//
    public void LoadCheckpoint() //Remove after VS
    //-----------------------//
    {
        GameController.gameControllerInstance.LoadCheckPoint();

    }//END LoadCheckpoint


}//END LoseScreenMenuManager
