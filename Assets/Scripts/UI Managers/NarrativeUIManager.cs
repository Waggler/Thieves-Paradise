using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NarrativeUIManager : MonoBehaviour
{


    #region Components

    [Header("Components")]
    [SerializeField] private int currentDialogueValue;
    [SerializeField] private float typingTime;
    [SerializeField] private Animator dialogueAnimator;

    [Header("Dialogue Components")]
    [SerializeField] private GameObject speakerBox;
    [SerializeField] private GameObject textBox;
    
    [Header("Choice Components")]
    [SerializeField] private GameObject choicePanel;

    [SerializeField] private GameObject choice1;
    [SerializeField] private GameObject choice2;
    [SerializeField] private GameObject choice3;
    [SerializeField] private GameObject choice4;
    [SerializeField] private GameObject choice5;

    private Queue<string> sentences;

    public enum CurrentMission
    {
        LADY,
        MASSES,
        UNION,
        CIA,
        MAFIA,
        VOICES
    }

    public CurrentMission currentMission;

    [Header("Text Elements")]
    [SerializeField] private TMP_Text speakerText;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private TMP_Text choice1Text;
    [SerializeField] private TMP_Text choice2Text;
    [SerializeField] private TMP_Text choice3Text;
    [SerializeField] private TMP_Text choice4Text;
    [SerializeField] private TMP_Text choice5Text;

    [SerializeField] private TMP_Text lastDialogueText;


    [Header("Images")]
    [SerializeField] private Image portrait1Image;
    [SerializeField] private Image[] portrait1ImageList;

    [SerializeField] private Image portrait2Image;
    [SerializeField] private Image[] portrait2ImageList;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image[] backgroundImageList;

    [Header("Bools")]
    [SerializeField] private bool isLadyNightOne;
    [SerializeField] private bool isLadyNightTwo;
    [SerializeField] private bool isLadyNightThree;
    [SerializeField] private bool isLadyBonusComplete;

    [SerializeField] private bool isMassesNightOne;
    [SerializeField] private bool isMassesNightTwo;
    [SerializeField] private bool isMassesNightThree;
    [SerializeField] private bool isMassesBonusComplete;

    [SerializeField] private bool isUnionNightOne;
    [SerializeField] private bool isUnionNightTwo;
    [SerializeField] private bool isUnionNightThree;
    [SerializeField] private bool isUnionBonusComplete;

    [SerializeField] private bool isCIANightOne;
    [SerializeField] private bool isCIANightTwo;
    [SerializeField] private bool isCIANightThree;
    [SerializeField] private bool isCIABonusComplete;

    [SerializeField] private bool isMafiaNightOne;
    [SerializeField] private bool isMafiaNightTwo;
    [SerializeField] private bool isMafiaNightThree;
    [SerializeField] private bool isMafiaBonusComplete;

    [SerializeField] private bool isVoicesNightOne;
    [SerializeField] private bool isVoicesNightTwo;
    [SerializeField] private bool isVoicesNightThree;
    [SerializeField] private bool isVoicesBonusComplete;


    #endregion Components


    #region Monobehaviors & Startup


    //--------------------------//
    void Start()
    //--------------------------//
    {
        Init();

    }//END Start

    //--------------------------//
    void Init()
    //--------------------------//
    {
        CurrentNight();

        sentences = new Queue<string>();

    }//END Init

    //--------------------------//
    void CurrentNight()
    //--------------------------//
    {
        switch (currentMission)
        {
            case (CurrentMission.LADY):
                if(isLadyNightOne == true)
                {

                }
                else if(isLadyNightTwo == true)
                {

                }
                else if (isLadyNightThree == true)
                {

                }
                if(isLadyBonusComplete == true)
                {

                }

                break;

            case (CurrentMission.MASSES):
                if (isMassesNightOne == true)
                {

                }
                else if (isMassesNightTwo == true)
                {

                }
                else if (isMassesNightThree == true)
                {

                }
                if (isMassesBonusComplete == true)
                {

                }

                break;

            case (CurrentMission.UNION):
                if (isUnionNightOne == true)
                {

                }
                else if (isUnionNightTwo == true)
                {

                }
                else if (isUnionNightThree == true)
                {

                }
                if (isUnionBonusComplete == true)
                {

                }

                break;

            case (CurrentMission.CIA):
                if (isCIANightOne == true)
                {

                }
                else if (isCIANightTwo == true)
                {

                }
                else if (isCIANightThree == true)
                {

                }
                if (isCIABonusComplete == true)
                {

                }

                break;

            case (CurrentMission.MAFIA):
                if (isMafiaNightOne == true)
                {

                }
                else if (isMafiaNightTwo == true)
                {

                }
                else if (isMafiaNightThree == true)
                {

                }
                if (isMafiaBonusComplete == true)
                {

                }

                break;
            case (CurrentMission.VOICES):

                if (isVoicesNightOne == true)
                {

                }
                else if (isVoicesNightTwo == true)
                {

                }
                else if (isVoicesNightThree == true)
                {

                }
                if (isVoicesBonusComplete == true)
                {

                }

                break;
        }

    }//END Start


    #endregion Monobehaviors & Startup


    #region Methods


    //--------------------------//
    void ChangePortrait()
    //--------------------------//
    {
        //int i;
        //int f;
        if(currentDialogueValue == 0) //Use this to adjust portrait images
        {
          //i = 0;
          //f = 0;

            
        }

    }//END ChangePortrait

    //--------------------------//
    void OpenChoice()
    //--------------------------//
    {
        choicePanel.SetActive(true);
        textBox.SetActive(false);
        speakerBox.SetActive(false);

    }//END OpenChoice

    //-----------------------//
    public void StartDialogue(DialogueManager dialogue)
    //-----------------------//
    {


        speakerText.text = dialogue.characterName;

        sentences.Clear();

        dialogueAnimator.SetBool("isDialogueOpen", true);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }//END StartDialogue

    //-----------------------//
    public void DisplayNextSentence()
    //-----------------------//
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

    }//END DisplayNextSentence

    //-----------------------//
    public void EndDialogue()
    //-----------------------//
    {
        Debug.Log("End of Convo");
        dialogueAnimator.SetBool("isDialogueOpen", false);

        return;

    }//END EndDialogue


    #endregion Methods


    #region Coroutines


    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingTime); //Is the wait time between typed letters
        }
    }


    #endregion


}//END NarrativeUIManager
