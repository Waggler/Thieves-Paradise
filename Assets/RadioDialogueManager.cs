using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioDialogueManager : MonoBehaviour
{
    [SerializeField] private RadioManager radioManager;
    private float delayTime = 0.05f;

    public Dialogue[] dialogue;
    public int currentDialogueIndex;


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
