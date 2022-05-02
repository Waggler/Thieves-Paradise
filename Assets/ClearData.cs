using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearData : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    private InitializeBasicSettings settingsIniter;
    private SettingsMenuManager settingsManager;


    private void Start()
    {
        settingsIniter = FindObjectOfType<InitializeBasicSettings>();
        settingsManager = FindObjectOfType<SettingsMenuManager>();
    }
    public void ClearPrefs()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        PlayerPrefs.DeleteAll();
        settingsIniter.SetDefaultSettings();
        settingsManager.Init();
    }
}
