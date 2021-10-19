using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemInterface[] inventory;
    private ItemSuperScript[] inventory2;
    private GameObject[] inventory3;
    private bool[] inventorySpace; //true = occupied, false = empty
    private int activeItem; //array index

    [SerializeField] private int inventorySize = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = new ItemInterface[inventorySize];
        inventory2 = new ItemSuperScript[inventorySize];
        inventory3 = new GameObject[inventorySize];

        inventorySpace = new bool[inventorySize];
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region PlayerInput
    private bool isItemNearby = false;
    //Reciever for Player Input
    public void Interact()
    {
        
        if (isItemNearby)
        {
            print("Grab Item");
        }else
        {
            print("Pressed Button");
        }
    }

    public void UseItemPrimary()
    {
        print("Use Item");
    }

    public void UseItemSecondary()
    {
        print("Use Item 2");
    }


    #endregion

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

    #region Nearby Items
    //stores all items currently within reach of the player
    private List<GameObject> nearbyItems;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<BasicItemScript>() != null)
        {
            isItemNearby = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BasicItemScript>() != null)
        {
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void GetNearestItem()
    {
        if(nearbyItems.Count == 0)
        {
            //don't do anything if nothing is nearby
            return;
        }
        float minDistance = 100;
        int itemIndex;
        for (int i = 0; i < nearbyItems.Count; i++)
        {
            
        }
    }
    #endregion
}
