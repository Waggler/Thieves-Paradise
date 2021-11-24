using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class itemCheckerScript : MonoBehaviour
{
    public string keyItemName;
    public TextMeshProUGUI checkText;
    // Start is called before the first frame update
    void Start()
    {
        checkText.text = "Have " + keyItemName + "?";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("touched something!");
        if (other.gameObject.GetComponent<InventoryController>() != null)
        {
            print("touched player!");
            if(other.gameObject.GetComponent<InventoryController>().CheckHasItem(keyItemName))
            {
                checkText.text = "Have " + keyItemName + "? Yes!";
                GoToWinScreen();
            }else
            {
                checkText.text = "Have " + keyItemName + "? No...";
            }

        }
    }
    private void OnTriggerExit()
    {
        checkText.text = "Have " + keyItemName + "?";
    }

    private void GoToWinScreen()
    {
        SceneManager.LoadScene(2);
    }
}
