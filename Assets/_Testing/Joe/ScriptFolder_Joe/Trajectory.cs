using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Trajectory : MonoBehaviour
{
    [Header("Calulation Variables")]
    [SerializeField] private Transform InitialPosition;
    private InventoryController inventoryController;
    private Vector3 TrueVelocity;
    private float AngleofLaunch = 45.0f;

    [Header("Visualization")]
    [SerializeField] private LineRenderer Line;
    [SerializeField] private int LineLength;
    [SerializeField] private Material Mat;
    [SerializeField] private Color lineColor;
    private int CheckingInt;
    private InputManager inputManager;
    private bool TrajectoryStart = true;

    void Start()
    {
        Line.positionCount = LineLength;
        inventoryController = GetComponent<InventoryController>();
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        TrueVelocity = inventoryController.throwVector / 48; 
        TrajectoryStart = inputManager.IsThrowing;
        PredictingLaunch();
    }
    void PredictingLaunch()
    {
        if(TrajectoryStart)
        {
            Line.material = Mat;
            VisulizeTrajectory(TrueVelocity);
            Line.enabled = true;
            Line.startColor = lineColor;
            Line.endColor = lineColor;
            Line.startWidth = 0.01f;
            Line.endWidth = 0.2f;
        }
        else
        {
            Line.enabled = false;
        }
    }
    void VisulizeTrajectory(Vector3 Thrown)
    {
        for(int i = 0; i < LineLength; i++)
        {
            Vector3 Position = CalculateTrajectory(Thrown, i / (float)(LineLength) / 0.5f);
            Line.SetPosition(i, Position);
        }
    }

    Vector3 CalculateTrajectory(Vector3 vo, float time)
    {
        Vector3 VelocityXZ = vo;
        VelocityXZ.y = 0f;

        Vector3 Result = (InitialPosition.position + transform.forward) + vo * time;
        float SpawnY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + InitialPosition.position.y;

        Result.y = SpawnY;

        return Result;
    }
}
