using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{


    #region Components


    [Header("Components")]
    [SerializeField] private Image inventorySlot1;
    [SerializeField] private Image inventorySlot2;
    [SerializeField] private Image inventorySlot3;
    [SerializeField] private Image inventorySlot4;

    [SerializeField] private Image[] inventoryItems;

    [Header("Components")]
    [SerializeField] private TMP_Text slot1Text;
    [SerializeField] private TMP_Text slot2Text;
    [SerializeField] private TMP_Text slot3Text;
    [SerializeField] private TMP_Text slot4Text;

    [SerializeField] private TMP_Text checktext;

    [SerializeField] private Image reticle;

    [SerializeField] private bool isDebugMode;
    [SerializeField] private bool isReticleOn;


    #endregion Components


    #region Methods


    //-----------------------//
    void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    //-----------------------//
    void Init()
    //-----------------------//
    {
        if (isDebugMode == true)
        {
            slot1Text.gameObject.SetActive(true);
            slot2Text.gameObject.SetActive(true);
            slot3Text.gameObject.SetActive(true);
            slot4Text.gameObject.SetActive(true);
            checktext.gameObject.SetActive(true);
        }
        else if (isDebugMode == false)
        {
            slot1Text.gameObject.SetActive(false);
            slot2Text.gameObject.SetActive(false);
            slot3Text.gameObject.SetActive(false);
            slot4Text.gameObject.SetActive(false);
            checktext.gameObject.SetActive(false);
        }

        if (isReticleOn == true)
        {
            reticle.gameObject.SetActive(true);
        }
        else if (isReticleOn == false)
        {
            reticle.gameObject.SetActive(false);
        }

    }//END Init

    //-----------------------//
    void ChangeInventorySlotOneUI()
    //-----------------------//
    {


    }//END ChangeInventorySlotOneUI

    //-----------------------//
    void ChangeInventorySlotTwoUI()
    //-----------------------//
    {


    }//END ChangeInventorySlotTwoUI

    //-----------------------//
    void ChangeInventorySlotThreeUI()
    //-----------------------//
    {


    }//END ChangeInventorySlotThreeUI

    //-----------------------//
    void ChangeInventorySlotFourUI()
    //-----------------------//
    {


    }//END ChangeInventorySlotFourUI


    #endregion Methods


}//END InventoryUIManager
