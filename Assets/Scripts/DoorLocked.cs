using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    //message1 is the message that talks about the locked door
    public GameObject door;
    public bool isLocked = true;
    public GameObject message1;
    public GameObject message2;
    public Key key;

    // Start is called before the first frame update
    void Start()
    {
        message1.SetActive(false);
        message2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(key.gotKey == true)
        {
            isLocked = false;
        }
    }

    void OnTriggerEnter()
    {
        if(isLocked == true)
        {
            message1.SetActive(true);
        }
        if(isLocked == false)
        {
            message2.SetActive(true);
        }
    }

    void OnTriggerExit()
    {
        if(isLocked == true)
        {
            message1.SetActive(false);
        }
        if(isLocked == false)
        {
            message2.SetActive(false);
        }
    }
}
