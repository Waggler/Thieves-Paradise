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
            if(context.performed)
            {
                if (highlightedObject.GetComponent<BasicItemScript>() != null)
                {
                    print("Pickup Object");
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
            highlightText.text = newObject.name;
            //stop highlighting previous object
            //highlight new object
            //set new object to highlighted object
            highlightedObject = newObject;
        }
    }

    private void UnHighlightObject()
    {
        highlightText.text = "Nothing Nearby";
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
        Physics.Raycast(camPos.position, camPos.forward * distance, out hit, distance, layerMask);
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
        switch (other.tag)
        {
            case "Item":
            nearbyObjects.Add(other.gameObject);
            break;

            default:
            //do nothing
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
