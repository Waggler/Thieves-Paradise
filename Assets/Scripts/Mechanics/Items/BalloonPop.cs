using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
    public GameObject balloon;

    // Start is called before the first frame update
    void Start()
    {
        balloon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter()
    {
        Destroy(gameObject);
        //balloon.SetActive(false);
    }
}
