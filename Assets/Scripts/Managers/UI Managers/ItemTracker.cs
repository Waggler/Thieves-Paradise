using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTracker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayoutGroup itemLayout;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject[] heistItemPrefabs;
    [SerializeField] private ItemScript[] heistItems;
    [SerializeField] private List<ItemToSteal> displayedItems;
    [SerializeField] private InventoryController iController;

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
        if (iController == null)
        {
            iController = FindObjectOfType<InventoryController>();
        }

        heistItems = new ItemScript[heistItemPrefabs.Length];

        for (int i = 0; i < heistItemPrefabs.Length; i++)
        {
            heistItems[i] = heistItemPrefabs[i].GetComponent<ItemScript>();
        }

        foreach (ItemScript item in heistItems)
        {
            ItemToSteal temp = Instantiate(itemPrefab, itemLayout.transform).GetComponent<ItemToSteal>();
            temp.Init(item);
            displayedItems.Add(temp);
        }

    }//END Init

    //-----------------------//
    public void CheckStatus()
    //-----------------------//
    {

        foreach (ItemToSteal item in displayedItems)
        {
            if (iController.CheckHasItem(item.name) == true)
            {
                item.nameText.fontStyle = TMPro.FontStyles.Strikethrough;
            }
        }

    }//END CheckStatus


}//END CLASS ItemTracker
