using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectibleMenuManager : MonoBehaviour
{

    [Header("Components")]
    [Header("Buttons")]
    [SerializeField] private Button ladyButton;
    [SerializeField] private Button massesButton;
    [SerializeField] private Button unionButton;
    [SerializeField] private Button mafiaButton;
    [SerializeField] private Button ciaButton;
    [SerializeField] private Button finaleButton;

    [Space(5)]

    [Header("Screens")]
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject ladyScreen;
    [SerializeField] private GameObject unionScreen;
    [SerializeField] private GameObject massesScreen;
    [SerializeField] private GameObject mafiaScreen;
    [SerializeField] private GameObject ciaScreen;
    [SerializeField] private GameObject finaleScreen;


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
            ladyButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isLadyComplete") == 1)
        {
            ladyButton.interactable = true;

        }
        if (PlayerPrefs.GetInt("isUnionComplete") == 0)
        {
            unionButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isUnionComplete") == 1)
        {
            unionButton.interactable = true;

        }
        if (PlayerPrefs.GetInt("isMassesComplete") == 0)
        {
            massesButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isMassesComplete") == 1)
        {
            massesButton.interactable = true;

        }
        if (PlayerPrefs.GetInt("isMafiaComplete") == 0)
        {
            mafiaButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isMafiaComplete") == 1)
        {
            mafiaButton.interactable = true;

        }
        if (PlayerPrefs.GetInt("isCIAComplete") == 0)
        {
            ciaButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isCIAComplete") == 1)
        {
            ciaButton.interactable = true;

        }
        if (PlayerPrefs.GetInt("isFinaleComplete") == 0)
        {
            finaleButton.interactable = false;
        }
        else if (PlayerPrefs.GetInt("isFinaleComplete") == 1)
        {
            finaleButton.interactable = true;

        }

    }//END Init

    //-----------------------//
    public void OpenHeistScreen(int screenValue)
    //-----------------------//
    {
        if (screenValue == 0)
        {
            mainScreen.SetActive(true);
            ladyScreen.SetActive(true);
        }
        if (screenValue == 1)
        {
            mainScreen.SetActive(true);
            unionScreen.SetActive(true);
        }
        if (screenValue == 2)
        {
            mainScreen.SetActive(true);
            massesScreen.SetActive(true);
        }
        if (screenValue == 3)
        {
            mainScreen.SetActive(true);
            mafiaScreen.SetActive(true);
        }
        if (screenValue == 4)
        {
            mainScreen.SetActive(true);
            ciaScreen.SetActive(true);
        }
        if (screenValue == 5)
        {
            mainScreen.SetActive(true);
            finaleScreen.SetActive(true);
        }

    }//END OpenHeistScreen

    //-----------------------//
    public void MainMenu()
    //-----------------------//
    {
        SceneManager.LoadScene(0);

    }//END MainMenu

}//END CLASS CollectibleMenuManager
