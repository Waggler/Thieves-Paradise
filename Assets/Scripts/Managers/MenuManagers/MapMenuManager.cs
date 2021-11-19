using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapMenuManager : MonoBehaviour
{
    #region Components


    [Header("Components")]
    [SerializeField] private VerticalLayoutGroup checklistLayoutGroup;

    [SerializeField] private Transform checklistTransform;

    [Header("Item Lists")]
    [SerializeField] private GameObject[] ladyItems;
    [SerializeField] private GameObject[] unionItems;
    [SerializeField] private GameObject[] massesItems;
    [SerializeField] private GameObject[] mafiaItems;
    [SerializeField] private GameObject[] ciaItems;
    [SerializeField] private GameObject[] voicesItems;

    [Header("Item Bools")]
    [SerializeField] private bool isItemOneStolen;
    [SerializeField] private bool isItemTwoStolen;
    [SerializeField] private bool isItemThreeStolen;
    [SerializeField] private bool isItemFourStolen;
    [SerializeField] private bool isItemFiveStolen;
    [SerializeField] private bool isItemSixStolen;
    [SerializeField] private bool isBonusTtemOneStolen;
    [SerializeField] private bool isBonusTtemTwoStolen;
    [SerializeField] private bool isBonusTtemThreeStolen;

    public enum Mission
    {
        LADY,
        UNION,
        MASSES,
        MAFIA,
        CIA,
        VOICES
    }

    public Mission currentMission;


    #endregion Components


    #region Methods


    //-----------------------//
    void PopulateChecklist()
    //-----------------------//
    {
        switch (currentMission)
        {
            case (Mission.LADY):
                foreach (GameObject checklistItem in ladyItems)
                {
                    Instantiate(checklistItem, checklistTransform);

                }

                break;
            case (Mission.UNION):
                foreach (GameObject checklistItem in unionItems)
                {
                    Instantiate(checklistItem, checklistTransform);

                }

                break;
            case (Mission.MASSES):
                foreach (GameObject checklistItem in massesItems)
                {
                    Instantiate(checklistItem, checklistTransform);

                }

                break;
            case (Mission.MAFIA):
                foreach (GameObject checklistItem in mafiaItems)
                {
                    Instantiate(checklistItem, checklistTransform);

                }

                break;
            case (Mission.CIA):
                foreach (GameObject checklistItem in ciaItems)
                {
                    Instantiate(checklistItem, checklistTransform);

                }

                break;
            case (Mission.VOICES):
                foreach (GameObject checklistItem in voicesItems)
                {
                    Instantiate(checklistItem, checklistTransform);

                }

                break;
        }


    }//END PopulateChecklist

    //-----------------------//
    void ItemStolen()
    //-----------------------//
    {
        if (isItemOneStolen == true)
        {
            
        }
        if (isItemTwoStolen == true)
        {

        }
        if (isItemThreeStolen == true)
        {

        }
        if (isItemFourStolen == true)
        {

        }
        if (isItemFiveStolen == true)
        {

        }
        if (isItemSixStolen == true)
        {

        }
        if (isBonusTtemOneStolen == true)
        {

        }
        if (isBonusTtemTwoStolen == true)
        {

        }
        if (isBonusTtemThreeStolen == true)
        {

        }

    }//END ItemStolen


    #endregion Methods


}//END MapMenuManager
