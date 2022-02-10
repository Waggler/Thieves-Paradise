using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
    public GameObject balloon;
    [SerializeField]
    private AudioClip pop;

    // Start is called before the first frame update
    void Start()
    {
        balloon.SetActive(true);
        //AudioSource pop = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter()
    {
        Destroy(gameObject);
        //balloon.SetActive(false);
        //pop.Play();
    }
}
