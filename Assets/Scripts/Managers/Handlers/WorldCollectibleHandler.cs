using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCollectibleHandler : MonoBehaviour
{
    [SerializeField] private WorldMenuManager manager;
    public LocationCollectibleData data;

    public void OpenLocation()
    {
        manager.InitLocation(data);
    }

}
