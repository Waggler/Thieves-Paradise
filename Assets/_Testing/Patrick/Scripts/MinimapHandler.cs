using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapHandler : MonoBehaviour
{
    private GameObject[] circles;
    private int[] circleFloors;
    [SerializeField] private GameObject circlePrefab;
    private bool isFirstFloor;


    void Start()
    {
        InitCirclePos();
    }
    private void InitCirclePos()
    {
        GameObject[] heistRef = GetComponent<ItemTracker>().heistItemObjects;
        circles = new GameObject[heistRef.Length];
        circleFloors = new int[heistRef.Length];

        for (int i = 0; i < circles.Length; i++)
        {
            //create circle
            circles[i] = Instantiate(circlePrefab, heistRef[i].transform.position, heistRef[i].transform.rotation);
            //move circle up to minimap
            circles[i].transform.position = new Vector3(circles[i].transform.position.x, 75f, circles[i].transform.position.z);
            //set layer to Minimap Layer
            circles[i].layer = 9;

            //set what floors the circles are on
            if (heistRef[i].transform.position.y > 10)
            {
                circleFloors[i] = 2;
            }else
            {
                circleFloors[i] = 1;
            }
        }
    }

    public void DisplayMapOnPause()
    {
        //if the player is above a certain y value, display a different floor
    }

    public void FloorSwitcher()
    {

    }
}
