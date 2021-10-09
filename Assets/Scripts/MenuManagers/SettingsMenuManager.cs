using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;


    #region Methods


    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {

        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);


    }//END ChangeScreen


    #endregion Methods


}//END SettingsMenuManager
