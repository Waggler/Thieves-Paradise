using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonPop : MonoBehaviour
{
    public GameObject balloon;
    [SerializeField]
    private AudioSource pop;

    // Start is called before the first frame update
    void Start()
    {
        balloon.SetActive(true);
        pop = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter()
    {
        pop.Play();
        Debug.Log("Pop sound played!");
        Destroy(gameObject);
        //balloon.SetActive(false);
    }

    public void Pop()
    {
        
    }
}
