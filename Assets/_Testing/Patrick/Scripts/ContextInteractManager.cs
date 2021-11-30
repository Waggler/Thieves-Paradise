using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ContextInteractManager : MonoBehaviour
{
    //Controller References
    private InventoryController inventory;
    //Pushpull variable
    //door variable

    //List of Nearby Objects
    private List<GameObject> nearbyObjects;
    private Transform camPos;
    private GameObject highlightedObject;

    private LayerMask layerMask;

    
    // Start is called before the first frame update
    void Start()
    {
        nearbyObjects = new List<GameObject>();

        camPos = Camera.main.transform;
        inventory = this.GetComponent<InventoryController>();
        //get the player layer to make sure they don't block themselves from seeing interactables
        layerMask = ~LayerMask.GetMask("Player"); 
    }

    // Update is called once per frame
    void Update()
    {
        /* foreach (GameObject i in nearbyObjects)
        {
            print(i.name);
        } */


        if (nearbyObjects.Count > 0)
        {
            FindObjectToHighlight();
        } else if (highlightedObject != null)
        {
            UnHighlightObject();
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        
        if (nearbyObjects.Count > 0)
        {
            //print("Something is Nearby");
            if(context.performed)
            {
                //print("Input Recieved");
                if (highlightedObject.GetComponent<ItemInterface>() != null && inventory.CanPickupItems())
                {
                    //print("Pickup Object: " + highlightedObject.name);
                    nearbyObjects.Remove(highlightedObject);
                    inventory.ContextInteract(highlightedObject.GetComponent<ItemInterface>());
                    
                    nearbyObjects.TrimExcess();
                }
                if (highlightedObject.GetComponent<ButtonScript>() != null)
                {
                    if (highlightedObject.GetComponent<ButtonScript>().isLocked)
                    {
                        if(inventory.CheckHasItem("Keycard"))
                        {
                            highlightedObject.GetComponent<ButtonScript>().Unlock();
                        }
                    }else
                    {
                        highlightedObject.GetComponent<ButtonScript>().PressButton();
                    }
                }
            }
        }
    }
    
    private void FindObjectToHighlight()
    {
        GameObject tempObject;

        //first check if the player is looking directly at one of the objects
        tempObject = ObjectInFocus();
        if (tempObject == null || !nearbyObjects.Contains(tempObject))
        {
            //if they're not, then go with the nearest object
            tempObject = nearbyObjects[GetNearestObject()];
        }

        HighlightObject(tempObject);
    }
    public TextMeshProUGUI highlightText;
    private void HighlightObject(GameObject newObject)
    {
        if (newObject != highlightedObject)
        {
            //highlightText.text = newObject.name;
            //stop highlighting previous object
            UnHighlightObject();
            
            //set new object to highlighted object
            highlightedObject = newObject;
            //highlight new object

            if(highlightedObject.GetComponent<Outline>() == null)
            {   //error check to handle if the object doesn't have an outline, just give it one real quick
                //if people set up their objects right this shouldn't be an issue
                highlightedObject.AddComponent<Outline>();
                highlightedObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineAndSilhouette;
                highlightedObject.GetComponent<Outline>().OutlineColor = Color.white;
                highlightedObject.GetComponent<Outline>().OutlineWidth = 5f;
                highlightedObject.GetComponent<Outline>().enabled = false;
            }
            highlightedObject.GetComponent<Outline>().enabled = true;
        }
    }

    private void UnHighlightObject()
    {
        //highlightText.text = "Nothing Nearby";
        if (highlightedObject != null && highlightedObject.GetComponent<Outline>() != null)
        {
            highlightedObject.GetComponent<Outline>().enabled = false;
        }
        highlightedObject = null;
    }

    #region Object Selection
    private GameObject ObjectInFocus()
    {
        GameObject objectInFocus = null;
        RaycastHit hit;

        float distance = Vector3.Distance(camPos.position, transform.position);
        distance += 2f; //check 2 meters past the player
        
        Debug.DrawRay(camPos.position, camPos.forward * distance, Color.red, Time.deltaTime);
        Physics.Raycast(camPos.position, camPos.forward * distance, out hit, distance, layerMask, QueryTriggerInteraction.Ignore);
        if (hit.collider != null)
        {
            objectInFocus = hit.collider.gameObject;
        }


        return objectInFocus;
    }

    private int GetNearestObject()
    {
        float minDistance = 100;
        int minIndex = 0;
        for (int i = 0; i < nearbyObjects.Count; i++)
        {
            float curDistance = Vector3.Distance(nearbyObjects[i].transform.position, transform.position);
            
            if (curDistance < minDistance)
            {
                minDistance = curDistance;
                minIndex = i;
            }
        }
        return minIndex;
    }
    #endregion

    #region Nearby Object Tracker
    private void OnTriggerEnter(Collider other)
    {
        //if we already know of the item, don't add it again
        if (nearbyObjects.Contains(other.gameObject))
            return;

        switch (other.tag)
        {
            case "Item":
            
                nearbyObjects.Add(other.gameObject);
            break;

            case "Button":
            
                nearbyObjects.Add(other.gameObject);
            break;

            default:
            //do literally nothing
            break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (nearbyObjects.Contains(other.gameObject))
        {
            nearbyObjects.Remove(other.gameObject);
            nearbyObjects.TrimExcess();
        }
    }
    #endregion
    
}
