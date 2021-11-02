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
    [SerializeField] private Image[] portrait1ImageList;

    [SerializeField] private Image portrait2Image;
    [SerializeField] private Image[] portrait2ImageList;

    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image[] backgroundImageList;


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
    void ChangePortrait()
    //--------------------------//
    {

        if(dialogueManager.currentDialogueIndex == 0) //Use this to adjust portrait images
        {


            
        }

    }//END ChangePortrait


    //-----------------------//
    public void StartDialogue(Dialogue dialogue)
    //-----------------------//
    {
        speakerText.text = dialogue.characterName;

        if(sentences != null)
        {
            sentences.Clear();
        }

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

        if(dialogueManager.currentDialogueIndex == choiceManager.choiceIndex1)
        {
            choiceManager.ShowChoices(choiceManager.choiceAmount1, 1);
        }
        if (dialogueManager.currentDialogueIndex == choiceManager.choiceIndex2)
        {
            choiceManager.ShowChoices(choiceManager.choiceAmount2, 2);
        }
        if (dialogueManager.currentDialogueIndex == choiceManager.choiceIndex3)
        {
            choiceManager.ShowChoices(choiceManager.choiceAmount3, 3);
        }
        if (dialogueManager.currentDialogueIndex == choiceManager.choiceIndex4)
        {
            choiceManager.ShowChoices(choiceManager.choiceAmount4, 4);
        }
        if (dialogueManager.currentDialogueIndex == choiceManager.choiceIndex5)
        {
            choiceManager.ShowChoices(choiceManager.choiceAmount5, 5);
        }


        dialogueAnimator.SetBool("isDialogueOpen", false);
        dialogueManager.currentDialogueIndex ++;

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
