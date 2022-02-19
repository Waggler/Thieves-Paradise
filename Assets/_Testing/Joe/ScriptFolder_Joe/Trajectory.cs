using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Trajectory : MonoBehaviour
{
    private InventoryController inventoryController;
    private float Velocity;
    private float AngleofLaunch = 45.0f;
    [SerializeField] private Transform InitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        inventoryController = GetComponent<InventoryController>();
    }
}
