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


    #endregion Components


    #region Methods


    //--------------------------//
    public void ShowChoices(int choiceAmount, int index)
    //--------------------------//
    {

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
