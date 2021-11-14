using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitItemScript : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    [SerializeField] public GameObject myPrefab;//reference to this object's prefab
    [SerializeField] private float noiseRadius = 5;
    [SerializeField] private float baitRadius = 10;
    [SerializeField] private bool isActive;
    private SuspicionManager alertManager;
    private float baitInterval = 2;
    private float timer;


    public GameObject myself
    {
        get {return myPrefab;}
        set {myPrefab = value;}
    }
    public string itemName
    {
        get {return objectName;}
        set {objectName = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        durability = itemDurability;
        itemName = objectName;
        
        if (noiseRadius != 0)
            thrownNoiseRadius = noiseRadius;
        
        alertManager = (SuspicionManager)FindObjectOfType(typeof(SuspicionManager));
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive && timer < 0)
        {
            //only lure once every 5 seconds
            LurePigs();
            timer = baitInterval;
        }else if (isActive)
        {
            timer -= Time.deltaTime;
        }
    }

    private void LurePigs()
    {
        alertManager.AlertGuards(transform.position, transform.position, baitRadius);
    }

    public void UseItem()
    {

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, baitRadius);
    }
}
