using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseLoadScreenManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject loadScreen;

    [Header("Saves List")]
    [SerializeField] private GameObject[] saves;

    [SerializeField] private GameObject savePrefab;

    [SerializeField] private RectTransform saveListTransform;

    [SerializeField] private int totalSaves;

    #endregion Components


    #region Methods


    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {

        pauseMenu.SetActive(true);
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

    /*      
    //-------------------------//
    public void IncreaseRosterSize()        //In case we want to make the save list hella big
    //-------------------------//
    {
        if (totalSaves >= 4)
        {
            saveListTransform.sizeDelta = new Vector2(saveListTransform.sizeDelta.x, saveListTransform.sizeDelta.y + 220f);
        }
        else if (totalSaves >= 7)
        {
            saveListTransform.sizeDelta = new Vector2(saveListTransform.sizeDelta.x, saveListTransform.sizeDelta.y + 220f);

        }
        else if (totalSaves >= 10)
        {
            saveListTransform.sizeDelta = new Vector2(saveListTransform.sizeDelta.x, saveListTransform.sizeDelta.y + 220f);

        }

    }//END IncreaseRosterSize
    */


    #endregion Methods


}//END PauseLoadScreenManager
