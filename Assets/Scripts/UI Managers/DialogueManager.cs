using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private NarrativeUIManager narrativeUIManager;

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
        TriggerDialogue();

    }//END Start


    //-----------------------//
    public void TriggerDialogue()
    //-----------------------//
    {
        narrativeUIManager.StartDialogue(dialogue[currentDialogueIndex]);


    }//END TriggerDialogue


}//END DialogueManager
