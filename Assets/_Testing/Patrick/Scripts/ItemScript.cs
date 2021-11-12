using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    [SerializeField] public GameObject myself;//reference to this object's prefab
    [SerializeField] private float noiseRadius = 15;

    // Start is called before the first frame update
    void Start()
    {
        durability = itemDurability;
        itemName = objectName;
        
        if (noiseRadius != 0)
            thrownNoiseRadius = noiseRadius;
        
    }

    // Update is called once per frame
    public void UseItem()
    {
        print("Used Active Item");
    }    
}
