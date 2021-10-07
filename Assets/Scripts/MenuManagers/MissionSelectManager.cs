using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionSelectManager : MonoBehaviour
{
    #region Components


    [Header("Components")]
    [Header("Buttons")]
    [SerializeField] private Button ladyButton;
    [SerializeField] private Button unionButton;
    [SerializeField] private Button massesButton;
    [SerializeField] private Button mafiaButton;
    [SerializeField] private Button voicesButton;
    [SerializeField] private Button ciaButton;

    [Header("Bools")]
    public bool isTierOneComplete;
    public bool isTierTwoComplete;
    public bool isTierThreeComplete;

    public bool isLadyComplete;
    public bool isUnionComplete;
    public bool isMassesComplete;
    public bool isMafiaComplete;
    public bool isVoicesComplete;
    public bool isCIAComplete;


    #endregion Components


    #region Methods


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        if (isTierOneComplete == false)
        {
            unionButton.interactable = false;
            massesButton.interactable = false;
        }
        else if (isTierOneComplete == true)
        {
            ladyButton.interactable = false;
        }

        if (isTierTwoComplete == false)
        {
            mafiaButton.interactable = false;
            ciaButton.interactable = false;
        }
        else if (isTierTwoComplete == true)
        {

            mafiaButton.interactable = true;
            ciaButton.interactable = true;
        }

        if (isTierThreeComplete == false)
        {
            voicesButton.interactable = false;
        }
        else if (isTierThreeComplete == true)
        {
            voicesButton.interactable = true;

        }

        CloseMission();

    }//END Start

    //-----------------------//
    private void CloseMission()
    //-----------------------//
    {
        if (isLadyComplete == true)
        {
            ladyButton.interactable = false;
        }
        if (isMassesComplete == true)
        {
            massesButton.interactable = false;
        }
        if (isUnionComplete == true)
        {
            unionButton.interactable = false;
        }
        if (isCIAComplete == true)
        {
            ciaButton.interactable = false;
        }
        if (isMafiaComplete == true)
        {
            mafiaButton.interactable = false;
        }
        if (isVoicesComplete == true)
        {
            Debug.Log("Finale Complete.");
        }

    }//END CloseMission

    //-----------------------//
    public void MainMenu()
    //-----------------------//
    {
        Debug.Log("Back to Main Menu");

    }//END MainMenu

    //-----------------------//
    public void LadyMission()
    //-----------------------//
    {
        Debug.Log("Selected the Lady Mission");

    }//END LadyMission

    //-----------------------//
    public void MassesMission()
    //-----------------------//
    {
        Debug.Log("Selected the Masses Mission");

    }//END MassesMission

    //-----------------------//
    public void UnionMission()
    //-----------------------//
    {
        Debug.Log("Selected the Union Mission");

    }//END UnionMission

    //-----------------------//
    public void MafiaMission()
    //-----------------------//
    {
        Debug.Log("Selected the Mafia Mission");

    }//END MafiaMission

    //-----------------------//
    public void CIAMission()
    //-----------------------//
    {
        Debug.Log("Selected the CIA Mission");

    }//END CIAMission

    //-----------------------//
    public void VoicesMission()
    //-----------------------//
    {
        Debug.Log("Selected the Voices Mission");

    }//END VoicesMission


    #endregion Methods


}//END MissionSelectManager
