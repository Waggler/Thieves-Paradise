using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioDialogueManager : MonoBehaviour
{
    [SerializeField] private RadioManager radioManager;
    private float delayTime = 0.05f;

    public Dialogue[] dialogue;
    [Tooltip("This is handled by the RadioManager script, do not alter unless you know what you're doing.")]
    public int currentDialogueIndex;


    //-----------------------//
    public void Init()
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
            radioManager.StartDialogue(dialogue[currentDialogueIndex]);
        }
        catch
        {
            Debug.Log("DialogueIndex Exceeded Array Bounds");
        }

    }//END TriggerDialogue

    IEnumerator IStartDelay()
    {
        yield return new WaitForSeconds(delayTime);
        TriggerDialogue();
    }

}//END RadioDialogueManager
