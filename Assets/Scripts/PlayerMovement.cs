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
    [SerializeField] private float Acceleration;
    private float CurrentSpeed;
    private Vector3 Direction;
    private bool IsSprinting;

    [Header("Crouching")]
    [SerializeField] private float StandardHeight;
    [SerializeField] private float CrouchingHeight;
    private bool IsCovered = false;
    private bool IsCrouching = false;
    private bool WasCrouching = false;

    [Header("Physics")]
    [SerializeField] private float Gravity;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float HeightFromGround;
    [SerializeField] private float CrouchingHeightFromGround;
    [SerializeField] private CapsuleCollider Collider;
    [SerializeField] private CharacterController Controller;
    private float GroundHeight;
    private Vector3 VerticalVelocity = Vector3.zero;
    private bool IsGrounded = true;

    #endregion

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
        Controller = GetComponent<CharacterController>();
        Collider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        GroundCheck();

        if(IsCrouching == false && WasCrouching == true)
        {
            CoveredCheck();
        }


        print(CurrentSpeed);
        
        #region Gravity
        if(IsGrounded)
        {
            VerticalVelocity.y = 0;
        }

        VerticalVelocity.y -= Gravity * Time.deltaTime;
        Controller.Move(VerticalVelocity * Time.deltaTime);
        #endregion

        #region Movement
        Controller.Move(Direction * CurrentSpeed * Time.deltaTime);
        #endregion
    }

    #region Functions
 
    #region Move
    //----------MOVEMENT----------//
    public void Movement(Vector3 Move)
    {
        Direction = new Vector3(Move.x, 0f, Move.z);
    }
    #endregion

    #region Jump
    //----------JUMP----------//
    public void Jump()
    {
        if(IsGrounded)
        {
            VerticalVelocity.y = Mathf.Sqrt(-2f * JumpHeight * -Gravity);
            Controller.Move(VerticalVelocity * Time.deltaTime);
        }
    }
    #endregion

    #region Sprint
    //----------SPRINT----------//
    public void Sprint(bool Sprinting)
    {
        if(Sprinting == true)
        {
            CurrentSpeed = Mathf.Lerp(CurrentSpeed, RunningSpeed, Acceleration);

            Acceleration += 1f * Time.deltaTime;

            if(CurrentSpeed == RunningSpeed)
            {
                CurrentSpeed = RunningSpeed;
                print("Running");
            }

            IsSprinting = true;
        }
        else if(Sprinting == false)
        {
            CurrentSpeed = WalkingSpeed;
            IsSprinting = false;
            Acceleration = 0.1f;
        }
    }
    #endregion

    #region Crouch
    //----------CROUCH----------//
    public void Crouch(bool Crouching)
    {
        if(Crouching == true && IsGrounded == true)
        {
            CurrentSpeed = CrouchSpeed;
            Collider.height = CrouchingHeight;
            Controller.height = CrouchingHeight;
            Controller.center = new Vector3 (0f, -0.5f, 0f);
            Collider.center = new Vector3 (0f, -0.5f, 0f);
            GroundHeight = CrouchingHeightFromGround;
            IsCrouching = true;
            WasCrouching = true;
        }
        else if(Crouching == false && IsCovered == false)
        {
            CurrentSpeed = WalkingSpeed;
            Collider.height = StandardHeight;
            Controller.height = StandardHeight;
            Controller.center = new Vector3 (0f, 0f, 0f);
            Collider.center = new Vector3 (0f, 0f, 0f);
            GroundHeight = HeightFromGround;
            IsCrouching = false;
            WasCrouching = false;
        }
    }
    #endregion

    #region Ground
    //---GROUNDCHECK---//
    void GroundCheck()
    {
        if(Physics.Raycast(transform.position, Vector3.down, HeightFromGround + 0.1f))
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
    #endregion

    #region Covered
    //---COVEREDCHECK---//
    void CoveredCheck()
    {

        if(Physics.Raycast(transform.position, Vector3.up, HeightFromGround + 0.1f))
        {
            IsCovered = true;
        }
        else
        {
            IsCovered = false;
        }
    }

    #endregion

    #endregion
}
