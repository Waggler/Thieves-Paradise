using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class InventoryController : MonoBehaviour
{
    private ItemInterface[] itemInterfaceInventory;
    private bool[] inventorySpace; //true = occupied, false = empty
    private int activeItemIndex; //array index
    private List<ItemInterface> nearbyItems;//for storing all items within reach
    private GameObject[] hotbarMesh = new GameObject[4];

    private float throwForce;
    

    [SerializeField] private int inventorySize = 4;
    [SerializeField] private Transform holdItemPos;
    
    // Start is called before the first frame update
    void Start()
    {
        itemInterfaceInventory = new ItemInterface[inventorySize];

        inventorySpace = new bool[inventorySize];
        nearbyItems = new List<ItemInterface>();

        for(int i = 0; i < hotbarMesh.Length; i++)
        {
            string slotName = "Object Mesh (" + (i + 1) + ")";
            hotbarMesh[i] = GameObject.Find(slotName);

            hotbarMesh[i].GetComponent<MeshFilter>().mesh = null;
            hotbarMesh[i].GetComponent<MeshRenderer>().material = null;
        }

        SwapItem(0);

        GameObject debugPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
        debugPlatform.transform.position = Vector3.down * 1002;
        debugPlatform.transform.localScale = new Vector3(10, 1, 10);
    }

    private bool throwing;

    // Update is called once per frame
    void Update()
    {
        if(throwing && throwForce < 1000)
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
                ItemInterface newItem;
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
                    newItem.myself.SetActive(false);

                    nearbyItems.Remove(newItem);
                    nearbyItems.TrimExcess();
                }
            }else
            {
                print("No Items Nearby");
            }
        }
    }

    public void ContextInteract(ItemInterface newItem)
    {
        if (!IsInventoryFull())
        {
            AddItem(newItem);
            print("Grabbed Item");
            newItem.myself.SetActive(false);
            newItem.myself.transform.position = Vector3.down * 1000;

            nearbyItems.Remove(newItem);
            nearbyItems.TrimExcess();
        }
    }

    public bool CanPickupItems()
    {
        return !IsInventoryFull();
    }
    public void UseItemPrimary()
    {
        //do whatever the active item can
        //print("Use Item");
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
            //print("Item Secondary");
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
        
        GameObject thrownItem = Instantiate(itemInterfaceInventory[activeItemIndex].myself, holdItemPos.position, Quaternion.identity);
        thrownItem.SetActive(true);
        thrownItem.name = thrownItem.GetComponent<ItemInterface>().itemName;
        Vector3 throwVector = transform.forward * throwForce + transform.up * throwForce;
        thrownItem.GetComponent<Rigidbody>().AddForce(throwVector);
        thrownItem.GetComponent<Rigidbody>().AddTorque(Vector3.one * Random.Range(5f,15f));
        thrownItem.GetComponent<ItemSuperScript>().ThrowItem();

        
        RemoveActiveItem();
    }
    private void SwapItem(int selection)
    {
        activeItemIndex = selection;
        //update UI Visual
        for(int i = 0; i < hotbarMesh.Length; i++)
        {
            if (activeItemIndex == i)
            {
                hotbarMesh[i].transform.localScale = Vector3.one * 8;
            }else
            {
                hotbarMesh[i].transform.localScale = Vector3.one * 6;
            }
        }
    }

    private void UseActiveItem()
    {
        itemInterfaceInventory[activeItemIndex].UseItem();
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

    private void AddItem(ItemInterface newItem)
    {
        int newItemIndex = -1;
        print("Attempting to add " + newItem.myself.name);
        //attempt to fill the current active inventory slot
        if (inventorySpace[activeItemIndex] == false)
        {
            itemInterfaceInventory[activeItemIndex] = newItem;
            inventorySpace[activeItemIndex] = true;
            newItemIndex = activeItemIndex;
        }else
        {
            //otherwise fill the first open space
            for(int i = 0; i < inventorySpace.Length; i++)
            {
                if(inventorySpace[i] == false && newItemIndex == -1)
                {
                    itemInterfaceInventory[i] = newItem;
                    inventorySpace[i] = true;
                    newItemIndex = i;
                }
            }
        }
        print(newItemIndex);
        //update mesh
        hotbarMesh[newItemIndex].GetComponent<MeshFilter>().mesh = newItem.myself.GetComponent<MeshFilter>().mesh;
        hotbarMesh[newItemIndex].GetComponent<MeshRenderer>().material = newItem.myself.GetComponent<MeshRenderer>().material;
        nearbyItems.Remove(newItem);
        nearbyItems.TrimExcess();
        
    }//END AddItem

    private void RemoveActiveItem()
    {
        if(inventorySpace[activeItemIndex] == true)
        {
            inventorySpace[activeItemIndex] = false;
            Destroy(itemInterfaceInventory[activeItemIndex].myself);
            
            itemInterfaceInventory[activeItemIndex] = null;

            hotbarMesh[activeItemIndex].GetComponent<MeshFilter>().mesh = null;
            hotbarMesh[activeItemIndex].GetComponent<MeshRenderer>().material = null;
            //hotbarMesh[activeItemIndex].text = "Item Slot " + (activeItemIndex + 1);
        }
    }

    public bool CheckHasItem(string keyItemName)
    {
        print("checking items");
        for(int i = 0; i < itemInterfaceInventory.Length; i++)
        {
            if (inventorySpace[i] == true && itemInterfaceInventory[i].myself.name == keyItemName)
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
/*     private void OnTriggerEnter(Collider other)
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
    } */

    private ItemInterface GetNearestItem()
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
            Vector3 distance = nearbyItems[i].myself.transform.position - transform.position;
            if (distance.magnitude < minDistance)
            {
                minDistance = distance.magnitude;
                itemIndex = i;
            }
        }
        print(nearbyItems[itemIndex].myself.name);
        return nearbyItems[itemIndex];
    }
    #endregion
}
