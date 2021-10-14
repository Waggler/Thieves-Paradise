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
    private bool IsSprinting = false;
    private bool UnSprinting = true;

    [Header("Crouching")]
    [SerializeField] private float StandardHeight;
    [SerializeField] private float CrouchingHeight;
    private bool IsCrouching = false;
    private bool UnCrouched = true;


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

    [Header("Rolling")]
    [SerializeField] private float RollingSpeed;
    [SerializeField] private float RollingTime;
    private float CurrentRollTime;
    private Vector3 RollDirection;
    private bool IsRolling;

    [Header("Sliding")]
    [SerializeField] private float Deceleration;
    private bool IsSliding;

    #endregion

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
        CurrentRollTime = RollingTime;
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

        if(IsSprinting == true && IsCrouching == false)
        {
            Sprinting();
        }

        #region Gravity
        //FIX: Crouching Gravity is a bit wonky, talk to Patrick on Tuesday about this.
        if(IsGrounded)
        {
            VerticalVelocity.y = 0;
        }

        if(IsCrouching == false)
        {
            VerticalVelocity.y -= Gravity * Time.deltaTime;
            Controller.Move(VerticalVelocity * Time.deltaTime);
        }
        else if(IsCrouching == true)
        {
            VerticalVelocity.y -= Gravity * Time.deltaTime * 35;
            Controller.Move(VerticalVelocity * Time.deltaTime);
        }
        #endregion

        #region Movement
        if(IsRolling == false || IsSliding == false)
        {
            Controller.Move(Direction * CurrentSpeed * Time.deltaTime);
        }

        #endregion

        #region Roll Action
        Rolling();

        #endregion
    
        #region Slide Action
        if(IsSprinting == true && IsCrouching == true)
        {
            IsSliding = true;
            Sliding();
        }

        #endregion

    }

    #region Functions
 
    #region Move
    //----------MOVEMENT----------//
    public void Movement(Vector3 Move)
    {
        Direction = new Vector3(Move.x, 0f, Move.z);
        if(Direction != Vector3.zero && IsRolling == false && IsSliding == false) 
        {
            RollDirection = Direction;
        }
    }
    #endregion

    #region Jump
    //----------JUMP----------//
    public void Jump()
    {
        if(IsGrounded && IsCrouching == false)
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
        if(Sprinting == true  && IsCrouching == false)
        {
            IsSprinting = true;
            if(UnSprinting == false)
            {
                UnSprinting = true;
            }
            else if(UnSprinting == true)
            {
                UnSprinting = false;
            }
        }
        else if(Sprinting == false && IsCrouching == false && UnSprinting == true)
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
            IsCrouching = true;
            if(UnCrouched == true && IsSprinting == false)
            {
                CrouchDown();
                UnCrouched = false;
            }
            else if(UnCrouched == false)
            {
                UnCrouched = true;
            }
        }
        else if(Crouching == false && UnCrouched == true)
        {
            IsCrouching = false;
        }
    }
    #endregion

    #region Roll
    //----------ROLL----------//
    public void Roll(bool Rolling)
    {
        if(Rolling == true && IsCrouching == true)
        {
            IsRolling = true;
        }
    }

    #endregion

    #region Sliding
    //---SLIDING---//
    void Sliding()
    {
        if(CurrentSpeed > CrouchSpeed)
        {
            IsCrouching = true;
            Controller.Move(RollDirection * CurrentSpeed * Time.deltaTime);
            Collider.height = CrouchingHeight;
            Controller.height = CrouchingHeight;
            Controller.center = new Vector3 (0f, -0.5f, 0f);
            Collider.center = new Vector3 (0f, -0.5f, 0f);
            GroundHeight = CrouchingHeightFromGround;
            CurrentSpeed -= Deceleration * Time.deltaTime; 
        }
        else if(CurrentSpeed <= CrouchSpeed)
        {
            CrouchDown();
            UnCrouched = false;
            IsSprinting = false;
            IsSliding = false;
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
        if(Physics.Raycast(transform.position, Vector3.up, HeightFromGround + 0.1f))
        {
            UnCrouched = false;
            IsCrouching = true;
            return;
        }
        else
        {
            StandUp();
        }

        if(UnCrouched == false)
        {
            CrouchDown();
        }
    }

    #endregion

    #region Sprinting
    //---SPRINTING---//
    void Sprinting()
    {
        if(Direction == Vector3.zero && CurrentSpeed > WalkingSpeed)
        {
            CurrentSpeed -= Deceleration * Time.deltaTime;
        }
        else if(Direction == Vector3.zero && CurrentSpeed <= WalkingSpeed)
        {
            CurrentSpeed = WalkingSpeed;
        }
        else if(CurrentSpeed < RunningSpeed)
        {
            CurrentSpeed += Acceleration * Time.deltaTime;
        }
        else if(CurrentSpeed >= RunningSpeed)
        {
            CurrentSpeed = RunningSpeed;
        }
    }
    #endregion

    #region Rolling
    void Rolling()
    {
        if(IsRolling == true)
        {
            if(CurrentRollTime > 0)
            {
                Controller.Move(RollDirection * RollingSpeed * Time.deltaTime);
                CurrentRollTime -= Time.deltaTime;
            }
            else if(CurrentRollTime <= 0)
            {
                IsRolling = false;
            }
        }
        if(IsRolling == false && CurrentRollTime < RollingTime)
        {
            CurrentRollTime += Time.deltaTime;
            if(CurrentRollTime > RollingTime)
            {
                CurrentRollTime = RollingTime;
            }
        }
    }
    #endregion

    #region StandUp
    //---STAND-UP---//
    void StandUp()
    {
        CurrentSpeed = WalkingSpeed;
        Collider.height = StandardHeight;
        Controller.height = StandardHeight;
        Controller.center = new Vector3(0f, 0f, 0f);
        Collider.center = new Vector3(0f, 0f, 0f);
        GroundHeight = HeightFromGround;
    }

    #endregion
    
    #region CrouchDown
    //---CROUCH-DOWN---//
    void CrouchDown()
    {
        CurrentSpeed = CrouchSpeed;
        Collider.height = CrouchingHeight;
        Controller.height = CrouchingHeight;
        Controller.center = new Vector3 (0f, -0.5f, 0f);
        Collider.center = new Vector3 (0f, -0.5f, 0f);
        GroundHeight = CrouchingHeightFromGround;
        IsCrouching = true;
    }

    #endregion
    
    #endregion
}
