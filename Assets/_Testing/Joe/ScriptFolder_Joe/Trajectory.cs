using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Trajectory : MonoBehaviour
{
    [Header("Calulation Variables")]
    [SerializeField] private Transform InitialPosition;
    private InventoryController inventoryController;
    private float Velocity;
    private float AngleofLaunch = 45.0f;

    [Header("Visualization")]
    [SerializeField] private LineRenderer Line;
    [SerializeField] private int LineLength;
    [SerializeField] private Material Mat1, Mat2, Mat3;

    void Start()
    {
        inventoryController = GetComponent<InventoryController>();
    }

    void Update()
    {
        Velocity = inventoryController.throwForce;

    }

    void Visulization()
    {
        for(int i = 0; i < LineLength; i++)
        {

        }
    }

    void Calculations()
    {

    }
}
