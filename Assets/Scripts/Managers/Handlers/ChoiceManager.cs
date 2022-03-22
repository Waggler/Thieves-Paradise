using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ChoiceManager : MonoBehaviour
{

    #region Components


    [Header("Components")]
    [Header("Choice Components")]
    [SerializeField] private DialogueManager dialogueManager;
    [HideInInspector]public Dialogue currentChoice;

    [SerializeField] private Animator choiceAnimator;

    [SerializeField] private GameObject[] choiceUiObjects;

    [SerializeField] private TMP_Text[] choiceTexts;

    public Button continueButton;


    #endregion Components


    #region Methods


    public void InitChoices()
    {
        for (int i = 0; i < currentChoice.choices.Length && i < 6; i++)
        {
            choiceTexts[i].text = currentChoice.choices[i].choiceOption;
        }

        ShowChoices(currentChoice.choices.Length);
    }


    //--------------------------//
    public void ShowChoices(int choiceAmount)
    //--------------------------//
    {
        switch (choiceAmount)
        {
            default:
                choiceUiObjects[4].SetActive(true);
                choiceUiObjects[3].SetActive(true);
                choiceUiObjects[2].SetActive(true);
                choiceUiObjects[1].SetActive(true);
                choiceUiObjects[0].SetActive(true);
                break;
            case 4:
                choiceUiObjects[3].SetActive(true);
                choiceUiObjects[2].SetActive(true);
                choiceUiObjects[1].SetActive(true);
                choiceUiObjects[0].SetActive(true);
                break;
            case 3:
                choiceUiObjects[2].SetActive(true);
                choiceUiObjects[1].SetActive(true);
                choiceUiObjects[0].SetActive(true);
                break;
            case 2:
                choiceUiObjects[1].SetActive(true);
                choiceUiObjects[0].SetActive(true);
                break;
            case 1:
                choiceUiObjects[0].SetActive(true);
                break;
        }

        choiceAnimator.SetBool("isChoiceBoxOpen", true);

    }//END ShowChoices

    //--------------------------//
    public void CloseChoices()
    //--------------------------//
    {
        choiceAnimator.SetBool("isChoiceBoxOpen", false);

        foreach (GameObject choiceUiObject in choiceUiObjects)
            choiceUiObject.SetActive(false);


    }//END CloseChoices

    public void ShowResponse(int optionId)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);
        currentChoice.isChoice = false;
        dialogueManager.TriggerResponse(currentChoice.responses[optionId]);
        CloseChoices();
    }


    #endregion Methods


}//END ChoiceManager
