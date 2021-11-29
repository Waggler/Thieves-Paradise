using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{

    #region Components


    [Header("Components")]
    [Header("Choice Components")]
    [SerializeField] private DialogueManager dialogueManager;
    public Dialogue currentChoice;

    [SerializeField] private Animator choiceAnimator;

    [SerializeField] private GameObject[] choiceUiObjects;
    [SerializeField] private GameObject choice1;
    [SerializeField] private GameObject choice2;
    [SerializeField] private GameObject choice3;
    [SerializeField] private GameObject choice4;
    [SerializeField] private GameObject choice5;

    [SerializeField] private TMP_Text[] choiceTexts;
    [SerializeField] private TMP_Text choice1Text;
    [SerializeField] private TMP_Text choice2Text;
    [SerializeField] private TMP_Text choice3Text;
    [SerializeField] private TMP_Text choice4Text;
    [SerializeField] private TMP_Text choice5Text;


    #endregion Components


    #region Methods


    public void InitChoices()
    {
        for (int i = 0; i < currentChoice.choiceOptions.Length && i < 6; i++)
        {
            choiceTexts[i].text = currentChoice.choiceOptions[i];
        }

        ShowChoices(currentChoice.choiceOptions.Length);
    }


    //--------------------------//
    public void ShowChoices(int choiceAmount)
    //--------------------------//
    {
        /*
        if (choiceAmount == 1)
        {
            choice1.SetActive(true);

        }
        if (choiceAmount == 2)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);

        }
        if (choiceAmount == 3)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);

        }
        if (choiceAmount == 4)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);
            choice4.SetActive(true);

        }
        if (choiceAmount == 5)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);
            choice4.SetActive(true);
            choice5.SetActive(true);

        }*/

        switch (choiceAmount)
        {
            default:
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice4.SetActive(true);
                choice5.SetActive(true);
                break;
            case 4:
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                choice4.SetActive(true);
                break;
            case 3:
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                break;
            case 2:
                choice1.SetActive(true);
                choice2.SetActive(true);
                break;
            case 1:
                choice1.SetActive(true);
                choice2.SetActive(true);
                choice3.SetActive(true);
                break;
        }

        choiceAnimator.SetBool("isChoiceBoxOpen", true);


    }//END ShowChoices

    //--------------------------//
    public void CloseChoices()
    //--------------------------//
    {
        choiceAnimator.SetBool("isChoiceBoxOpen", false);

        choice1.SetActive(false);
        choice2.SetActive(false);
        choice3.SetActive(false);
        choice4.SetActive(false);
        choice5.SetActive(false);

    }//END CloseChoices

    public void ChooseChoiceOption(int optionId)
    {

    }


    #endregion Methods


}//END ChoiceManager
