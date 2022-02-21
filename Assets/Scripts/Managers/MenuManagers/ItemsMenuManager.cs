using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsMenuManager : MonoBehaviour
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

    [Header("Text")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;




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

    //-----------------------//
    public void InitItem()
    //-----------------------//
    {
        Init();

    }//END InitItem


    #endregion Startups


    #endregion Methods


}//END CLASS ItemsMenuManager
