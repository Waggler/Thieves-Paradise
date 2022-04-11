using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChanger : MonoBehaviour
{
    public bool isThisOn1stFloor;


    public MiniMapManager miniMapManager;
    private void Awake()
    {
        miniMapManager = FindObjectOfType<MiniMapManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            if(isThisOn1stFloor == true)
            {
                miniMapManager.ChangeFloor(true);
            }
            else
            {
                miniMapManager.ChangeFloor(false);
            }
        }
    }



}
