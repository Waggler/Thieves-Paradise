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
    [SerializeField] private Button unionButton;
    [SerializeField] private Button massesButton;
    [SerializeField] private Button mafiaButton;
    [SerializeField] private Button finaleButton;
    [SerializeField] private Button ciaButton;


    #endregion Components


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
        if (PlayerPrefs.GetInt("isLadyComplete") == 0)
        {
            unionButton.interactable = false;
            massesButton.interactable = false;
        }
        else
        {
            unionButton.interactable = true;
            massesButton.interactable = true;
            ladyButton.interactable = false;
        }

        if (PlayerPrefs.GetInt("isUnionComplete") == 0 || PlayerPrefs.GetInt("isMassesComplete") == 0)
        {
            mafiaButton.interactable = false;
            ciaButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isUnionComplete") == 1 && PlayerPrefs.GetInt("isMassesComplete") == 1)
        {
            unionButton.interactable = false;
            massesButton.interactable = false;
            mafiaButton.interactable = true;
            ciaButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("isMafiaComplete") == 0 || PlayerPrefs.GetInt("isCIAComplete") == 0)
        {
            finaleButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isMafiaComplete") == 1 && PlayerPrefs.GetInt("isCIAComplete") == 1)
        {
            mafiaButton.interactable = false;
            ciaButton.interactable = false;
            finaleButton.interactable = true;

        }


    }//END Init

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
