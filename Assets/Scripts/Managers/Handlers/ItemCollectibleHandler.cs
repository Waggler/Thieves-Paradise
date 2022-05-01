using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCollectibleHandler: MonoBehaviour
{
    [SerializeField] private ItemsMenuManager manager;
    public ItemCollectibleData data;

    public void OpenItem()
    {
        manager.InitItem(data);
    }

}
