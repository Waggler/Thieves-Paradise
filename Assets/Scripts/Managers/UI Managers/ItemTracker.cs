using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTracker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayoutGroup itemLayout;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject[] heistItemObjects;
     private ItemScript[] heistItems;
     private List<ItemToSteal> displayedItems;
    [SerializeField] private InventoryController iController;
    [SerializeField] private GameObject[] itemDisplays;
    [SerializeField] private GameObject[] itemBoxes;
    [SerializeField] private GameObject[] checkmarks;

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

        heistItems = new ItemScript[heistItemObjects.Length];

        //initialize list of items

        for (int i = 0; i < 3; i++)
        {
            if(i < heistItemObjects.Length)
            {
                heistItems[i] = heistItemObjects[i].GetComponent<ItemScript>();
                //spawn in items to be displayed
                itemDisplays[i].GetComponent<MeshFilter>().mesh = heistItemObjects[i].GetComponent<MeshFilter>().mesh;
                itemDisplays[i].GetComponent<MeshRenderer>().material = heistItemObjects[i].GetComponent<MeshRenderer>().material;
            }else
            {
                itemDisplays[i].SetActive(false);
            }

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
