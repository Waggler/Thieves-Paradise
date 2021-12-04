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
    [SerializeField] private Image portrait2Image;
    [SerializeField] private Image backgroundImage;

    public Sprite[] characterImages;
    public Sprite[] backgroundImages;

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
            char[] letters = sentence.ToCharArray();

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

            dialogueManager.backgroundCurrentSpriteIndex++;
            dialogueManager.characterOneCurrentSpriteIndex++;
            dialogueManager.characterTwoCurrentSpriteIndex++;

            portrait1Image.sprite = characterImages[dialogueManager.characterOneCurrentSpriteIndex];
            portrait2Image.sprite = characterImages[dialogueManager.characterTwoCurrentSpriteIndex];
            //backgroundImage.sprite = backgroundImages[dialogueManager.backgroundCurrentSpriteIndex];
        


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
        bool skipDelimiter = false;
        foreach (char letter in sentence.ToCharArray())
        {

            if (letter == '<')
            {
                skipDelimiter = true;
            }
            if (!skipDelimiter)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingTime); //Is the wait time between typed letters
            }    
            else
            {
                dialogueText.text += letter;
            }

            if (letter == '>')
            {
                skipDelimiter = false;
            }

        }
    }


    #endregion


}//END NarrativeUIManager
