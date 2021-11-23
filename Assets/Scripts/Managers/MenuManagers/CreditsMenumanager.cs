using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenumanager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsScreen;


    #region Methods


    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {

        mainMenu.SetActive(true);
        creditsScreen.SetActive(false);


    }//END ChangeScreen


    #endregion Methods


}//END CreditsMenumanager
