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
    private Vector3 TrueVelocity = new Vector3 (0,500,500);
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
        //Velocity = inventoryController.throwForce;
        TrajectoryStart = inputManager.IsZoomed;
        PredictingLaunch();
    }
    void PredictingLaunch()
    {
        if(TrajectoryStart)
        {
            Line.material = Mat1;
            VisulizeTrajectory(TrueVelocity);
        }
        else
        {
            Line.material = Mat2;
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

        Vector3 Result = InitialPosition.position + vo * time;
        float SpawnY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + InitialPosition.position.y;

        Result.y = SpawnY;

        return Result;
    }
}
