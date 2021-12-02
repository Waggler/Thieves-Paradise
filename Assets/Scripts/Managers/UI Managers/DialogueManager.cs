using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private NarrativeUIManager narrativeUIManager;
    private float delayTime = 0.05f;

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
        try
        {
            narrativeUIManager.StartDialogue(dialogue[currentDialogueIndex]);
            narrativeUIManager.ChangeLeftPortrait();
            narrativeUIManager.ChangeRightPortrait();
        }
        catch
        {
            Debug.Log("DialogueIndex Exceeded Array Bounds");
        }

    }//END TriggerDialogue

    public void TriggerResponse(Response response)
    {
        narrativeUIManager.StartResponse(response);
    }

    IEnumerator IStartDelay()
    {
        yield return new WaitForSeconds(delayTime);
        TriggerDialogue();
    }

}//END DialogueManager
