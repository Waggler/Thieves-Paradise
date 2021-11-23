using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool gotKey = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gotKey == true)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter()
    {
        gotKey = true;
    }
}
