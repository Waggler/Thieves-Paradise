using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    [SerializeField] private float WalkingSpeed;
    [SerializeField] private float RunningSpeed;
    [SerializeField] private float CrouchSpeed;
    private float CurrentSpeed;
    Vector3 Direction;

    [Header("Physics")]
    [SerializeField] private float Gravity;
    [SerializeField] private float JumpForce;
    [SerializeField] private CharacterController Controller;
    Vector3 VerticalVelocity = Vector3.zero;

    #endregion

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
        Controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        #region Gravity
        if(Controller.isGrounded)
        {
            print("ground");
        }

        //VerticalVelocity.y -= Gravity * Time.deltaTime;
        //Controller.Move(VerticalVelocity * Time.deltaTime);
        #endregion

        #region Movement
        Controller.Move(Direction * CurrentSpeed * Time.deltaTime);
        #endregion
    }

    #region Functions
 
    #region Move
    public void Movement(Vector3 Move)
    {
        Direction = new Vector3(Move.x, 0f, Move.z);
    }
    #endregion

    #region Jump
    public void Jump()
    {
        
    }
    #endregion

    #region Sprint
    public void Sprint(bool Sprinting)
    {
        if(Sprinting == true)
        {
            CurrentSpeed = RunningSpeed;
        }
        else if(Sprinting == false)
        {
            CurrentSpeed = WalkingSpeed;
        }
    }
    #endregion

    #region Crouch
    public void Crouch(bool Crouching)
    {
        if(Crouching == true)
        {
            CurrentSpeed = CrouchSpeed;
        }
        else if(Crouching == false)
        {
            CurrentSpeed = WalkingSpeed;
        }
    }
    #endregion

    #endregion
}
