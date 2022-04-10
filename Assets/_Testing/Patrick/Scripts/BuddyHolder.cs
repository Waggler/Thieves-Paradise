using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuddyHolder : MonoBehaviour
{
    [SerializeField] private GameObject buddy;
    [SerializeField] private GameObject holdingspot;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Quaternion rotationOffset;
    public bool displayBuddy;

    void Start()
    {
        if(displayBuddy)
        {
            DisplayBuddy();
        }
    }

    public void DisplayBuddy()
    {
        GameObject bud = Instantiate(buddy, holdingspot.transform.position, holdingspot.transform.rotation);
        bud.transform.parent = holdingspot.transform;
        bud.transform.localPosition = positionOffset;
        bud.transform.localRotation = rotationOffset;
    }
}
