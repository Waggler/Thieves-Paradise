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

    private void Start()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    private void Update()
    {
        Velocity = inventoryController.throwForce;

    }
}
