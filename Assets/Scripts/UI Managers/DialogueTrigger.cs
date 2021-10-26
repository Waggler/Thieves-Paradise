using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private NarrativeUIManager narrativeUIManager;

    public DialogueManager dialogue;

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
        narrativeUIManager.StartDialogue(dialogue);


    }//END TriggerDialogue


}//END DialogueTrigger
