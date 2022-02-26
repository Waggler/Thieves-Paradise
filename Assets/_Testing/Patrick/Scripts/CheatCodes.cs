using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CheatCodes : MonoBehaviour
{
    
    public GameObject cheatMenu;
    public TextMeshProUGUI godText;
    private bool cheatsEnabled;
    private bool isGod;
    

    void Update()
    {
        if (cheatsEnabled)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                ToggleGodMode();
            }
            if (godText.text == "God Mode Enabled (G)")
            {
                PlayerMovement player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
                if (player.hp < 5)
                {
                    godText.text = "God Mode Disabled (G)";
                }
            }
        }
    }

    public void ShowMenu()
    {
        if (cheatsEnabled)
        {
            cheatMenu.SetActive(false);
            cheatsEnabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            cheatMenu.SetActive(true);
            cheatsEnabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void LoadScene(int scene)
    {
        ShowMenu();
        SceneManager.LoadScene(scene);
    }

    public void ToggleGodMode()
    {
        print("Attempting to Set God Mode");
        PlayerMovement player = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        if (player == null)
        {
            print("Player Not Found");
            return;
        }
        if (player.hp < 5)
        {
            print("God Mode Enabled");
            player.hp = 99999;
            godText.text = "God Mode Enabled (G)";
        }else
        {
            godText.text = "God Mode Disabled (G)";
            player.hp = 2;
        }
    }
}
