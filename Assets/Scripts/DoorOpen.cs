using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool inArea = false;
    public GameObject door;
    public GameObject message;

    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        inArea = true;
        message.SetActive(true);
        door.transform.eulerAngles = new Vector3(0f, -76.942f, 0f);
        door.transform.position = new Vector3(8.57f, 2.1f, 6.456132f);
        message.SetActive(false);
    }

    private void OnTriggerExit(Collider collider)
    {
        if(inArea == true)
        {
            inArea = false;
            door.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            door.transform.position = new Vector3(9.334243f, 2.1f, 6.456132f);
        }
        message.SetActive(false);
    }
}
