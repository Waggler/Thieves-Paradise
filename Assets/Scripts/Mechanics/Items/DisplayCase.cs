using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayCase : MonoBehaviour
{
    public GameObject caseDoor;
    public Key key;
    public bool isLocked = true;
    public bool isOpened = false;
    public PlayerMovement pm;
    public InputManager im;
    public bool inArea = false;
    public float doorOpen;
    public float Timer = 0f;
    public bool reachedTimer = false;
    float minAngle = 0.0f;
    float maxAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(key.gotKey == true)
        {
            isLocked = false;
        }

        if(isOpened == true)
        {
            maxAngle = doorOpen;
        }
    }
}
