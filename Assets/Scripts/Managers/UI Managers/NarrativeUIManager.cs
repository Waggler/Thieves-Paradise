using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NarrativeUIManager : MonoBehaviour
{


    #region Components

    [Header("Components")]
    [SerializeField] private float typingTime;
    [SerializeField] private Animator dialogueAnimator;

    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private ChoiceManager choiceManager;

    [Header("Dialogue Components")]
    [SerializeField] private GameObject speakerBox;
    [SerializeField] private GameObject textBox;

    [SerializeField] private int dialoguePortraitSwapIndex;

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

    [Header("Images")]
    [SerializeField] private Image portrait1Image;
    [SerializeField] private Sprite[] portrait1ImageList;

    [SerializeField] private Image portrait2Image;
    [SerializeField] private Sprite[] portrait2ImageList;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] backgroundImageList;

    [SerializeField] private int rightCharacterOneIntroIndex;
    [SerializeField] private int rightCharacterTwoIntroIndex;
    [SerializeField] private int rightCharacterThreeIntroIndex;
    [SerializeField] private int rightCharacterFourIntroIndex;
    [SerializeField] private int leftCharacterOneIntroIndex;
    [SerializeField] private int leftCharacterTwoIntroIndex;
    [SerializeField] private int leftCharacterThreeIntroIndex;
    [SerializeField] private int leftCharacterFourIntroIndex;

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
        sentences = new Queue<string>();

    }//END Init



    #endregion Monobehaviors & Startup


    #region Methods


    //--------------------------//
    public void ChangeLeftPortrait()
    //--------------------------//
    {

        if (dialogueManager.currentDialogueIndex == leftCharacterOneIntroIndex) 
        {
            portrait1Image.sprite = portrait1ImageList[0];

        }
        if (dialogueManager.currentDialogueIndex == leftCharacterTwoIntroIndex) 
        {
            portrait1Image.sprite = portrait1ImageList[1];

        }
        if (dialogueManager.currentDialogueIndex == leftCharacterThreeIntroIndex)
        {
            portrait1Image.sprite = portrait1ImageList[2];

        }
        if (dialogueManager.currentDialogueIndex == leftCharacterFourIntroIndex)
        {
            portrait1Image.sprite = portrait1ImageList[3];

        }

    }//END ChangeLeftPortrait

    //--------------------------//
    public void ChangeRightPortrait()
    //--------------------------//
    {

        if (dialogueManager.currentDialogueIndex == rightCharacterOneIntroIndex)
        {
            portrait2Image.sprite = portrait2ImageList[0];

        }
        if (dialogueManager.currentDialogueIndex == rightCharacterTwoIntroIndex)
        {
            portrait2Image.sprite = portrait2ImageList[1];

        }
        if (dialogueManager.currentDialogueIndex == rightCharacterThreeIntroIndex)
        {
            portrait2Image.sprite = portrait2ImageList[2];

        }
        if (dialogueManager.currentDialogueIndex == rightCharacterFourIntroIndex)
        {
            portrait2Image.sprite = portrait2ImageList[3];

        }

    }//END ChangeRightPortrait

    //-----------------------//
    public void StartDialogue(Dialogue dialogue)
    //-----------------------//
    {
        speakerText.text = dialogue.characterName;

        if (sentences != null)
        {
            sentences.Clear();
        }

        dialogueAnimator.SetBool("isDialogueOpen", true);

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        choiceManager.currentChoice = dialogue;

        DisplayNextSentence();

    }//END StartDialogue


    //-----------------------//
    public void StartResponse(Response response)
    //-----------------------//
    {
        speakerText.text = response.responseSpeaker;

        if (sentences != null)
        {
            sentences.Clear();
        }

        dialogueAnimator.SetBool("isDialogueOpen", true);

        foreach (string sentence in response.responseOption)
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
        if (choiceManager.currentChoice.isChoice == true)
        {
            choiceManager.InitChoices();
        }
        else
        {


            dialogueManager.currentDialogueIndex++;


            dialogueManager.TriggerDialogue();
        }


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
