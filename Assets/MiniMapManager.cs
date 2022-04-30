using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    public GameObject floor2Map;
    public GameObject floor1Map;
    public GameObject player;
    public float floor2Threshhold;
    public bool isFloor1;


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        FloorCheck();
    }
    public void FloorCheck()
    {
        if (player.transform.position.y < floor2Threshhold)
        {
            ChangeFloor(false);
        }
        else
        {
            ChangeFloor(true);
        }
    }


    public void ChangeFloor(bool isFloor1)
    {
        //Displays floor 1 map
        if (isFloor1 == true)
        {
            floor2Map.SetActive(true);
            floor1Map.SetActive(false);
        }
        //Displays floor 2 map
        else
        {
            floor1Map.SetActive(true);
            floor2Map.SetActive(false);
        }
    }


}
