using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadScreenManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject loadScreen;

    [Header("Saves List")]
    [SerializeField] private GameObject[] saves;

    [SerializeField] private GameObject savePrefab;

    [SerializeField] private Transform saveListTransform;

    [SerializeField] private int totalSaves;

    #endregion Components


    #region Methods


    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {

        mainMenu.SetActive(true);
        loadScreen.SetActive(false);


    }//END ChangeScreen


    //-----------------------//
    public void PopulateSaves()
    //-----------------------//
    {
        for (int i = 0; i < totalSaves; i++)
        {
            Instantiate(savePrefab, saveListTransform);

        }

    }//END PopulateSaves


    #endregion Methods


}//END LoadScreenManager
