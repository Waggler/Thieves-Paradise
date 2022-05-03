using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class NotesMenuManager : MonoBehaviour
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
        if (PlayerPrefs.GetInt("isNote1PickedUp") == 1)
        {
            note1Button.interactable = true;
        }
        else
        {
            note1Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("isNote2PickedUp") == 1)
        {
            note2Button.interactable = true;
        }
        else
        {
            note2Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("isNote3PickedUp") == 1)
        {
            note3Button.interactable = true;
        }
        else
        {
            note3Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("isNote4PickedUp") == 1)
        {
            note4Button.interactable = true;
        }
        else
        {
            note4Button.interactable = false;
        }
        if (PlayerPrefs.GetInt("isNote5PickedUp") == 1)
        {
            note5Button.interactable = true;
        }
        else
        {
            note5Button.interactable = false;
        }

    }//END Init

    //-----------------------//
    public void InitNote(NoteCollectibleData data)
    //-----------------------//
    {
        descriptionText.text = data.description.ToString();
        fadePanel.SetActive(true);
        descriptionPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(closeButton.gameObject);

    }//END InitNote


    #endregion Methods
}
