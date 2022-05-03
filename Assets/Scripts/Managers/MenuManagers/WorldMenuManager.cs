using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class WorldMenuManager : MonoBehaviour
{
    #region Components

    [Header("Components")]

    [Space(5)]

    [Header("Panels")]
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private Button closeButton;

    [Space(5)]

    [Header("Buttons")]
    [SerializeField] private Button musicButton;
    [SerializeField] private Button jewelryButton;
    [SerializeField] private Button perfumeButton;
    [SerializeField] private Button luxuryButton;
    [SerializeField] private Button partyButton;
    [SerializeField] private Button toyButton;
    [SerializeField] private Button apparelButton;
    [SerializeField] private Button hardwareButton;
    [SerializeField] private Button kitchenButton;
    [SerializeField] private Button arcadeButton;
    [SerializeField] private Button borgarButton;
    [SerializeField] private Button fitnessButton;
    [SerializeField] private Button menButton;
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
    [SerializeField] private Sprite musicSilhouette;
    [SerializeField] private Sprite musicCollected;
    [SerializeField] private Sprite jewelrySilhouette;
    [SerializeField] private Sprite jewelryCollected;
    [SerializeField] private Sprite perfumeSilhouette;
    [SerializeField] private Sprite perfumeCollected;
    [SerializeField] private Sprite luxurySilhouette;
    [SerializeField] private Sprite luxuryCollected;
    [SerializeField] private Sprite partySilhouette;
    [SerializeField] private Sprite partyCollected;
    [SerializeField] private Sprite toySilhouette;
    [SerializeField] private Sprite toyCollected;
    [SerializeField] private Sprite apparelSilhouette;
    [SerializeField] private Sprite apparelCollected;
    [SerializeField] private Sprite hardwareSilhouette;
    [SerializeField] private Sprite hardwareCollected;
    [SerializeField] private Sprite kitchenSilhouette;
    [SerializeField] private Sprite kitchenCollected;
    [SerializeField] private Sprite arcadeSilhouette;
    [SerializeField] private Sprite arcadeCollected;
    [SerializeField] private Sprite borgarSilhouette;
    [SerializeField] private Sprite borgarCollected;
    [SerializeField] private Sprite fitnessSilhouette;
    [SerializeField] private Sprite fitnessCollected;
    [SerializeField] private Sprite menSilhouette;
    [SerializeField] private Sprite menCollected;
    [SerializeField] private Sprite yeehawSilhouette;
    [SerializeField] private Sprite yeehawCollected;
    [SerializeField] private Sprite tokyoSilhouette;
    [SerializeField] private Sprite tokyoCollected;
    [SerializeField] private Sprite beachSilhouette;
    [SerializeField] private Sprite beachCollected;
    [SerializeField] private Sprite mainSilhouette;
    [SerializeField] private Sprite mainCollected;
    [SerializeField] private Sprite clockSilhouette;
    [SerializeField] private Sprite clockCollected;




    #endregion Components


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        PlayerPrefs.SetInt("isJewelryUnlocked", 1);

        Init();

    }//END Start

    //-----------------------//
    private void Init()
    //-----------------------//
    {
        if (PlayerPrefs.GetInt("isMusicUnlocked") == 1)
        {
            musicButton.interactable = true;
            musicButton.image.sprite = musicCollected;
        }
        else
        {
            musicButton.interactable = false;
            musicButton.image.sprite = musicSilhouette;

        }

        if (PlayerPrefs.GetInt("isJewelryUnlocked") == 1)
        {
            jewelryButton.interactable = true;
            jewelryButton.image.sprite = jewelryCollected;
        }
        else
        {
            jewelryButton.interactable = false;
            jewelryButton.image.sprite = jewelrySilhouette;

        }

        if (PlayerPrefs.GetInt("isPerfumeUnlocked") == 1)
        {
            perfumeButton.interactable = true;
            perfumeButton.image.sprite = perfumeCollected;
        }
        else
        {
            perfumeButton.interactable = false;
            perfumeButton.image.sprite = perfumeSilhouette;
        }

        if (PlayerPrefs.GetInt("isLuxuryUnlocked") == 1)
        {
            luxuryButton.interactable = true;
            luxuryButton.image.sprite = luxuryCollected;
        }
        else
        {
            luxuryButton.interactable = false;
            luxuryButton.image.sprite = luxurySilhouette;

        }

        if (PlayerPrefs.GetInt("isPartyUnlocked") == 1)
        {
            partyButton.interactable = true;
            partyButton.image.sprite = partyCollected;
        }
        else
        {
            partyButton.interactable = false;
            partyButton.image.sprite = partySilhouette;

        }

        if (PlayerPrefs.GetInt("isToyUnlocked") == 1)
        {
            toyButton.interactable = true;
            toyButton.image.sprite = toyCollected;
        }
        else
        {
            toyButton.interactable = false;
            toyButton.image.sprite = toySilhouette;

        }

        if (PlayerPrefs.GetInt("isApparelUnlocked") == 1)
        {
            apparelButton.interactable = true;
            apparelButton.image.sprite = apparelCollected;
        }
        else
        {
            apparelButton.interactable = false;
            apparelButton.image.sprite = apparelSilhouette;

        }

        if (PlayerPrefs.GetInt("isHardwareUnlocked") == 1)
        {
            hardwareButton.interactable = true;
            hardwareButton.image.sprite = hardwareCollected;
        }
        else
        {
            hardwareButton.interactable = false;
            hardwareButton.image.sprite = hardwareSilhouette;

        }

        if (PlayerPrefs.GetInt("isKitchenUnlocked") == 1)
        {
            kitchenButton.interactable = true;
            kitchenButton.image.sprite = kitchenCollected;
        }
        else
        {
            kitchenButton.interactable = false;
            kitchenButton.image.sprite = kitchenSilhouette;

        }

        if (PlayerPrefs.GetInt("isArcadeUnlocked") == 1)
        {
            arcadeButton.interactable = true;
            arcadeButton.image.sprite = arcadeCollected;
        }
        else
        {
            arcadeButton.interactable = false;
            arcadeButton.image.sprite = arcadeSilhouette;

        }

        if (PlayerPrefs.GetInt("isBorgarUnlocked") == 1)
        {
            borgarButton.interactable = true;
            borgarButton.image.sprite = borgarCollected;
        }
        else
        {
            borgarButton.interactable = false;
            borgarButton.image.sprite = borgarSilhouette;

        }

        if (PlayerPrefs.GetInt("isFitnessUnlocked") == 1)
        {
            fitnessButton.interactable = true;
            fitnessButton.image.sprite = fitnessCollected;
        }
        else
        {
            fitnessButton.interactable = false;
            fitnessButton.image.sprite = fitnessSilhouette;

        }

        if (PlayerPrefs.GetInt("isMenUnlocked") == 1)
        {
            menButton.interactable = true;
            menButton.image.sprite = menCollected;
        }
        else
        {
            menButton.interactable = false;
            menButton.image.sprite = menSilhouette;

        }

        if (PlayerPrefs.GetInt("isYeehawUnlocked") == 1)
        {
            yeehawButton.interactable = true;
            yeehawButton.image.sprite = yeehawCollected;
        }
        else
        {
            yeehawButton.interactable = false;
            yeehawButton.image.sprite = yeehawSilhouette;

        }

        if (PlayerPrefs.GetInt("isTokyoUnlocked") == 1)
        {
            tokyoButton.interactable = true;
            tokyoButton.image.sprite = tokyoCollected;
        }
        else
        {
            tokyoButton.interactable = false;
            tokyoButton.image.sprite = tokyoSilhouette;

        }

        if (PlayerPrefs.GetInt("isBeachUnlocked") == 1)
        {
            beachButton.interactable = true;
            beachButton.image.sprite = beachCollected;
        }
        else
        {
            beachButton.interactable = false;
            beachButton.image.sprite = beachSilhouette;

        }

        if (PlayerPrefs.GetInt("isLobbyUnlocked") == 1)
        {
            mainButton.interactable = true;
            mainButton.image.sprite = mainCollected;
        }
        else
        {
            mainButton.interactable = false;
            mainButton.image.sprite = mainSilhouette;

        }

        if (PlayerPrefs.GetInt("isClockUnlocked") == 1)
        {
            clockButton.interactable = true;
            clockButton.image.sprite = clockCollected;
        }
        else
        {
            clockButton.interactable = false;
            clockButton.image.sprite = clockSilhouette;

        }


    }//END Init



    //-----------------------//
    public void InitLocation(LocationCollectibleData data)
    //-----------------------//
    {
        nameText.text = data.title.ToString();
        descriptionText.text = data.description.ToString();
        imageOne.sprite = data.locationSprite1;
        imageTwo.sprite = data.locationSprite2;
        fadePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);
        descriptionPanel.SetActive(true);

    }//END InitLocation

}//END CLASS WorldMenuManager
