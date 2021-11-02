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
    [SerializeField] public Dialogue choiceOptions;

    [SerializeField] private Animator choiceAnimator;

    [Header("Making Choices")]
    [Tooltip("The Dialogue Index where the player may make a narrative choice, set outside of number of dialogue indexes if not making a choice")]
    public int choiceIndex1;
    [Tooltip("The Dialogue Index where the player may make a narrative choice, set outside of number of dialogue indexes if not making a choice")]
    public int choiceIndex2;
    [Tooltip("The Dialogue Index where the player may make a narrative choice, set outside of number of dialogue indexes if not making a choice")]
    public int choiceIndex3;
    [Tooltip("The Dialogue Index where the player may make a narrative choice, set outside of number of dialogue indexes if not making a choice")]
    public int choiceIndex4;
    [Tooltip("The Dialogue Index where the player may make a narrative choice, set outside of number of dialogue indexes if not making a choice")]
    public int choiceIndex5;

    [Tooltip("How Many choices for index 1?")]
    public int choiceAmount1;
    [Tooltip("How Many choices for index 2?")]
    public int choiceAmount2;
    [Tooltip("How Many choices for index 3?")]
    public int choiceAmount3;
    [Tooltip("How Many choices for index 4?")]
    public int choiceAmount4;
    [Tooltip("How Many choices for index 5?")]
    public int choiceAmount5;


    [SerializeField] private GameObject choice1;
    [SerializeField] private GameObject choice2;
    [SerializeField] private GameObject choice3;
    [SerializeField] private GameObject choice4;
    [SerializeField] private GameObject choice5;

    [SerializeField] private TMP_Text choice1Text;
    [SerializeField] private TMP_Text choice2Text;
    [SerializeField] private TMP_Text choice3Text;
    [SerializeField] private TMP_Text choice4Text;
    [SerializeField] private TMP_Text choice5Text;

    [Header("Choice Text")]
    public string Index1Choice1String;
    public string Index1Choice2String;
    public string Index1Choice3String;
    public string Index1Choice4String;
    public string Index1Choice5String;
                  
    public string Index2Choice1String;
    public string Index2Choice2String;
    public string Index2Choice3String;
    public string Index2Choice4String;
    public string Index2Choice5String;

    public string Index3Choice1String;
    public string Index3Choice2String;
    public string Index3Choice3String;
    public string Index3Choice4String;
    public string Index3Choice5String;

    public string Index4Choice1String;
    public string Index4Choice2String;
    public string Index4Choice3String;
    public string Index4Choice4String;
    public string Index4Choice5String;

    public string Index5Choice1String;
    public string Index5Choice2String;
    public string Index5Choice3String;
    public string Index5Choice4String;
    public string Index5Choice5String;


    #endregion Components


    #region Methods


    //--------------------------//
    public void ShowChoices(int choiceAmount, int index)
    //--------------------------//
    {

        if (choiceAmount == 1)
        {
            choice1.SetActive(true);

            if (index == 1)
            {
                choice1Text.SetText(Index1Choice1String);
            }
            if (index == 2)
            {
                choice1Text.SetText(Index2Choice1String);
            }
            if (index == 3)
            {
                choice1Text.SetText(Index3Choice1String);
            }
            if (index == 4)
            {
                choice1Text.SetText(Index4Choice1String);
            }
            if (index == 5)
            {
                choice1Text.SetText(Index5Choice1String);
            }
        }
        if (choiceAmount == 2)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);

            if (index == 1)
            {
                choice1Text.SetText(Index1Choice1String);
                choice2Text.SetText(Index1Choice2String);

            }
            if (index == 2)
            {
                choice1Text.SetText(Index2Choice1String);
                choice2Text.SetText(Index2Choice2String);

            }
            if (index == 3)
            {
                choice1Text.SetText(Index3Choice1String);
                choice2Text.SetText(Index3Choice2String);

            }
            if (index == 4)
            {
                choice1Text.SetText(Index4Choice1String);
                choice2Text.SetText(Index4Choice2String);

            }
            if (index == 5)
            {
                choice1Text.SetText(Index5Choice1String);
                choice2Text.SetText(Index5Choice2String);
            }

        }
        if (choiceAmount == 3)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);

            if (index == 1)
            {
                choice1Text.SetText(Index1Choice1String);
                choice2Text.SetText(Index1Choice2String);
                choice3Text.SetText(Index1Choice3String);

            }
            if (index == 2)
            {
                choice1Text.SetText(Index2Choice1String);
                choice2Text.SetText(Index2Choice2String);
                choice3Text.SetText(Index2Choice3String);

            }
            if (index == 3)
            {
                choice1Text.SetText(Index3Choice1String);
                choice2Text.SetText(Index3Choice2String);
                choice3Text.SetText(Index3Choice3String);

            }
            if (index == 4)
            {
                choice1Text.SetText(Index4Choice1String);
                choice2Text.SetText(Index4Choice2String);
                choice3Text.SetText(Index4Choice3String);

            }
            if (index == 5)
            {
                choice1Text.SetText(Index5Choice1String);
                choice2Text.SetText(Index5Choice2String);
                choice3Text.SetText(Index5Choice3String);

            }

        }
        if (choiceAmount == 4)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);
            choice4.SetActive(true);

            if (index == 1)
            {
                choice1Text.SetText(Index1Choice1String);
                choice2Text.SetText(Index1Choice2String);
                choice3Text.SetText(Index1Choice3String);
                choice4Text.SetText(Index1Choice4String);


            }
            if (index == 2)
            {
                choice1Text.SetText(Index2Choice1String);
                choice2Text.SetText(Index2Choice2String);
                choice3Text.SetText(Index2Choice3String);
                choice4Text.SetText(Index2Choice4String);

            }
            if (index == 3)
            {
                choice1Text.SetText(Index3Choice1String);
                choice2Text.SetText(Index3Choice2String);
                choice3Text.SetText(Index3Choice3String);
                choice4Text.SetText(Index3Choice4String);

            }
            if (index == 4)
            {
                choice1Text.SetText(Index4Choice1String);
                choice2Text.SetText(Index4Choice2String);
                choice3Text.SetText(Index4Choice3String);
                choice4Text.SetText(Index4Choice4String);


            }
            if (index == 5)
            {
                choice1Text.SetText(Index5Choice1String);
                choice2Text.SetText(Index5Choice2String);
                choice3Text.SetText(Index5Choice3String);
                choice4Text.SetText(Index5Choice4String);

            }

        }
        if (choiceAmount == 5)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);
            choice4.SetActive(true);
            choice5.SetActive(true);

            if (index == 1)
            {
                choice1Text.SetText(Index1Choice1String);
                choice2Text.SetText(Index1Choice2String);
                choice3Text.SetText(Index1Choice3String);
                choice4Text.SetText(Index1Choice4String);
                choice5Text.SetText(Index1Choice5String);


            }
            if (index == 2)
            {
                choice1Text.SetText(Index2Choice1String);
                choice2Text.SetText(Index2Choice2String);
                choice3Text.SetText(Index2Choice3String);
                choice4Text.SetText(Index2Choice4String);
                choice5Text.SetText(Index2Choice5String);

            }
            if (index == 3)
            {
                choice1Text.SetText(Index3Choice1String);
                choice2Text.SetText(Index3Choice2String);
                choice3Text.SetText(Index3Choice3String);
                choice4Text.SetText(Index3Choice4String);
                choice5Text.SetText(Index3Choice5String);

            }
            if (index == 4)
            {
                choice1Text.SetText(Index4Choice1String);
                choice2Text.SetText(Index4Choice2String);
                choice3Text.SetText(Index4Choice3String);
                choice4Text.SetText(Index4Choice4String);
                choice5Text.SetText(Index4Choice5String);


            }
            if (index == 5)
            {
                choice1Text.SetText(Index5Choice1String);
                choice2Text.SetText(Index5Choice2String);
                choice3Text.SetText(Index5Choice3String);
                choice4Text.SetText(Index5Choice4String);
                choice5Text.SetText(Index5Choice5String);

            }

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


    #endregion Methods


}//END ChoiceManager
