using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private NarrativeUIManager narrativeUIManager;

    [Tooltip("Set this value to less than 0.09, it allows the narrativeUIManager to initialize")]
    [SerializeField] private float delayTime;

    public Dialogue[] dialogue;
    public int currentDialogueIndex;


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    //-----------------------//
    private void Init()
    //-----------------------//
    {
        StartCoroutine(IStartDelay());

    }//END Start


    //-----------------------//
    public void TriggerDialogue()
    //-----------------------//
    {
        narrativeUIManager.StartDialogue(dialogue[currentDialogueIndex]);


    }//END TriggerDialogue

    IEnumerator IStartDelay()
    {
        yield return new WaitForSeconds(delayTime);
        TriggerDialogue();
    }

}//END DialogueManager
