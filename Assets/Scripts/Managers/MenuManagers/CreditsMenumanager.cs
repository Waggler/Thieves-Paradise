using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreditsMenumanager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsScreen;

    public Button creditsCloseButton;



    #region Methods


    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {

        mainMenu.SetActive(true);
        creditsScreen.SetActive(false);
        // Have to null before reset
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsCloseButton.gameObject);


    }//END ChangeScreen


    #endregion Methods


}//END CreditsMenumanager
