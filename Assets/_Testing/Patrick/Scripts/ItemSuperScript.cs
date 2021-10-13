using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSuperScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int durability
    {
        get;
        set;
    }

    public void Pickup()
    {
        print("Pickup Item");
    }
    public void Throw()
    {
        print("Throw Item");
    }

    public void Use()
    {
        if (durability > 0)
        {
            print("Used Item");
            durability--; //decriment durability
        } else
        {
            print("Item Broke");
        }
    }


}
