using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StolenItemMenuManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private GameObject collectionScreen;
    [SerializeField] private GameObject stolenItemScreen;

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private RectTransform stolenItemPanel;

    public int stolenNightItems;


    #endregion Components


    #region Methods


    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {

        collectionScreen.SetActive(true);
        stolenItemScreen.SetActive(false);


    }//END ChangeScreen

    //-----------------------//
    public void PopulateStolenItems ()
    //-----------------------//
    {
        for (int i = 0; i < stolenNightItems; i++)
        {
            Instantiate(itemPrefab, stolenItemPanel);

        }


    }//END PopulateStolenItems


    #endregion Methods


}//END StolenItemMenuManager
