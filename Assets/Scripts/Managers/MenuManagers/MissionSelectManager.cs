using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    [SerializeField] private Image massesPicture;
    [SerializeField] private Image mafiaPicture;

    [SerializeField] private Sprite massesActiveSprite;
    [SerializeField] private Sprite mafiaActiveSprite;




    public Button playClosedButton;



    #endregion Components


    #region Methods


    //-----------------------//
    private void OnEnable()
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
            massesButton.interactable = false;
        }
        else
        {
            massesButton.interactable = true;
            ladyButton.interactable = true;

            massesPicture.sprite = massesActiveSprite;
        }

        if (PlayerPrefs.GetInt("isMassesComplete") == 0)
        {
            mafiaButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isMassesComplete") == 1)
        {
            massesButton.interactable = true;
            mafiaButton.interactable = true;

            mafiaPicture.sprite = mafiaActiveSprite;

        }

        if (PlayerPrefs.GetInt("isMafiaComplete") == 1)
        {
            ladyButton.interactable = true;
            massesButton.interactable = true;
            mafiaButton.interactable = true;

            massesPicture.sprite = massesActiveSprite;
            mafiaPicture.sprite = mafiaActiveSprite;

        }



    }//END Init
    
    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {
        mainMenu.SetActive(true);
        missionMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playClosedButton.gameObject);

    }//END ChangeScreen

    //-----------------------//
    public void LoadMission(int sceneIndex)
    //-----------------------//
    {
        SceneManager.LoadScene(sceneIndex);

    }//END LoadMission


    #endregion Methods


}//END MissionSelectManager
