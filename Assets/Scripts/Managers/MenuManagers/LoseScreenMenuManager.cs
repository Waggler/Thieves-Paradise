using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LoseScreenMenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator loseAnimator;


    [SerializeField] private Button firstButton;

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
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
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
