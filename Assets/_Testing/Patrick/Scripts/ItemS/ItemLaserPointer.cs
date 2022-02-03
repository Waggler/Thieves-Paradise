using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLaserPointer : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    private GameObject myPrefab;//reference to this object's prefab
    [SerializeField] private float noiseRadius = 15;

    [SerializeField] private GameObject deployableObject;

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

        myself = this.gameObject;
        myself.name = itemName;
    }

    // Update is called once per frame
    public void UseItem()
    {
        print("Used Laser Pointer Item");
        
    }
}
