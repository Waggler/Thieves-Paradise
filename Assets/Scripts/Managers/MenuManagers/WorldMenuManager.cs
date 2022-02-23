using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldMenuManager : MonoBehaviour
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

    [Header("Text")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    [Space(5)]

    [Header("Images")]
    [SerializeField] private Image imageOne;
    [SerializeField] private Image imageTwo;


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


    #endregion Methods


}//END CLASS WorldMenuManager
