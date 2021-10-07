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
    private bool IsCrouching = false;


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

        if(IsCrouching == false && IsSprinting == false)
        {
            CoveredCheck();
        }

        if(IsSprinting == true)
        {
            Sprinting();
        }

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
            IsSprinting = true;
        }
        else if(Sprinting == false)
        {
            CurrentSpeed = WalkingSpeed;
            IsSprinting = false;
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
        }
        else if(Crouching == false)
        {
            IsCrouching = false;
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
        //---USE-SOMETHING-THAT-ISN'T-RAYCAST---//
        //RaycastHit hit;
        //Vector3 p1 = transform.position + Controller.center + Vector3.up * -Controller.height * 0.5f;
        //Vector3 p2 = p1 + Vector3.up * Controller.height;

        if(Physics.Raycast(transform.position, Vector3.up, HeightFromGround + 0.1f))
        {
            return;
        }
        else
        {
            CurrentSpeed = WalkingSpeed;
            Collider.height = StandardHeight;
            Controller.height = StandardHeight;
            Controller.center = new Vector3(0f, 0f, 0f);
            Collider.center = new Vector3(0f, 0f, 0f);
            GroundHeight = HeightFromGround;
        }
    }

    #endregion

    #region Sprinting
    //---SPRINTING---//
    void Sprinting()
    {
        if(CurrentSpeed < RunningSpeed)
        {
            CurrentSpeed += Acceleration * Time.deltaTime;
        }
        else if(CurrentSpeed >= RunningSpeed)
        {
            CurrentSpeed = RunningSpeed;
        }
    }
    #endregion

    #endregion
}
