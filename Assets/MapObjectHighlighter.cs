using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectHighlighter : MonoBehaviour
{
    [Tooltip("Auto generated")]
    [SerializeField] private ItemTracker itemTracker;

    [SerializeField] private GameObject[] heistItemObjects;

    private float floor2Threshhold = 11.5f;

    void Start()
    {
        Debug.Log("Initializing normally");

        Init();
    }

    void Update()
    {
    }

    private void Init()
    {
        itemTracker = GetComponentInChildren<ItemTracker>();

        heistItemObjects = itemTracker.heistItemObjects;


        foreach (GameObject gameObject in heistItemObjects)
        {
            Debug.Log(gameObject);

            if (gameObject.transform.position.y < floor2Threshhold)
            {
            }
        }
    }

}