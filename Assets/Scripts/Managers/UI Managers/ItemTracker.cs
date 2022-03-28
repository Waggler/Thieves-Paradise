using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTracker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayoutGroup itemLayout;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private ItemScript heistItems;

    //-----------------------//
    void Start()
    //-----------------------//
    {
        /*
        foreach (ItemScript item in heistItems)
        {
            
            Instantiate(itemPrefab, itemLayout);
        }
        */
    }//END Start


}//END CLASS ItemTracker
