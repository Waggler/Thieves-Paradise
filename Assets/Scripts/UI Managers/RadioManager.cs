using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private RadioDialogueManager radioDialogueManager;

    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private TMP_Text speakerText;

    [SerializeField] private Animator subtitleAnimator;

    [SerializeField] private float typingTime;


    private Queue<string> sentences;


    #endregion Components


    #region Methods


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

    //-----------------------//
    public void StartDialogue(Dialogue dialogue)
    //-----------------------//
    {
        speakerText.text = dialogue.characterName;

        if (sentences != null)
        {
            sentences.Clear();
        }

        subtitleAnimator.SetBool("isSubtitlesOpen", true);

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


        subtitleAnimator.SetBool("isSubtitlesOpen", false);

        radioDialogueManager.currentDialogueIndex++;

        return;

    }//END EndDialogue



    IEnumerator TypeSentence(string sentence)
    {
        subtitleText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            subtitleText.text += letter;
            yield return new WaitForSeconds(typingTime); //Is the wait time between typed letters
        }
    }


    #endregion Methods


}//END RadioManager
