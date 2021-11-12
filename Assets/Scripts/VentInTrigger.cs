using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentInTrigger : MonoBehaviour
{
    public bool inVent = false;
    public VentOutTrigger ventOut;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnTriggerEnter()
    {
        inVent = true;
        ventOut.outVent = false;
    }
}
