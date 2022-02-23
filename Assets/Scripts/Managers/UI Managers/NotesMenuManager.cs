using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotesMenuManager : MonoBehaviour
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


    #endregion Startups


    #endregion Methods
}
