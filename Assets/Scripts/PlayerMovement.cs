using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Speed")]
    [SerializeField] private float WalkingSpeed;
    [SerializeField] private float RunningSpeed;
    [SerializeField] private float CrouchSpeed;
    private float CurrentSpeed;

    #endregion

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
    }
 
    public void CallJumpFunctionHere()
    {
        print("Jump");
    }

    public void Movement(Vector3 Move)
    {
        print(Move);
    }

    public void Sprint(bool Sprinting)
    {
        print($"Is sprinting: {Sprinting}.");
    }

    public void Crouch(bool Crouching)
    {
        print($"Is crouching: {Crouching}.");
    }
}
