using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTracker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayoutGroup itemLayout;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] public GameObject[] heistItemObjects;
    [SerializeField] private float[] itemUIScales;
     private ItemScript[] heistItems;
     private List<ItemToSteal> displayedItems;
    [SerializeField] private InventoryController iController;
    [SerializeField] private GameObject[] itemDisplays;
    [SerializeField] private GameObject[] itemBoxes;
    [SerializeField] private GameObject[] checkmarks;

    public bool debug;

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

        //initialize object holders
        heistItems = new ItemScript[heistItemObjects.Length];
        displayedItems = new List<ItemToSteal>();

        //initialize list of items

        for (int i = 0; i < 3; i++)
        {
            if(i < heistItemObjects.Length)
            {
                heistItems[i] = heistItemObjects[i].GetComponent<ItemScript>();
                if(debug) print(heistItems[i].name); 
                //spawn in items to be displayed
                itemDisplays[i].GetComponent<MeshFilter>().mesh = heistItemObjects[i].GetComponent<MeshFilter>().mesh;
                itemDisplays[i].GetComponent<MeshRenderer>().material = heistItemObjects[i].GetComponent<MeshRenderer>().material;
                itemDisplays[i].transform.localScale = Vector3.one * itemUIScales[i];
            }else
            {
                itemDisplays[i].SetActive(false);
                itemBoxes[i].SetActive(false);
            }
            checkmarks[i].SetActive(false);

        }

        foreach (ItemScript item in heistItems)
        {
            ItemToSteal temp = Instantiate(itemPrefab, itemLayout.transform).GetComponent<ItemToSteal>();
            temp.Init(item);

            if(debug) print(displayedItems.Count); 

            displayedItems.Add(temp);
            
        }
        
    }//END Init

    //-----------------------//
    public void CheckStatus()
    //-----------------------//
    {

        //foreach (ItemToSteal item in displayedItems)
        for(int i = 0; i < displayedItems.Count; i++)
        {
            if (iController.CheckHasItem(displayedItems[i].name) == true)
            {
                displayedItems[i].nameText.fontStyle = TMPro.FontStyles.Strikethrough;

                checkmarks[i].SetActive(true);
            }
        }

    }//END CheckStatus


}//END CLASS ItemTracker
