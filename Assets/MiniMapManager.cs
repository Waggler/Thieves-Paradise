using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public GameObject floor1Map;
    public GameObject floor2Map;
    public bool isFloor1;
    public PlayerMovement player;


    public void ChangeFloor(bool isFloor1)
    {
        if (isFloor1 == true)
        {
            floor1Map.SetActive(true);
            floor2Map.SetActive(false);
        }
        else
        {
            floor2Map.SetActive(true);
            floor1Map.SetActive(false);
        }
    }
}
