using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RadioManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private TMP_Text speakerText;

    [SerializeField] private Animator subtitleAnimator;


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


    }//END DisplayNextSentence

    //-----------------------//
    public void EndDialogue()
    //-----------------------//
    {
        Debug.Log("End of Convo");


        subtitleAnimator.SetBool("isSubtitlesOpen", false);

        return;

    }//END EndDialogue


    #endregion Methods


}//END RadioManager
