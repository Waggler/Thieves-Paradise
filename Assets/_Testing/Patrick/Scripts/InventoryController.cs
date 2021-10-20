using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemInterface[] inventory;
    private ItemSuperScript[] inventory2;
    private GameObject[] gameObjectInventory;
    private bool[] inventorySpace; //true = occupied, false = empty
    private int activeItem; //array index
    private List<GameObject> nearbyItems;//for storing all items within reach

    [SerializeField] private int inventorySize = 4;
    [SerializeField] private Transform holdItemPos;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = new ItemInterface[inventorySize];
        inventory2 = new ItemSuperScript[inventorySize];
        gameObjectInventory = new GameObject[inventorySize];

        inventorySpace = new bool[inventorySize];
        nearbyItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #region PlayerInput

    //Reciever for Player Input
    public void Interact()
    {
        if (nearbyItems.Count > 0)
        {            
            GameObject newItem;
            if (nearbyItems.Count == 1)
            {
                newItem = nearbyItems[0];
            }else 
            {
                newItem = GetNearestItem();
            }
            if (!IsInventoryFull())
            {
                AddItem(newItem);
                print("Grabbed Item");
                newItem.SetActive(false);
                nearbyItems.Remove(newItem);
            }
        }else
        {
            print("No Items Nearby");
        }
    }
    public void UseItemPrimary()
    {
        //do whatever the active item can
        print("Use Item");
    }
    public void UseItemSecondary()
    {
        if (inventorySpace[activeItem] == false)
        {
            //cancel if current selected item is empty
            return;
        }
        //throw active item
        print("Throw Item");
        GameObject thrownItem = Instantiate(gameObjectInventory[activeItem], holdItemPos.position, Quaternion.identity);
        thrownItem.SetActive(true);
        thrownItem.GetComponent<Rigidbody>().AddForce(Vector3.forward * 5 + Vector3.up * 5);
    }
    public void Item1()
    {
        SwapItem(0);
    }
    public void Item2()
    {
        SwapItem(1);
    }
    public void Item3()
    {
        SwapItem(2);
    }
    public void Item4()
    {
        SwapItem(3);
    }
    #endregion //player input

    private void SwapItem(int selection)
    {
        activeItem = selection;
        //update UI Visual
    }

    private void UseActiveItem()
    {
        inventory[activeItem].UseItem();
    }
    //check for if we have space in the inventory
    private bool IsInventoryFull()
    {
        bool full = true;
        for(int i = 0; i < inventorySpace.Length; i++)
        {
            if(inventorySpace[i] == false)
            {
                full = false;
            }
        }
        return full;
    }

    private void AddItem(GameObject newItem)
    {
        //attempt to fill the current active inventory slot
        if (inventorySpace[activeItem] == false)
        {
            gameObjectInventory[activeItem] = newItem;
            inventorySpace[activeItem] = true;
        }else
        {
            //otherwise fill the first open space
            for(int i = 0; i < inventorySpace.Length; i++)
            {
                if(inventorySpace[i] == false)
                {
                    gameObjectInventory[i] = newItem;
                    inventorySpace[i] = true;
                }
            }
        }
        
    }//END AddItem

    private void DropActiveItem()
    {
        if(inventorySpace[activeItem] == true)
        {
            
            inventorySpace[activeItem] = false;
        }
    }

    #region Nearby Items
    //stores all items currently within reach of the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            nearbyItems.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            nearbyItems.Remove(other.gameObject);
        }
    }

    private GameObject GetNearestItem()
    {
        if(nearbyItems.Count == 0)
        {
            //don't do anything if nothing is nearby
            return null;
        }
        float minDistance = 100;
        int itemIndex = 0;
        for (int i = 0; i < nearbyItems.Count; i++)
        {
            //find the item closest to the player
            Vector3 distance = nearbyItems[i].transform.position - transform.position;
            if (distance.magnitude < minDistance)
            {
                minDistance = distance.magnitude;
                itemIndex = i;
            }
        }
        print(nearbyItems[itemIndex].name);
        return nearbyItems[itemIndex];
    }
    #endregion
}
