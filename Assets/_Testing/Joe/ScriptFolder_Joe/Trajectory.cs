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
    [SerializeField] private Material Mat1, Mat2;
    private InputManager inputManager;
    private bool TrajectoryStart;

    void Start()
    {
        Line.positionCount = LineLength;
        inventoryController = GetComponent<InventoryController>();
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        Velocity = inventoryController.throwForce;
        TrajectoryStart = inputManager.IsZoomed;
        PredictingLaunch();
    }
    void PredictingLaunch()
    {
        if(TrajectoryStart)
        {
            Line.material = Mat1;
        }
        else
        {
            Line.material = Mat2;
        }
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
