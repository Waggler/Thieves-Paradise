using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectionScreenManager : MonoBehaviour
{
    #region Components


    [Header("Components")]
    [SerializeField] private GameObject collectionMenu;
    [SerializeField] private GameObject missionMenu;
    [SerializeField] private GameObject collectionNightMenu;


    //TODO link with gamecontroller for bool flags based on nights completed/items taken

    #endregion Components


    #region Methods



    //-----------------------//
    public void ChangeScreen(int screenValue) //TODO Link with stolenItemMenuManager to populate what items appear
    //-----------------------//
    {

        if (screenValue == 0)
        {
            collectionMenu.SetActive(false);
            missionMenu.SetActive(true);

        }
        else if (screenValue == 1)
        {
            collectionMenu.SetActive(false);
            collectionNightMenu.SetActive(true);
        }


    }//END ChangeScreen


    #endregion Methods


}//END CollectionScreenManager
