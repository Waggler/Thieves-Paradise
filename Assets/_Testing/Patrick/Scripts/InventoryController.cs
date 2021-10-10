using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemInterface[] inventory;
    private bool[] inventorySpace; //true = occupied, false = empty
    private int activeItem; //array index

    [SerializeField] private int inventorySize = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = new ItemInterface[inventorySize];
        inventorySpace = new bool[inventorySize];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwapItem(int selection)
    {
        activeItem = selection;
    }

    private void UseActiveItem()
    {
        inventory[activeItem].UseItem();
    }

    private void AddItem(ItemInterface newItem)
    {
        for(int i = 0; i < inventorySpace.Length; i++)
        {
            if(inventorySpace[i] == false)
            {
                inventory[i] = newItem;
                inventorySpace[i] = true;
                newItem.PickupItem();
            }
        }
    }//END AddItem

    private void DropActiveItem()
    {
        if(inventorySpace[activeItem] == true)
        {
            inventory[activeItem].DropItem();
            inventorySpace[activeItem] = false;
        }
    }
}
