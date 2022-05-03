using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemsMenuManager : MonoBehaviour
{
    #region Components

    [Header("Components")]
    [Header("Panels")]
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Button closeButton;

    [Space(5)]

    [Header("Buttons")]
    [SerializeField] private Button smokeButton;
    [SerializeField] private Button bottleButton;
    [SerializeField] private Button cardButton;
    [SerializeField] private Button balloonButton;
    [SerializeField] private Button laserPointerButton;
    [SerializeField] private Button cupButton;

    [Space(5)]

    [Header("Visuals")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text useDescriptionText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private RawImage itemImage;



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
        if(PlayerPrefs.GetInt("isSmokePickedUp") == 1)
        {
            smokeButton.interactable = true;
        }
        else
        {
            smokeButton.interactable = false;

        }
        if (PlayerPrefs.GetInt("isBottlePickedUp") == 1)
        {
            bottleButton.interactable = true;

        }
        else
        {
            bottleButton.interactable = false;

        }
        if (PlayerPrefs.GetInt("isCardPickedUp") == 1)
        {
            cardButton.interactable = true;

        }
        else
        {
            cardButton.interactable = false;

        }
        if (PlayerPrefs.GetInt("isBalloonPickedUp") == 1)
        {
            balloonButton.interactable = true;

        }
        else
        {
            balloonButton.interactable = false;

        }
        if (PlayerPrefs.GetInt("isLaserPickedUp") == 1)
        {
            laserPointerButton.interactable = true;

        }
        else
        {
            laserPointerButton.interactable = false;

        }
        if (PlayerPrefs.GetInt("isCupPickedUp") == 1)
        {
            cupButton.interactable = true;

        }
        else
        {
            cupButton.interactable = false;

        }

    }//END Init

    //-----------------------//
    public void InitItem(ItemCollectibleData data)
    //-----------------------//
    {
        nameText.text = data.title.ToString();
        descriptionText.text = data.description;
        itemImage.texture = data.rawImage;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);
        fadePanel.SetActive(true);
        descriptionPanel.SetActive(true);

    }//END InitItem


    #endregion Methods


}//END CLASS ItemsMenuManager
