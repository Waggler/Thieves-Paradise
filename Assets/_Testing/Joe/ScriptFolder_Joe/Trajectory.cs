using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Trajectory : MonoBehaviour
{
    //Some how this isn't even firing. Try to see if !Trajectory start works.

    [Header("Calulation Variables")]
    [SerializeField] private Transform InitialPosition;
    private InventoryController inventoryController;
    private Vector3 TrueVelocity;
    private float AngleofLaunch = 45.0f;

    [Header("Visualization")]
    [SerializeField] private LineRenderer Line;
    [Range(3,30)]
    [SerializeField] private int LineLength;
    [SerializeField] private Material Mat;
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
        TrueVelocity = inventoryController.throwVector / 100; //find the right scaling variable here
        TrajectoryStart = inputManager.IsZoomed;
        PredictingLaunch();
    }
    void PredictingLaunch()
    {
        if(TrajectoryStart)
        {
            Debug.Log("I am working.");
            Line.material = Mat;
            VisulizeTrajectory(TrueVelocity);
            Line.enabled = true;
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
            Debug.Log($"I = {i}");
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

        Debug.Log($"The Result is {Result}");

        return Result;
    }
}
