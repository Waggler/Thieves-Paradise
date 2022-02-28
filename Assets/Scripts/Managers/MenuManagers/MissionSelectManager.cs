using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionSelectManager : MonoBehaviour
{
    #region Components


    [Header("Components")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject missionMenu;

    [Header("Buttons")]
    [SerializeField] private Button ladyButton;
    [SerializeField] private Button massesButton;
    [SerializeField] private Button mafiaButton;



    #endregion Components


    #region Methods


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        //Init();

    }//END Start

    /*
    //-----------------------//
    private void Init()
    //-----------------------//
    {
        if (PlayerPrefs.GetInt("isLadyComplete") == 0)
        {
            massesButton.interactable = false;
        }
        else
        {
            massesButton.interactable = true;
            ladyButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("isMassesComplete") == 0)
        {
            mafiaButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isMassesComplete") == 1)
        {
            massesButton.interactable = false;
            mafiaButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("isMafiaComplete") == 1)
        {
            ladyButton.interactable = true;
            massesButton.interactable = true;
            mafiaButton.interactable = true;
        }



    }//END Init
    */
    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {
        mainMenu.SetActive(true);
        missionMenu.SetActive(false);

    }//END ChangeScreen

    //-----------------------//
    public void LoadMission(int sceneIndex)
    //-----------------------//
    {
        SceneManager.LoadScene(sceneIndex);

    }//END LoadMission


    #endregion Methods


}//END MissionSelectManager
