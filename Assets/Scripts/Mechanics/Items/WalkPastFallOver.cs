using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPastFallOver : MonoBehaviour
{
    public PlayerMovement pm;

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
        if(pm.IsCrouching == true)
        {
            Debug.Log("Walked Past!");
        }

        if(pm.IsCrouching == false)
        {
            Debug.Log("Sound Made!");
        }
    }
}
