using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    // Start is called before the first frame update
    void Start()
    {
        durability = itemDurability;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem()
    {

    }
}
