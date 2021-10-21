using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    [SerializeField] public GameObject myself;//reference to this object's prefab

    // Start is called before the first frame update
    void Start()
    {
        durability = itemDurability;
        itemName = objectName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem()
    {
        print("Used Active Item");
    }
}
