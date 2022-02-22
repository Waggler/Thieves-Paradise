using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPastFallOver : MonoBehaviour
{
    public PlayerMovement pm;
    public InputManager im;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter()
    {
        if(pm.IsCrouching == true || im.isCrouching == true)
        {
            Debug.Log("Walked Past!");
        }

        if(pm.IsCrouching == false || im.isCrouching == false)
        {
            Debug.Log("Sound Made!");
        }
    }
}
