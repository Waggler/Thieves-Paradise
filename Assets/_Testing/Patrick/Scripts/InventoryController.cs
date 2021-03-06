using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventoryController : MonoBehaviour
{
    public ItemInterface[] itemInterfaceInventory;
    private bool[] inventorySpace; //true = occupied, false = empty
    private int activeItemIndex; //array index
    private List<ItemInterface> nearbyItems;//for storing all items within reach
    private GameObject[] hotbarMesh = new GameObject[4];

    [HideInInspector] public float throwForce;
    [SerializeField] private float throwForceCap = 500;
    [HideInInspector] public bool isHoldingItem;
    [HideInInspector] public Vector3 throwVector;
    

    private int inventorySize = 4;
    [SerializeField] private Transform holdItemPos;

    private LayerMask layerMask;
    private InputManager im;

    [SerializeField] private bool AutoThrowForce;

    private ItemTracker itemTracker;
    private ScoreKeeper scoreKeeper;
    
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

        layerMask = ~LayerMask.GetMask("Player");

        im = GetComponent<InputManager>();
        itemTracker = (ItemTracker)FindObjectOfType(typeof(ItemTracker));

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private bool throwing;

    // Update is called once per frame
    void Update()
    {
        if(throwing)
        {
            ThrowForceCalc();
        }
    }

    private void ThrowForceCalc()
    {
        if (AutoThrowForce)
        {
            //print(throwForce);
            throwForce += Time.deltaTime * 200;   
        }

        throwForce = Mathf.Clamp(throwForce, 50, throwForceCap); //bound the throw force to a min and max value

        //convert to a vector for calcs
        throwVector = transform.forward * throwForce + transform.up * throwForce * 1.25f;
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
            //print("Grabbed Item");
            //newItem.myself.SetActive(false);
            //newItem.myself.transform.position = Vector3.down * 1000;

            nearbyItems.Remove(newItem);
            nearbyItems.TrimExcess();
        }
    }

    public bool CanPickupItems()
    {
        return !IsInventoryFull();
    }

    public bool IsActiveSlotEmpty()
    {
        return !inventorySpace[activeItemIndex];
    }
    public void UseItemPrimary(InputAction.CallbackContext context)
    {
        //print("Attempting to use Item");
        if (inventorySpace[activeItemIndex] == false)
        {
            return;
        }

        if (throwing)
        {
            //cancel throwing state
            GetComponentInChildren<AnimationController>().IsPlayerWinding(false);
            throwForce = 0;
            throwing = false;
            im.ZoomCancel();
            return;
        }
        
        if (context.started)
        {
            itemInterfaceInventory[activeItemIndex].UseItem();
        }
        if (context.canceled)
        {
            itemInterfaceInventory[activeItemIndex].UseItemEnd();
        }
    }
    public void UseItemSecondary(InputAction.CallbackContext context)
    {
        

        GetComponentInChildren<AnimationController>().IsPlayerWinding(true);
        if (context.started)
        {
            if (inventorySpace[activeItemIndex] == false || itemInterfaceInventory[activeItemIndex].isKeyItem == true)
            {
                //cancel if current selected item slot is empty
                print("Slot Empty");
                return;
            }

            throwing = true;
            //display throw arc preview
            scoreKeeper.itemUseCount++;
        }
        if (context.canceled && throwing == true)
        {
            GetComponentInChildren<AnimationController>().IsPlayerWinding(false);
            //Throw the Item
            //print("Item Secondary");
            ThrowItem();
            throwForce = 0;
            throwing = false;
        }
    }
    public void Item1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwapItem(0);
        }
    }
    public void Item2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwapItem(1);
        }
    }
    public void Item3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwapItem(2);
        }
    }
    public void Item4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SwapItem(3);
        }
    }
    #endregion //player input

    private void ThrowItem()
    {
        if (inventorySpace[activeItemIndex] == false)
        {
            //cancel if current selected item is empty
            print("Slot Empty");
            return;
        }else if (itemInterfaceInventory[activeItemIndex].isKeyItem == true)
        {
            print("Slot holds Key item");
            return;
        }
        //throw active item


        //play anim
        GetComponentInChildren<AnimationController>().TriggerLowThrow(true);
        GetComponentInChildren<AnimationController>().TriggerLowThrow(false);
        GetComponentInChildren<AnimationController>().IsPlayerWinding(false);

        if (inventorySpace[activeItemIndex] == false)
        {
            //cancel if current selected item is empty
            print("Slot Empty");
            return;
        }
        //check if something is in the way of the spawn
        /* if (Physics.Raycast(holdItemPos.position,transform.forward, 1f, layerMask, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(holdItemPos.position,transform.forward, Color.red, 0.5f);
            print("Something is in the way");
            return;
        } */
        
        if (throwForce < 200)
        {
            throwForce = 200;
        }else if (throwForce > 500)
        {
            throwForce = 500;
        }

        print(throwForce);
        //reset item to normal status
        ResetItem(activeItemIndex);

        //throw active item
        GameObject thrownItem = Instantiate(itemInterfaceInventory[activeItemIndex].myself, holdItemPos.position + transform.forward, Quaternion.identity);
        thrownItem.SetActive(true);
        thrownItem.name = thrownItem.GetComponent<ItemInterface>().itemName;
        //throwVector = transform.forward * throwForce + transform.up * throwForce;
        
        thrownItem.GetComponent<Rigidbody>().AddForce(throwVector);
        thrownItem.GetComponent<Rigidbody>().AddTorque(Vector3.one * Random.Range(5f,15f));
        thrownItem.GetComponent<ItemSuperScript>().ThrowItem();
        
        RemoveActiveItem();
    }

    private void SwapItem(int selection)
    {
        //play anim
        GetComponentInChildren<AnimationController>().TriggerItemSwitch(true);
        GetComponentInChildren<AnimationController>().TriggerItemSwitch(false);

        activeItemIndex = selection;
        //update UI Visual
        for(int i = 0; i < hotbarMesh.Length; i++)
        {
            if (activeItemIndex == i && inventorySpace[i] == true)
            {
                hotbarMesh[i].transform.localScale = Vector3.one * 5 * itemInterfaceInventory[i].UIScalar;
            }else if (inventorySpace[i] == true) //otherwise the space is empty and doesn't matter
            {
                hotbarMesh[i].transform.localScale = Vector3.one * 4 * itemInterfaceInventory[i].UIScalar;
            }else
            {
                hotbarMesh[i].transform.localScale = Vector3.one; //emergency catch for if something needs this for some reason
            }
        }
        //store whether they're displaying an item or not for animation purposes
        if (inventorySpace[activeItemIndex] == false)
        {
            isHoldingItem = false;
        }else if (itemInterfaceInventory[activeItemIndex].isKeyItem)
        {
            isHoldingItem = false;
        }else
        {
            isHoldingItem = true;
        }

        //delay the change in display for the animation to play
        StartCoroutine(DisplayDelay());
    }

    private IEnumerator DisplayDelay()
    {
        yield return new WaitForSeconds(0.4f);
        ChangeHeldItemDisplay();
    }

    private void ChangeHeldItemDisplay()
    {
        for (int i = 0; i < itemInterfaceInventory.Length; i++)
        {
            if (inventorySpace[i] == true) //first check if the slot isn't empty
            {
                GameObject curObj = itemInterfaceInventory[i].myself;
                if (i == activeItemIndex)
                {
                    if (!itemInterfaceInventory[i].isKeyItem) //make sure it's not an objective item
                    {//only display non-objective items
                        DisplayItem(i);
                    }else
                    {//make sure to actually get rid of the key item otherwise
                        ResetItem(i);
                    }
                }else
                {
                    ResetItem(i);
                }
            }
        }
    }

    private void DisplayItem(int displayIndex)
    {
        GameObject curObj = itemInterfaceInventory[displayIndex].myself;

        curObj.transform.SetParent(holdItemPos);
        curObj.transform.position = holdItemPos.position;
        curObj.GetComponent<Rigidbody>().isKinematic = true;

        //disabling colliders to try and prevent unwanted interactions
        //this isn't a good long-term solution because an item might have different kinds of colliders
        //curObj.GetComponent<SphereCollider>().enabled = false;
        //curObj.GetComponent<BoxCollider>().enabled = false;

        //instead going to try setting it to a ghost layer instead as that may be more practical down the line

        curObj.layer = 11;//11 is the ghost layer where all interactions are disabled


        curObj.SetActive(true);        
    }

    private void ResetItem(int resetIndex)
    {
        GameObject curObj = itemInterfaceInventory[resetIndex].myself;
        curObj.transform.SetParent(null);
                    
        curObj.GetComponent<Rigidbody>().isKinematic = false;

        //curObj.GetComponent<SphereCollider>().enabled = true;
        //curObj.GetComponent<BoxCollider>().enabled = true;

        curObj.layer = 0;//default layer to re-enable interactions

        curObj.SetActive(false);
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
        //print(newItemIndex);
        //update mesh
        hotbarMesh[newItemIndex].GetComponent<MeshFilter>().mesh = newItem.myself.GetComponent<MeshFilter>().mesh;
        hotbarMesh[newItemIndex].GetComponent<MeshRenderer>().material = newItem.myself.GetComponent<MeshRenderer>().material;
        nearbyItems.Remove(newItem);
        nearbyItems.TrimExcess();

        SwapItem(newItemIndex);
        ChangeHeldItemDisplay();
        //DisplayItem(newItemIndex);

        //update Item Tracker
        if (itemTracker != null) itemTracker.CheckStatus();
        
        CheckCollectables(newItem.itemName);
    }//END AddItem

    private void CheckCollectables(string ItemName)
    {
        switch(ItemName)
        {
            case "Borgor Cup":
            {
                PlayerPrefs.SetInt("isCupPickedUp", 1);
                break;
            }
            case "Laser Pointer":
            {
                PlayerPrefs.SetInt("isLaserPickedUp", 1);
                break;
            }
            case "Puddlemaker":
            {
                PlayerPrefs.SetInt("isBalloonPickedUp", 1);
                break;
            }
            case "Keycard":
            {
                PlayerPrefs.SetInt("isCardPickedUp", 1);
                break;
            }
            case "Bottle":
            {
                PlayerPrefs.SetInt("isBottlePickedUp", 1);
                break;
            }
            case "Smoke Bomb":
            {
                PlayerPrefs.SetInt("isSmokePickedUp", 1);
                break;
            }
        }
    }

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
        ChangeHeldItemDisplay();
    }

    public bool CheckHasItem(string keyItemName)
    {
        //print("checking items");
        for(int i = 0; i < itemInterfaceInventory.Length; i++)
        {
            if (inventorySpace[i] == true && itemInterfaceInventory[i].myself.name == keyItemName)
            {
                //print("got the item!");
                return true;
            }
            //print("not the item");
        }
        return false;
    }

    public bool CheckSlotForKeyItem(int slot)
    {
        if(slot > 3 || slot < 0)
        {
            //make sure it stays in the bounds of the array
            return false;
        }
        if (inventorySpace[slot] == false)
        {
            return false;
        }
        if (itemInterfaceInventory[slot].isKeyItem == true)
        {
            return true;
        }
        return false;
    }

    public int GetActiveItemIndex()
    {
        return activeItemIndex;
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

    #region Saving&Loading
    public ItemInterface[] SaveInventory()
    {
        return itemInterfaceInventory;
    }
    
    public void LoadInventory(ItemInterface[] savedItems)
    {
        foreach(ItemInterface item in savedItems)
        {
            if(item != null)
            {
                AddItem(item);
            }
        }
    }

    #endregion
}
