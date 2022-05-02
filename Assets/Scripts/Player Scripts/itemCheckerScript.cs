using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class itemCheckerScript : MonoBehaviour
{
    public string levelCompleteKey;
    public int sceneNumberToLoad;
    [Tooltip("Each item must have a unique name")]
    public string[] keyItemName;
    public float numOfItemsNeededToWin = 1;

    private ScoreScreenManager scoreScreenManager;

    [HideInInspector] public float percentOfItemsGot;
    //public TextMeshProUGUI checkText;
    private float timer;
    private int winTime;
    private void Start()
    {
        if (scoreScreenManager == null)
        {
            scoreScreenManager = FindObjectOfType<ScoreScreenManager>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("touched something!");
        if (other.gameObject.GetComponent<InventoryController>() != null)
        {
            float itemCount = 0;
            for(int i = 0; i < keyItemName.Length; i++)
            {
                if(other.gameObject.GetComponent<InventoryController>().CheckHasItem(keyItemName[i]))
                {
                    //checkText.text = "Have " + keyItemName + "? Yes!";
                    itemCount++;
                }else
                {
                    //checkText.text = "Have " + keyItemName + "? No...";
                }
            }
            //print("touched player!");
            if (itemCount >= numOfItemsNeededToWin)
            {
                percentOfItemsGot = itemCount / keyItemName.Length;
                GoToWinScreen();
            }else
            {
                //do something else if needed
            }
        }

        if (other.gameObject.GetComponent<BuddyHolder>() != null)
        {
            if (other.gameObject.GetComponent<BuddyHolder>().displayBuddy)
            {
                GoToWinScreen();
            }
        }
    }
    private void OnTriggerExit()
    {
        //checkText.text = "Have " + keyItemName + "?";
    }

    private void GoToWinScreen()
    {
        if (levelCompleteKey != null)
        {
            PlayerPrefs.SetInt(levelCompleteKey, 1);

        }
        winTime = (int) timer;
        scoreScreenManager.StartScoring();
        scoreScreenManager.nextSceneIndex = sceneNumberToLoad;
    }
}
