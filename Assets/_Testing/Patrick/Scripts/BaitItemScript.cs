using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitItemScript : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    [SerializeField] public GameObject myPrefab;//reference to this object's prefab
    [SerializeField] private float noiseRadius = 5;
    [SerializeField] private bool isActive;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            LurePigs();
        }
    }

    private void LurePigs()
    {
        
    }

    public void UseItem()
    {

    }
}
