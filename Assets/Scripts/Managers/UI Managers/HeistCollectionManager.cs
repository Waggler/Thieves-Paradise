using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeistCollectionManager : MonoBehaviour
{


    #region Components


    [SerializeField] ItemCollectibleData[] heistCollectibleData;

    public enum CurrentHeist
    {
        LADY,
        MASSES,
        MAFIA
    }

    public CurrentHeist currentHeist;

    [Header("Components")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject collectionMenu;

    [SerializeField] TMP_Text titleText;
    [Header("Heist 1")]
    [SerializeField] Button collectible1Button;
    [SerializeField] Button collectible2Button;
    [SerializeField] Button collectible3Button;
    [Header("Heist 2")]                 
    [SerializeField] Button collectible4Button;
    [SerializeField] Button collectible5Button;
    [SerializeField] Button collectible6Button;
    [Header("Heist 3")]
    [SerializeField] Button collectible7Button;
    [SerializeField] Button collectible8Button;
    [SerializeField] Button collectible9Button;


    #endregion Components


    #region Methods

    //-----------------------//
    public void Init()//TODO Add more buttons & data referencing?
    //-----------------------//
    {
        if (currentHeist == CurrentHeist.LADY)
        {
            titleText.text = "The Lady";

            if (PlayerPrefs.GetInt("isLadyCollectible1Taken") == 1)
            {
                collectible1Button.interactable = true;
            }
            else
            {
                collectible1Button.interactable = false;
            }
            if (PlayerPrefs.GetInt("isLadyCollectible2Taken") == 1)
            {
                collectible2Button.interactable = true;
            }
            else
            {
                collectible2Button.interactable = false;
            }
            if (PlayerPrefs.GetInt("isLadyCollectible3Taken") == 1)
            {
                collectible3Button.interactable = true;
            }
            else
            {
                collectible3Button.interactable = false;
            }
        }

        if (currentHeist == CurrentHeist.MASSES)
        {
            titleText.text = "The Masses";

            if (PlayerPrefs.GetInt("isMassesCollectible1Taken") == 1)
            {
                collectible1Button.interactable = true;
            }
            else
            {
                collectible1Button.interactable = false;
            }
            if (PlayerPrefs.GetInt("isMassesCollectible2Taken") == 1)
            {
                collectible2Button.interactable = true;
            }
            else
            {
                collectible2Button.interactable = false;
            }
            if (PlayerPrefs.GetInt("isMassesCollectible3Taken") == 1)
            {
                collectible3Button.interactable = true;
            }
            else
            {
                collectible3Button.interactable = false;
            }
        }

        if (currentHeist == CurrentHeist.MAFIA)
        {
            titleText.text = "The Mafia";

            if (PlayerPrefs.GetInt("isMafiaCollectible1Taken") == 1)
            {
                collectible1Button.interactable = true;
            }
            else
            {
                collectible1Button.interactable = false;
            }
            if (PlayerPrefs.GetInt("isMafiaCollectible2Taken") == 1)
            {
                collectible2Button.interactable = true;
            }
            else
            {
                collectible2Button.interactable = false;
            }
            if (PlayerPrefs.GetInt("isMafiaCollectible3Taken") == 1)
            {
                collectible3Button.interactable = true;
            }
            else
            {
                collectible3Button.interactable = false;
            }
        }

    }//END Init

    //-----------------------//
    public void Back()
    //-----------------------//
    {
        mainMenu.SetActive(true);
        collectionMenu.SetActive(false);

    }//END Back


    #endregion Methods


}//END CLASS HeistCollectionManager
