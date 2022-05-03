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
    [SerializeField] private Sprite smokeSilhouette;
    [SerializeField] private Sprite smokeCollected;
    [SerializeField] private Sprite bottleSilhouette;
    [SerializeField] private Sprite bottleCollected;
    [SerializeField] private Sprite balloonSilhouette;
    [SerializeField] private Sprite balloonCollected;
    [SerializeField] private Sprite laserSilhouette;
    [SerializeField] private Sprite laserCollected;
    [SerializeField] private Sprite cardSilhouette;
    [SerializeField] private Sprite cardCollected;
    [SerializeField] private Sprite cupSilhouette;
    [SerializeField] private Sprite cupCollected;



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
            smokeButton.image.sprite = smokeCollected;
        }
        else
        {
            smokeButton.interactable = false;
            smokeButton.image.sprite = smokeSilhouette;

        }
        if (PlayerPrefs.GetInt("isBottlePickedUp") == 1)
        {
            bottleButton.interactable = true;
            bottleButton.image.sprite = bottleCollected;

        }
        else
        {
            bottleButton.interactable = false;
            bottleButton.image.sprite = bottleSilhouette;

        }
        if (PlayerPrefs.GetInt("isCardPickedUp") == 1)
        {
            cardButton.interactable = true;
            cardButton.image.sprite = cardCollected;

        }
        else
        {
            cardButton.interactable = false;
            cardButton.image.sprite = cardSilhouette;

        }
        if (PlayerPrefs.GetInt("isBalloonPickedUp") == 1)
        {
            balloonButton.interactable = true;
            balloonButton.image.sprite = balloonCollected;

        }
        else
        {
            balloonButton.interactable = false;
            balloonButton.image.sprite = balloonSilhouette;

        }
        if (PlayerPrefs.GetInt("isLaserPickedUp") == 1)
        {
            laserPointerButton.interactable = true;
            laserPointerButton.image.sprite = laserCollected;

        }
        else
        {
            laserPointerButton.interactable = false;
            laserPointerButton.image.sprite = laserSilhouette;

        }
        if (PlayerPrefs.GetInt("isCupPickedUp") == 1)
        {
            cupButton.interactable = true;
            cupButton.image.sprite = cupCollected;

        }
        else
        {
            cupButton.interactable = false;
            cupButton.image.sprite = cupSilhouette;

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
