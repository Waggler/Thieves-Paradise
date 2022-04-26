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

    public TMP_Text subtitleText;

    [SerializeField] private Animator subtitleAnimator;

    [SerializeField] private float typingTime;
    [SerializeField] private float waitTime;

    [SerializeField] private Image bolt1Image;
    [SerializeField] private Image bolt2Image;
    [SerializeField] private Sprite[] boltSprites;

    [SerializeField] private AudioSource radioSource;
    [SerializeField] private AudioClip radioClip;


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

        SetTextColor();

    }//END Init

    //--------------------------//
    void SetTextColor()
    //--------------------------//
    {
        float hue = PlayerPrefs.GetFloat("RadioHue");
        float saturation = PlayerPrefs.GetFloat("RadioSaturation");

        Color textColor = Color.HSVToRGB(hue, saturation, 1);

        subtitleText.color =textColor;

    }//END SetTextColor

    //-----------------------//
    public void StartDialogue(Dialogue dialogue)
    //-----------------------//
    {

        if (sentences != null)
        {
            sentences.Clear();
        }

        radioSource.PlayOneShot(radioClip);

        bolt1Image.sprite = boltSprites[Random.Range(0, boltSprites.Length)];
        bolt2Image.sprite = boltSprites[Random.Range(0, boltSprites.Length)];

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
        yield return new WaitForSeconds(waitTime);
        DisplayNextSentence();
    }


    #endregion Methods


}//END RadioManager
