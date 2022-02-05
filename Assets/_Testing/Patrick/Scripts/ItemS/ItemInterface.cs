using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItemInterface
{
    //required functions
    GameObject myself
    {
        get;
        set;
    }
    string itemName
    {
        get;
        set;
    }
    void UseItem();

    void UseItemEnd();

}
