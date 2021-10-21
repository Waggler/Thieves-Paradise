using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    private ItemInterface[] inventory;
    private ItemSuperScript[] inventory2;
    private ItemScript[] itemScriptInventory;
    private bool[] inventorySpace; //true = occupied, false = empty
    private int activeItemIndex; //array index
    private List<ItemScript> nearbyItems;//for storing all items within reach
    public TextMeshProUGUI[] hotbarText;

    public float throwForce;
    

    [SerializeField] private int inventorySize = 4;
    [SerializeField] private Transform holdItemPos;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory = new ItemInterface[inventorySize];
        inventory2 = new ItemSuperScript[inventorySize];
        itemScriptInventory = new ItemScript[inventorySize];

        inventorySpace = new bool[inventorySize];
        nearbyItems = new List<ItemScript>();

        SwapItem(0);
    }

    private bool throwing;

    // Update is called once per frame
    void Update()
    {
        if(throwing)
        {
            throwForce += Time.deltaTime * 200;
        }
    }
    #region PlayerInput

    //Reciever for Player Input
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (nearbyItems.Count > 0)
            {            
                ItemScript newItem;
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
                    newItem.gameObject.SetActive(false);

                    nearbyItems.Remove(newItem);
                    nearbyItems.TrimExcess();
                }
            }else
            {
                print("No Items Nearby");
            }
        }
        
    }
    public void UseItemPrimary()
    {
        //do whatever the active item can
        print("Use Item");
    }
    public void UseItemSecondary(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            throwing = true;
            //display throw arc preview
        }
        if (context.canceled)
        {
            //Throw the Item
            print("Item Secondary");
            ThrowItem();
            throwForce = 0;
            throwing = false;
        }
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

    private void ThrowItem()
    {
        if (inventorySpace[activeItemIndex] == false)
        {
            //cancel if current selected item is empty
            print("Slot Empty");
            return;
        }
        //throw active item
        print("Throw Item");
        GameObject thrownItem = Instantiate(itemScriptInventory[activeItemIndex].myself, holdItemPos.position, Quaternion.identity);
        thrownItem.SetActive(true);
        thrownItem.name = thrownItem.GetComponent<ItemScript>().objectName;
        Vector3 throwVector = transform.forward * throwForce + transform.up * throwForce;
        thrownItem.GetComponent<Rigidbody>().AddForce(throwVector);
        print("Throw Force: " + throwVector);
        RemoveActiveItem();
    }
    private void SwapItem(int selection)
    {
        activeItemIndex = selection;
        //update UI Visual
        for(int i = 0; i < hotbarText.Length; i++)
        {
            if (activeItemIndex == i)
            {
                hotbarText[i].color = Color.blue;
            }else
            {
                hotbarText[i].color = Color.black;
            }
        }
    }

    private void UseActiveItem()
    {
        inventory[activeItemIndex].UseItem();
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

    private void AddItem(ItemScript newItem)
    {
        int newItemIndex = -1;
        print("Attempting to add " + newItem.name);
        //attempt to fill the current active inventory slot
        if (inventorySpace[activeItemIndex] == false)
        {
            itemScriptInventory[activeItemIndex] = newItem;
            inventorySpace[activeItemIndex] = true;
            newItemIndex = activeItemIndex;
        }else
        {
            //otherwise fill the first open space
            for(int i = 0; i < inventorySpace.Length; i++)
            {
                if(inventorySpace[i] == false && newItemIndex == -1)
                {
                    itemScriptInventory[i] = newItem;
                    inventorySpace[i] = true;
                    newItemIndex = i;
                }
            }
        }
        print(newItemIndex);
        hotbarText[newItemIndex].text = newItem.itemName;
        nearbyItems.Remove(newItem);
        nearbyItems.TrimExcess();
        
    }//END AddItem

    private void RemoveActiveItem()
    {
        if(inventorySpace[activeItemIndex] == true)
        {
            inventorySpace[activeItemIndex] = false;
            Destroy(itemScriptInventory[activeItemIndex].gameObject);
            
            itemScriptInventory[activeItemIndex] = null;
            hotbarText[activeItemIndex].text = "Item Slot " + (activeItemIndex + 1);
        }
    }

    public bool CheckHasItem(string keyItemName)
    {
        print("checking items");
        for(int i = 0; i < itemScriptInventory.Length; i++)
        {
            if (inventorySpace[i] == true && itemScriptInventory[i].objectName == keyItemName)
            {
                print("got the item!");
                return true;
            }
            print("not the item");
        }
        return false;
    }

    #region Nearby Items
    //stores all items currently within reach of the player
    private void OnTriggerStay(Collider other)
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            nearbyItems.Add(other.gameObject.GetComponent<ItemScript>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            nearbyItems.Remove(other.gameObject.GetComponent<ItemScript>());
            nearbyItems.TrimExcess();
        }
    }

    private ItemScript GetNearestItem()
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
