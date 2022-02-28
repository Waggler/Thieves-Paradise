using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private NarrativeUIManager narrativeUIManager;


    private float delayTime = 0.05f;

    public Dialogue[] dialogue;
    public int currentDialogueIndex;

    [Header("Levelmanager")]
    [SerializeField] private LevelManager levelManager;
    public int nextSceneIndex;
    public bool isEndCredits;
    public bool isLadyComplete;
    public bool isMassesComplete;
    public bool isMafiaComplete;

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
        delayTime = 0.05f;
        StartCoroutine(IStartDelay());
        //TriggerDialogue();
    }//END Init


    //-----------------------//
    public void TriggerDialogue()
    //-----------------------//
    {
        try
        {
            narrativeUIManager.StartDialogue(dialogue[currentDialogueIndex]);

        }
        catch
        {
            if (isLadyComplete == true)
            {
                PlayerPrefs.SetInt("isLadyComplete", 1);
            }
            if (isLadyComplete == true)
            {
                PlayerPrefs.SetInt("isMassesComplete", 1);
            }
            if (isLadyComplete == true)
            {
                PlayerPrefs.SetInt("endCredits", 1);
            }

            if (isEndCredits == true)
            {
                PlayerPrefs.SetInt("endCredits", 1);
            }

            Cursor.visible = false;

            levelManager.ChangeLevel(nextSceneIndex);

            //Debug.Log("DialogueIndex Exceeded Array Bounds");
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
