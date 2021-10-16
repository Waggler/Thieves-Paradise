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

    [SerializeField] private Transform stolenItemBank;
    [SerializeField] private Image stolenItem1;
    [SerializeField] private Image stolenItem2;
    [SerializeField] private Image stolenItem3;
    [SerializeField] private Image stolenItem4;


    //TODO link with gamecontroller for bool flags based on nights completed/items taken

    #endregion Components


    #region Methods



    //-----------------------//
    public void ChangeScreen(int screenValue)
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
            PopulateStolenItems();
        }


    }//END ChangeScreen

    //-----------------------//
    public void PopulateStolenItems() //TODO Utilize bool flags & enum to swap image of silhoeutte for the item
    //-----------------------//
    {
        
        


    }//END ChangeScreen


    #endregion Methods


}//END CollectionScreenManager
