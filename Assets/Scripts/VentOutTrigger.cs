using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentOutTrigger : MonoBehaviour
{
    public bool outVent = true;
    public VentInTrigger ventIn;
    
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
        outVent = true;
        ventIn.inVent = false;
    }
}
