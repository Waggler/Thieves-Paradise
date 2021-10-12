using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInventoryItem : MonoBehaviour
{
    string Name { get; }

    Sprite Image { get; }

}

public class InventoryEventArgs : EventArgs 
{
    public InventoryEventArgs(IInventoryItem item)
    {
        Item = item;
    }

    public IInventoryItem Item;
}