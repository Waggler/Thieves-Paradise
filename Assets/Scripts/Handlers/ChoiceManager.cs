using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceManager : MonoBehaviour
{
    [Header("Components")]
    [Header("Choice Components")]
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] public Dialogue choiceOptions;

    [SerializeField] private Animator choiceAnimator;

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
    public string choice1String;
    public string choice2String;
    public string choice3String;
    public string choice4String;
    public string choice5String;

    private void Start()
    {
        ShowChoices(5);
    }

    //--------------------------//
    public void ShowChoices(int choiceAmount)
    //--------------------------//
    {
        choiceAnimator.SetBool("isChoiceBoxOpen", true);

        if (choiceAmount == 1)
        {
            choice1.SetActive(true);
            choice1Text.SetText(choice1String);

        }
        if (choiceAmount == 2)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);

            choice1Text.SetText(choice1String);
            choice2Text.SetText(choice2String);

        }
        if (choiceAmount == 3)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);

            choice1Text.SetText(choice1String);
            choice2Text.SetText(choice2String);
            choice3Text.SetText(choice3String);

        }
        if (choiceAmount == 4)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);
            choice4.SetActive(true);

            choice1Text.SetText(choice1String);
            choice2Text.SetText(choice2String);
            choice3Text.SetText(choice3String);
            choice4Text.SetText(choice4String);

        }
        if (choiceAmount == 5)
        {
            choice1.SetActive(true);
            choice2.SetActive(true);
            choice3.SetActive(true);
            choice4.SetActive(true);
            choice5.SetActive(true);

            choice1Text.SetText(choice1String);
            choice2Text.SetText(choice2String);
            choice3Text.SetText(choice3String);
            choice4Text.SetText(choice4String);
            choice5Text.SetText(choice5String);

        }

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


}//END ChoiceManager
