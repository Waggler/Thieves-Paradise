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
    public bool IsCrouching = false;
    public bool UnCrouched = true;


    [Header("Physics")]
    [SerializeField] private float Gravity;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float HeightFromGround;
    [SerializeField] private float CrouchingHeightFromGround;
    [SerializeField] private CapsuleCollider Collider;
    [SerializeField] private CharacterController Controller;
    private float GroundHeight;
    private Vector3 VerticalVelocity = Vector3.zero;
    [SerializeField] private bool IsGrounded = true;

    [Header("Rolling")]
    [SerializeField] private float RollingSpeed;
    [SerializeField] private float RollingTime;
    private float CurrentRollTime;
    private Vector3 RollDirection;
    private bool IsRolling;

    //[Header("Camera")]
    private Transform PlayerCamera;
    private Vector3 FacingDirection;
    
    private LayerMask mask; //player layer mask to occlude the player from themselves

    #endregion

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
        CurrentRollTime = RollingTime;
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
        FacingDirection = PlayerCamera.forward * Direction.z + PlayerCamera.right * Direction.x;
        FacingDirection.y = 0f;
        FacingDirection = FacingDirection.normalized;

        Controller.Move(FacingDirection * CurrentSpeed * Time.deltaTime);

        if(FacingDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(FacingDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        #endregion

        #region Roll Action
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

        #endregion
    }

    #region Functions
 
    #region Move
    //----------MOVEMENT----------//
    public void Movement(Vector3 Move)
    {
        Direction = new Vector3(Move.x, 0f, Move.z);
        if(Direction != Vector3.zero)
        {
            RollDirection = Direction;
        }
    }
    #endregion

    #region Jump
    //----------JUMP----------//
    public void Jump()
    {
        if(IsGrounded && !IsCrouching)
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
            if(UnCrouched == true)
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
            //StandUp();
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
            
        }
        else
        {
            IsGrounded = false;
            if (IsCrouching)
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
