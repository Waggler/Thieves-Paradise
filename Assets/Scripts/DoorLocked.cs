using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLocked : MonoBehaviour
{
    public bool isLocked = true;
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

    void OnTriggerEnter()
    {
        if(isLocked == true)
        {
            message.SetActive(true);
        }
    }
}
