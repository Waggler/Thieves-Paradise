using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapHandler : MonoBehaviour
{
    private GameObject[] circles;
    private int[] circleFloors;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject playerMarkerPrefab;
    private GameObject playerMarker;
    private bool isFirstFloor;
    private GameObject playerRef;
    public float SecondFloorThreshold = 10;

    [SerializeField] private Sprite MapFloorOne;
    [SerializeField] private Sprite MapFloorTwo;
    private SpriteRenderer MapObject;
    private GameObject MapCamera;
    [SerializeField] private Button F1;
    [SerializeField] private Button F2;
    [SerializeField] private bool debug;

    void Start()
    {
        InitCirclePos();

        //grab player ref
        //PlayerMovement tempHolder = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        playerRef = GameObject.FindGameObjectWithTag("PlayerVisionTarget");

        //line up minimap
        MapObject = GameObject.FindGameObjectWithTag("MinimapObject").GetComponent<SpriteRenderer>();
        MapObject.gameObject.transform.position = new Vector3(-4f, 64.1045f, -97.3f);
        MapObject.gameObject.transform.localScale = new Vector3(10.85f, 10f, 10.38895f);
        if(debug) Debug.Log(MapObject.gameObject.transform.position);

        MapCamera = GameObject.FindGameObjectWithTag("MinimapCamera");
        MapCamera.transform.position = new Vector3(-10, 150, -85.9f);

        //set up player marker
        playerMarker = Instantiate(playerMarkerPrefab, playerRef.transform.position, Quaternion.identity);
        playerMarker.transform.position = new Vector3(playerRef.transform.position.x, 80f, playerRef.transform.position.z);
        playerMarker.transform.eulerAngles = new Vector3(90,0,0);

        FloorSwitcher();//initialize map
    }
    private void InitCirclePos()
    {
        GameObject[] heistRef = GetComponent<ItemTracker>().heistItemObjects;
        circles = new GameObject[heistRef.Length];
        circleFloors = new int[heistRef.Length];

        for (int i = 0; i < circles.Length; i++)
        {
            //create circle
            circles[i] = Instantiate(circlePrefab, heistRef[i].transform.position, Quaternion.identity);
            circles[i].transform.eulerAngles = new Vector3(90, 0 ,0);
            //move circle up to minimap
            circles[i].transform.position = new Vector3(circles[i].transform.position.x, 75f, circles[i].transform.position.z);
            //set layer to Minimap Layer
            circles[i].layer = 9;

            //set what floors the circles are on
            if (heistRef[i].transform.position.y > SecondFloorThreshold)
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
        if (playerRef.transform.position.y > SecondFloorThreshold)
        {
            if (isFirstFloor)
            {
                FloorSwitcher();
            }
        }else //player is on first floor
        {
            if(!isFirstFloor)
            {
                FloorSwitcher();
            }
        }

        playerMarker.transform.position = new Vector3(playerRef.transform.position.x, 80f, playerRef.transform.position.z);
        
        if(debug) Debug.Log("Displaying First Floor: " + isFirstFloor);
    }

    public void FloorSwitcher()
    {
        if (isFirstFloor)
        {
            //display 2nd floor
            MapObject.sprite = MapFloorTwo;
        
            isFirstFloor = false;

            F1.interactable = true;
            F2.interactable = false;

            if (playerRef.transform.position.y >= SecondFloorThreshold)
            {
                playerMarker.SetActive(true);
            }else
            {
                playerMarker.SetActive(false);
            }
        }else //is displaying second floor
        {
            //display 1st floor
            MapObject.sprite = MapFloorOne;

            isFirstFloor = true;

            F1.interactable = false;
            F2.interactable = true;

            if (playerRef.transform.position.y <= SecondFloorThreshold)
            {
                playerMarker.SetActive(true);
            }else
            {
                playerMarker.SetActive(false);
            }
        }

        //show/hide appropriate circles
        for (int i = 0; i < circles.Length; i++)
        {
            if (isFirstFloor)
            {
                if (circleFloors[i] == 1)
                {
                    circles[i].SetActive(true);
                }else
                {
                    circles[i].SetActive(false);
                }
            }else //is second floor
            {
                if(circleFloors[i] == 2)
                {
                    circles[i].SetActive(true);
                }else
                {
                    circles[i].SetActive(false);
                }
            }
        }
    }
}
