using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class itemCheckerScript : MonoBehaviour
{
    public string sceneNameToLoad;
    [Tooltip("Each item must have a unique name")]
    public string[] keyItemName;
    public float numOfItemsNeededToWin = 1;

    [HideInInspector] public float percentOfItemsGot;
    //public TextMeshProUGUI checkText;
    // Start is called before the first frame update

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
        SceneManager.LoadScene(sceneNameToLoad);
    }
}
