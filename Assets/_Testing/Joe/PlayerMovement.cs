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
    [SerializeField] private bool IsSprinting = false;
    [SerializeField] private bool UnSprinting = true;
    private float CurrentSpeed;
    private Vector3 Direction;

    [Header("Crouching")]
    [SerializeField] private float StandardHeight;
    [SerializeField] private float CrouchingHeight;
    [SerializeField] private bool IsCrouching = false;
    [SerializeField] private bool UnCrouched = true;


    [Header("Physics")]
    [SerializeField] private float Gravity;
    [SerializeField] private float MovingJumpHeight;
    [SerializeField] private float StillJumpHeight;
    [SerializeField] private float DiveHeight;
    [SerializeField] private float HeightFromGround;
    [SerializeField] private float CrouchingHeightFromGround;
    [SerializeField] private CapsuleCollider Collider;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private bool IsGrounded = true;
    private float GroundHeight;
    private Vector3 VerticalVelocity = Vector3.zero;


    [Header("Rolling")]
    [SerializeField] private float RollingSpeed;
    [SerializeField] private float RollingTime;
    [SerializeField] private bool IsRolling;
    private float CurrentRollTime;
    private Vector3 RollDirection;

    [Header("Sliding")]
    [SerializeField] private float Deceleration;
    [SerializeField] private bool IsSliding;

    [Header("Diving")]
    [SerializeField] private float DiveSpeed;
    [SerializeField] private float DiveTime;
    [SerializeField] private bool IsDiving;
    [SerializeField] private bool ResetDiving;
    private float CurrentDiveTime;

    [Header("Animation States")]
    [SerializeField] private AnimationController animationController;
    public bool Idle;
    public bool IdleCrouch;
    public bool Moving;
    public bool Crouching;
    public bool Running;
    public bool Jumping;
    public bool CrouchRoll;
    public bool Slide;
    public bool Diving;
    
    //[Header("Camera")]
    private Transform PlayerCamera;
    private Vector3 FacingDirection;

    private LayerMask mask; //player layer mask to occlude the player from themselves

    #endregion

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
        CurrentRollTime = RollingTime;
        CurrentDiveTime = DiveTime;
        Controller = GetComponent<CharacterController>();
        Collider = GetComponent<CapsuleCollider>();
        PlayerCamera = Camera.main.transform;
        mask = LayerMask.GetMask("Player");
        mask = ~mask;
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

        VerticalVelocity.y -= Gravity * Time.deltaTime;
        Controller.Move(VerticalVelocity * Time.deltaTime);
        

        #endregion

        #region Movement
        //Over the shoulder cam roll doesn't work. Cam is only going to be used for free cam.
        FacingDirection = PlayerCamera.forward * Direction.z + PlayerCamera.right * Direction.x;
        FacingDirection.y = 0f;
        FacingDirection = FacingDirection.normalized;

        if(!IsRolling && !IsSliding && !IsDiving)
        {
            Controller.Move(FacingDirection * CurrentSpeed * Time.deltaTime);
        }

        if(FacingDirection != Vector3.zero && !IsRolling && !IsSliding && FacingDirection.y == 0 && !IsDiving)
        {
            RollDirection = FacingDirection;
        }

        if(FacingDirection != Vector3.zero && !IsRolling && !IsSliding && !IsDiving)
        {
            Quaternion toRotation = Quaternion.LookRotation(FacingDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
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
        else
        {
            IsSliding = false;
        }

        #endregion

        #region Dive Action
        DiveJump();
        if(!IsDiving)
        {
            if(CurrentDiveTime < DiveTime)
            {
                CurrentDiveTime += Time.deltaTime;
                if(CurrentDiveTime > DiveTime)
                {
                    CurrentDiveTime = DiveTime;
                }
            }
        }
        #endregion

        #region Check For Animations
        AnimationStates();

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
        if(IsGrounded && !IsCrouching)
        {
            if (Direction.magnitude > 0.1)
            {
                if(!IsSprinting)
                {
                    Jumping = true;
                    animationController.IsPlayerJumping(Jumping);
                    VerticalVelocity.y = Mathf.Sqrt(-2f * MovingJumpHeight * -Gravity);
                    Controller.Move(VerticalVelocity * Time.deltaTime);
                }
                else if(IsSprinting)
                {
                    IsDiving = true;
                    VerticalVelocity.y = Mathf.Sqrt(-2f * DiveHeight * -Gravity);
                    Controller.Move(VerticalVelocity * Time.deltaTime);
                    print(RollDirection);
                }
            }
            else if(Direction.magnitude <= 0.1)
            {
                Jumping = true;
                animationController.IsPlayerJumping(Jumping);
                VerticalVelocity.y = Mathf.Sqrt(-2f * StillJumpHeight * -Gravity);
                Controller.Move(VerticalVelocity * Time.deltaTime);
            }
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
            UnSprinting = true;
            IsSliding = false;
        }
    }
    #endregion

    #region Dive
    //---DIVEJUMP---//
    void DiveJump()
    {
        //Current Bug: I need to end up crouching.
        if(IsDiving)
        {   
            if(CurrentDiveTime > 0)
            {
                Controller.Move(RollDirection * DiveSpeed * Time.deltaTime);
                CurrentDiveTime -= Time.deltaTime;
            }
            else if(CurrentDiveTime <= 0)
            {
                IsDiving = false;
                ResetDiving = true;
            }
        }
        if(!IsDiving && ResetDiving)
        {
            if(IsGrounded)
            {
                IsSprinting = false;
                UnSprinting = true;
                UnCrouched = false;
                ResetDiving = false;
                CrouchDown();
            }
        }
    }

    #endregion

    #region Ground
    //---GROUNDCHECK---//
    void GroundCheck()
    {
        //Debug.DrawRay(Controller.transform.position + Controller.center, Vector3.down, Color.red, Controller.height / 2  + 0.1f);
        //Physics.Raycast(Controller.transform.position + Controller.center, Vector3.down, Controller.height / 2  + 0.1f)
        Vector3 groundCheck = new Vector3 (transform.position.x, transform.position.y - (StandardHeight * 0.3f), transform.position.z);
        
        if(Physics.CheckSphere(groundCheck, StandardHeight / 4, mask))
        {
            IsGrounded = true;
            Jumping = false;
            animationController.IsPlayerJumping(Jumping);
        }
        else
        {
            IsGrounded = false;
            if(IsCrouching)
            {
                StandUp();
                IsCrouching = false;
            }
        }
    }
    #endregion

    #region Covered
    //---COVEREDCHECK---//
    void CoveredCheck()
    {
        //---USE-SOMETHING-THAT-ISN'T-RAYCAST---//
        if(Physics.Raycast(transform.position, Vector3.up, Controller.height / 2 + 0.1f))
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
    
    #region Animation States
    //---ANIMATIONSTATES---//
    void AnimationStates()
    {
        //---IDLE---//
        if(Direction == Vector3.zero && IsGrounded && !IsCrouching && !IsSliding && !IsRolling)
        {
            Idle = true;
            animationController.IsPlayerIdle(Idle);
        }
        else
        {
            Idle = false;
            animationController.IsPlayerIdle(Idle);
        }

        //---CROUCH-IDLE---//
        if(Direction == Vector3.zero && IsGrounded && IsCrouching && !IsSliding && !IsRolling)
        {
            IdleCrouch = true;
            animationController.IsPlayerCrouchIdle(IdleCrouch);
        }
        else
        {
            IdleCrouch = false;
            animationController.IsPlayerCrouchIdle(IdleCrouch);
        }

        //---WALKING---//
        if(Direction != Vector3.zero && IsGrounded && !IsSliding && !IsRolling)
        {
            Moving = true;
            animationController.IsPlayerWalking(Moving);
        }
        else
        {
            Moving = false;
            animationController.IsPlayerWalking(Moving);
        }

        //---RUNNING---//
        if(IsSprinting && !IsSliding && !IsDiving && !ResetDiving && Direction != Vector3.zero)
        {
            Running = true;
            animationController.IsPlayerSprinting(Running);
        }
        else
        {
            Running = false;
            animationController.IsPlayerSprinting(Running);
        }

        //---CROUCHING---//
        if(IsCrouching && !IsSliding && Direction != Vector3.zero)
        {
            Crouching = true;
            animationController.IsPlayerCrouching(Crouching);
        }
        else
        {
            Crouching = false;
            animationController.IsPlayerCrouching(Crouching);
        }

        //---ROLLING---//
        if(IsRolling)
        {
            CrouchRoll = true;
            animationController.IsPlayerRolling(CrouchRoll);
        }
        else
        {
            CrouchRoll = false;
            animationController.IsPlayerRolling(CrouchRoll);
        }
        
        //---SLIDING---//
        if(IsSliding)
        {
            Slide = true;
            animationController.IsPlayerSliding(Slide);
        }
        else
        {
            Slide = false;
            animationController.IsPlayerSliding(Slide);
        }

        //---DIVING---//
        if(IsDiving || ResetDiving)
        {
            Diving = true;
            animationController.IsPlayerDiving(Diving);
        }
        else
        {
            Diving = false;
            animationController.IsPlayerDiving(Diving);
        }
    }

    #endregion

    #endregion
}
