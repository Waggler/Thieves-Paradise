using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectiblesManager : MonoBehaviour
{


    #region Components

    [Header("Components")]

    [Space(5)]

    [Header("Panels")]
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject worldPanel;
    [SerializeField] private GameObject notePanel;

    [Space(5)]

    [Header("Buttons")]
    [Header("Items")]
    [SerializeField] private Button smokeButton;
    [SerializeField] private Button bottleButton;
    [SerializeField] private Button cardButton;
    [SerializeField] private Button balloonButton;

    [Space(5)]

    [Header("World")]
    [SerializeField] private Button musicButton;
    [SerializeField] private Button jewelryButton;
    [SerializeField] private Button perfumeButton;
    [SerializeField] private Button luxuryButton;
    [SerializeField] private Button partyButton;
    [SerializeField] private Button toyButton;
    [SerializeField] private Button antiqueButton;
    [SerializeField] private Button apparelButton;
    [SerializeField] private Button hardwareButton;
    [SerializeField] private Button kitchenButton;
    [SerializeField] private Button arcadeButton;
    [SerializeField] private Button borgarButton;
    [SerializeField] private Button fitnessButton;
    [SerializeField] private Button menButton;
    [SerializeField] private Button foodCourtButton;
    [SerializeField] private Button yeehawButton;
    [SerializeField] private Button tokyoButton;
    [SerializeField] private Button beachButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button clockButton;

    [Space(5)]

    [Header("Notes")]    
    [SerializeField] private Button note1Button;
    [SerializeField] private Button note2Button;
    [SerializeField] private Button note3Button;
    [SerializeField] private Button note4Button;
    [SerializeField] private Button note5Button;
    [SerializeField] private Button note6Button;
    [SerializeField] private Button note7Button;
    [SerializeField] private Button note8Button;
    [SerializeField] private Button note9Button;
    [SerializeField] private Button note10Button;

    [Space(5)]

    [Header("Text")]
    [SerializeField] private tmp
    



    #endregion Components


    #region Methods


    #region Startups


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
        

    }//END Init


    #endregion Startups


    #region Setups


    //-----------------------//
    public void InitItem()
    //-----------------------//
    {
        Init();

    }//END InitItem

    //-----------------------//
    public void InitWorld()
    //-----------------------//
    {


    }//END InitWorld

    //-----------------------//
    public void InitNote()
    //-----------------------//
    {


    }//END InitNote


    #endregion Startups


    #endregion Methods


}//END CLASS CollectiblesManager
