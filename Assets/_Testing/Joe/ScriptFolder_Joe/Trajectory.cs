using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Trajectory : MonoBehaviour
{
    [SerializeField] private float Velocity;
    [SerializeField] private float AngleofLaunch;
    [SerializeField] private float InitialHeight;
    private float y;
    private float x;
    private float Vy;
    private float Vx;

    // Start is called before the first frame update
    void Start()
    {

    }
}
