using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

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

    [SerializeField] private Button skipButton;

    [SerializeField] private int dialoguePortraitSwapIndex;

    [SerializeField] private int[] skipIndex;
    private int currentSkipIndex = 0;
    private int lastSkipIndex;
    private bool isPastLastSkip = false;

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
    //[SerializeField] private Sprite[] portrait1ImageList;

    [SerializeField] private Image portrait2Image;
    //[SerializeField] private Sprite[] portrait2ImageList;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] backgroundImageList;


    private bool isResponding;
    private Response currentResponse;


    #endregion Components


    #region Monobehaviors & Startup


    //--------------------------//
    void Start()
    //--------------------------//
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        Init();

    }//END Start

    //--------------------------//
    void Init()
    //--------------------------//
    {
        sentences = new Queue<string>();
        lastSkipIndex = Enumerable.Last(skipIndex);

    }//END Init



    #endregion Monobehaviors & Startup


    #region Methods


    //-----------------------//
    public void StartDialogue(Dialogue dialogue)
    //-----------------------//
    {
        speakerText.text = dialogue.characterName;

        portrait1Image.sprite = dialogue.characterOneSprite;
        portrait2Image.sprite = dialogue.characterTwoSprite;


        if (sentences != null)
        {
            sentences.Clear();
        }

        dialogueAnimator.SetBool("isDialogueOpen", true);


        foreach (string sentence in dialogue.sentences)
        {
            char[] lettters = sentence.ToCharArray();

            sentences.Enqueue(sentence);
        }
        choiceManager.currentChoice = dialogue;

        DisplayNextSentence();

    }//END StartDialogue


    //-----------------------//
    public void StartResponse(Response response)
    //-----------------------//
    {
        isResponding = true;

        currentResponse = response;
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
    public void DisplayNextResponseSentence(Response response)
    //-----------------------//
    {
        /*
        if (dialogueManager.currentDialogueIndex == skipIndex[currentSkipIndex] && dialogueManager.currentDialogueIndex == lastSkipIndex)
        {
            skipButton.gameObject.SetActive(false);
            isPastLastSkip = true;

        }
        if (isPastLastSkip == false) ;
        {
            skipButton.gameObject.SetActive(true);
        }
        */
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
    public void DisplayNextSentence()
    //-----------------------//
    {

        /*
        if (dialogueManager.currentDialogueIndex == skipIndex[currentSkipIndex] && dialogueManager.currentDialogueIndex == lastSkipIndex)
        {
            skipButton.gameObject.SetActive(false);
            isPastLastSkip = true;

        }
        if (isPastLastSkip == false);
        {
            skipButton.gameObject.SetActive(true);
        }
        */
        //if()

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

            isResponding = false;
            dialogueManager.currentDialogueIndex++;


            dialogueManager.TriggerDialogue();
        }


        return;

    }//END EndDialogue

    //-----------------------//
    public void IncrementResponse()
    //-----------------------//
    {
        speakerText.text = currentResponse.responseSpeaker[currentResponse.responseID];
        currentResponse.responseID++;
    }//END IncrementResponse

    //-----------------------//
    public void SkiptoChoice()
    //-----------------------//
    {

        try
        {

        if (dialogueManager.currentDialogueIndex < skipIndex[currentSkipIndex])
        {
            dialogueManager.currentDialogueIndex = skipIndex[currentSkipIndex];
            dialogueManager.TriggerDialogue();

        }
        else if (dialogueManager.currentDialogueIndex > skipIndex[currentSkipIndex])
        {
            currentSkipIndex++;
            SkiptoChoice(); //Continues incrementing the index, skips once it gets to the next one.

        }
        }
        catch
        {
            isPastLastSkip = true;
            skipButton.gameObject.SetActive(false);
        }



        skipButton.gameObject.SetActive(false);

    }//END SkiptoChoice


    #endregion Methods


    #region Coroutines


    IEnumerator TypeSentence(string sentence)
    {

        if (isResponding)
        {
            IncrementResponse();
        }

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


    IEnumerator TypeResponse(Response response, string sentence)
    {
        speakerText.text = response.responseSpeaker[response.responseID];
        response.responseID++;

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
