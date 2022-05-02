using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkipManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button skipButton;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private int skipButtonAppearIndex;

    //-----------------------//
    public void ShowSkip()
    //-----------------------//
    {
        if (dialogueManager.currentDialogueIndex == skipButtonAppearIndex)
        {
            skipButton.gameObject.SetActive(true);
        }

    }//END ShowSkip

    //-----------------------//
    public void ProcessSkip()
    //-----------------------//
    {
        StartCoroutine(IStartSkip());

    }//END ProcessSkip

    //-----------------------//
    public void StopSkip()
    //-----------------------//
    {
        StopAllCoroutines();


    }//END StopSkip

    //-----------------------//
    private IEnumerator IStartSkip()
    //-----------------------//
    {
        float holdTime = 0.1f;
        yield return new WaitForSeconds(holdTime);

        if (dialogueManager.isLadyComplete == true)
        {
            PlayerPrefs.SetInt("isLadyComplete", 1);
        }
        if (dialogueManager.isLadyComplete == true)
        {
            PlayerPrefs.SetInt("isMassesComplete", 1);
        }
        if (dialogueManager.isLadyComplete == true)
        {
            PlayerPrefs.SetInt("endCredits", 1);
        }

        if (dialogueManager.isEndCredits == true)
        {
            PlayerPrefs.SetInt("endCredits", 1);
        }

        Cursor.visible = false;

        levelManager.ChangeLevel(dialogueManager.nextSceneIndex);

    }//END IStartSkip

}//END Class SkipManager
