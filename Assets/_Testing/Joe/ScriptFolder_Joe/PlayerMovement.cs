using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    public int hp;
    [SerializeField] private float WalkingSpeed;
    [SerializeField] private float RunningSpeed;
    [SerializeField] private float CrouchSpeed;
    [Tooltip("The increase in speed when sprinting.")]
    [SerializeField] private float Acceleration;
    [SerializeField] public bool IsSprinting = false;
    [SerializeField] private bool UnSprinting = true;
    private bool canMove = true;
    public float CurrentSpeed;
    private Vector3 Direction;

    [Header("Crouching")]
    [Tooltip("The standing capsule collider height.")]
    [SerializeField] private float StandardHeight;
    [SerializeField] private float CrouchingHeight;
    [Tooltip("Set this as the same Y center for the player controller & colider.")]
    [SerializeField] private float SetCenterHeight;
    [SerializeField] public bool IsCrouching = false;
    [SerializeField] private bool IsStanding = true;
    [SerializeField] private bool IsCovered;

    [Header("Physics")]
    [SerializeField] private float Gravity;
    [Tooltip("Your jump height while walking around.")]
    [SerializeField] private float MovingJumpHeight;
    [Tooltip("Your jump height when standing still.")]
    [SerializeField] private float StillJumpHeight;
    [Tooltip("Your jump height when you dive.")]
    [SerializeField] private float DiveHeight;
    public CapsuleCollider playerCollider;
    [SerializeField] public CharacterController Controller;
    [SerializeField] private bool IsGrounded = true;
    private float HeightFromGround;
    private float CrouchingHeightFromGround;
    private float GroundHeight;
    private Vector3 VerticalVelocity = Vector3.zero;
    private Vector3 Test;

    [Header("Rolling")]
    [Tooltip("How fast you roll.")]
    [SerializeField] private float RollingSpeed;
    [Tooltip("How long you roll for.")]
    [SerializeField] private float RollingTime;
    [Tooltip("This delays the player from rolling, this is used for roll + animation sync")]
    [SerializeField] private float DelayTime;
    [SerializeField] private bool IsRolling;
    [SerializeField] private bool StillRolling;
    private float CurrentRollTime;
    private float CurrentDelayTime;
    private Vector3 RollDirection;

    [Header("Sliding")]
    [Tooltip("The speed at which you decrese down to slide.")]
    [SerializeField] private float Deceleration;
    [SerializeField] public bool IsSliding;

    [Header("Diving")]
    [Tooltip("How far you dive.")]
    [SerializeField] private float DiveSpeed;
    [Tooltip("How long you dive.")]
    [SerializeField] private float DiveTime;
    [SerializeField] private bool IsDiving;
    [SerializeField] private bool ResetDiving;
    [SerializeField] private bool StillDiving;
    private float CurrentDiveTime;

    //[Header("Camera")]
    private Transform PlayerCamera;
    private Vector3 FacingDirection;

    private LayerMask mask; //player layer mask to occlude the player from themselves

    [Header("Push & Pull")]
    [SerializeField] private float PushPullLightSpeed;
    [SerializeField] private float PushPullMediumSpeed;
    [SerializeField] private float PushPullHeavySpeed;
    [SerializeField] private bool IsPushPull;

    [Header("Togglable Buttons")]
    public bool ToggleSprint;
    public bool ToggleCrouch;
    [SerializeField] private bool IsUncovered;

    [Header("Stun State")]
    [Tooltip("This is for the total time the player will remained stuned with no action taken")]
    [SerializeField] private float StunTime;
    [Tooltip("This is the value that will be added when the player tries to shake off a stun.")]
    public float BreakOutValue;
    public float BreakOutCounter;
    [SerializeField] private float BreakOutThreshold;
    public bool IsStunned = false;
    public float CurrentStunTime = 0;

    [Header("Player Noise")]
    [Tooltip("The detection radius of the player when they are standing still and when they are moving while crouched")]
    [SerializeField] private int SilentLevel = 0;
    [Tooltip("The detection radius of the player when they are moving, jumping while standing still, jumping while moving, and rolling.")]
    [SerializeField] private int QuietLevel = 3;
    [Tooltip("The detection radius of the player when they are sliding")]
    [SerializeField] private int MediumLevel = 6;
    [Tooltip("The detection radius of the player when they are running and diving.")]
    [SerializeField] private int LoudLevel = 12;
    [Tooltip("This is the time it will take to update the states and send out a sound check for the guard.")]
    [SerializeField] private float NoiseClock = 0.25f;
    [SerializeField] private int CurrentLevel;
    private float CurrentNoiseClock;


    //[Header("Suspicion Manager")]
    public SuspicionManager SusMan;
    public GameController gameController;
    public InventoryController inventoryController;

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
    public bool Stunned;

    #endregion

    void Awake()
    {
        //This might need to be updated for any changes to the sus manager.
        
    }

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
        CurrentRollTime = RollingTime;
        CurrentDiveTime = DiveTime;
        CurrentDelayTime = DelayTime;
        Controller = GetComponent<CharacterController>();
        playerCollider = GetComponent<CapsuleCollider>();
        PlayerCamera = Camera.main.transform;
        mask = LayerMask.GetMask("Player");
        mask = ~mask;
        HeightFromGround = StandardHeight / 2;
        CrouchingHeightFromGround = CrouchingHeight / 2;

        SusMan = (SuspicionManager)FindObjectOfType(typeof(SuspicionManager));

        if (gameController == null)
        {
            gameController = FindObjectOfType<GameController>();
        }

        transform.position = GameController.gameControllerInstance.lastCheckPoint;
    }

    void Update()
    {
        
        GroundCheck();
        Rolling();
        AnimationStates();

        if ((!IsCrouching && !IsSprinting && !IsPushPull) || IsUncovered)
        {
            CoveredCheck();
            IsUncovered = false;
        }

        if (IsStanding && IsCrouching && IsCovered && ToggleCrouch)
        {
            UnCrouchedCheck();
        }

        if (IsSprinting && !IsCrouching)
        {
            Sprinting();
        }

        #region Gravity
        if (IsGrounded && Controller.velocity.y > 0)
        {
            VerticalVelocity.y = 0;
        }

        if (!IsGrounded)
        {
            VerticalVelocity.y -= Gravity * Time.deltaTime;
        }
        Controller.Move(VerticalVelocity * Time.deltaTime);


        #endregion

        #region Movement
        //Over the shoulder cam roll doesn't work. Cam is only going to be used for free cam.
        if (!IsRolling && !IsSliding && !IsDiving && !StillDiving && !IsStunned)
        {
            FacingDirection = PlayerCamera.forward * Direction.z + PlayerCamera.right * Direction.x;
        }
        FacingDirection.y = 0f;
        FacingDirection = FacingDirection.normalized;

        //Movement
        if (!IsRolling && !IsSliding && !IsDiving && !StillDiving && !IsStunned && canMove)
        {
            Controller.Move(FacingDirection * CurrentSpeed * Time.deltaTime);
        }

        //Setting Roll Direction for rolling, diving, and sliding.
        if (FacingDirection != Vector3.zero && !IsRolling && !IsSliding && FacingDirection.y == 0 && !IsDiving)
        {
            RollDirection = FacingDirection;
        }

        //Facing direction.
        if (FacingDirection != Vector3.zero && !IsRolling && !IsSliding && !IsDiving)
        {
            Quaternion toRotation = Quaternion.LookRotation(FacingDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }

        #endregion

        #region Slide Action

        if (IsSprinting == true && IsCrouching == true)
        {
            IsSliding = true;
            Sliding();
        }
        else if (IsSliding && IsSprinting && !IsCrouching)
        {
            CoveredCheck();
            if (IsCovered)
            {
                return;
            }
            else
            {
                IsSliding = false;
                playerCollider.height = StandardHeight;
                Controller.height = StandardHeight;
                Controller.center = new Vector3(0f, SetCenterHeight, 0f);
                playerCollider.center = new Vector3(0f, SetCenterHeight, 0f);
                GroundHeight = HeightFromGround;
            }
        }
        else
        {
            IsSliding = false;
        }

        #endregion

        #region Dive Action
        DiveJump();
        if (!IsDiving)
        {
            if (CurrentDiveTime < DiveTime)
            {
                CurrentDiveTime += Time.deltaTime;
                if (CurrentDiveTime > DiveTime)
                {
                    CurrentDiveTime = DiveTime;
                }
            }
        }
        #endregion

        #region Toggle Checks
        if (ToggleSprint)
        {
            UnSprinting = true;
        }

        if (ToggleCrouch)
        {
            IsStanding = true;
        }

        #endregion

        #region Stun Work
        if (IsStunned)
        {
            canMove = false;
            CurrentStunTime += Time.deltaTime;

            if (BreakOutCounter >= BreakOutThreshold && hp >= 1)
            {
                Collider[] hitColliders = Physics.OverlapSphere(playerCollider.transform.position, 10f, 1 << 8);
                foreach (Collider collider in hitColliders)
                {
                    if (collider.GetComponent<EnemyManager>() != null)
                    {
                        collider.GetComponent<EnemyManager>().isStunned = true;
                    }

                }
                IsStunned = false;
                IsDiving = false;
                StartCoroutine(IBreakFreeDelay());
                CurrentStunTime = 0;
                BreakOutCounter = 0;
                hp -= 1;
            }

            else if (CurrentStunTime >= StunTime || hp <= 0)
            {
                FindObjectOfType<LoseScreenMenuManager>().LoseGame();
            }
        }

        #endregion

        #region Noise Clock
        //Might be replaced later.
        if (CurrentNoiseClock < 0)
        {
            PlayerSound();
            CurrentNoiseClock = NoiseClock;
        }
        CurrentNoiseClock -= Time.deltaTime;
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
        if (IsGrounded && !IsCrouching && !IsPushPull)
        {
            if (Direction.magnitude > 0.1)
            {
                if (!IsSprinting)
                {
                    Jumping = true;
                    animationController.IsPlayerJumping(Jumping);
                    VerticalVelocity.y = Mathf.Sqrt(-2f * MovingJumpHeight * -Gravity);
                    Controller.Move(VerticalVelocity * Time.deltaTime);
                }
                else if (IsSprinting)
                {
                    IsDiving = true;
                    VerticalVelocity.y = Mathf.Sqrt(-2f * DiveHeight * -Gravity);
                    Controller.Move(VerticalVelocity * Time.deltaTime);
                }
            }
            else if (Direction.magnitude <= 0.1)
            {
                Jumping = true;
                animationController.IsPlayerJumping(Jumping);
                VerticalVelocity.y = Mathf.Sqrt(-2f * StillJumpHeight * -Gravity);
                Controller.Move(VerticalVelocity * Time.deltaTime);
            }
        }
        else if (IsGrounded && IsCrouching && !IsPushPull)
        {
            IsStanding = true;
            CoveredCheck();
        }
    }
    #endregion

    #region Sprint
    //----------SPRINT----------//
    public void Sprint(bool Sprinting)
    {
        if (Sprinting && !IsCrouching && !IsPushPull)
        {
            IsSprinting = true;
            if (UnSprinting == false)
            {
                UnSprinting = true;
            }
            else if (UnSprinting == true)
            {
                UnSprinting = false;
            }
        }
        else if (!Sprinting && !IsCrouching && UnSprinting && !IsPushPull)
        {
            CurrentSpeed = WalkingSpeed;
            IsSprinting = false;
        }
        else if (IsGrounded && IsCrouching && !IsPushPull)
        {
            IsStanding = true;
            IsSprinting = true;
            UnSprinting = false;
            CoveredCheck();
        }
    }
    #endregion

    #region Crouch
    //----------CROUCH----------//
    public void Crouch(bool Crouching)
    {
        if (Crouching && IsGrounded && !IsPushPull)
        {
            IsCrouching = true;
            if (IsStanding && !IsSprinting)
            {
                CrouchDown();
                IsStanding = false;
            }
            else if (!IsStanding)
            {
                IsStanding = true;
            }
        }
        else if (!Crouching && IsStanding)
        {
            IsCrouching = false;
        }
    }
    #endregion

    #region Roll
    //----------ROLL----------//
    public void Roll(bool Rolling)
    {
        if (Rolling && IsCrouching)
        {
            IsRolling = true;
        }

        StillRolling = Rolling;
    }

    #endregion

    #region Sliding
    //---SLIDING---//
    void Sliding()
    {
        if (CurrentSpeed > CrouchSpeed)
        {
            IsCrouching = true;
            Controller.Move(RollDirection * CurrentSpeed * Time.deltaTime);
            playerCollider.height = CrouchingHeight;
            Controller.height = CrouchingHeight;
            Controller.center = new Vector3(0f, -(CrouchingHeightFromGround), 0f);
            playerCollider.center = new Vector3(0f, -(CrouchingHeightFromGround), 0f);
            GroundHeight = CrouchingHeightFromGround;
            CurrentSpeed -= Deceleration * Time.deltaTime;
        }
        else if (CurrentSpeed <= CrouchSpeed)
        {
            CrouchDown();
            IsStanding = false;
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
        //Current Bug: Once I hit the ground, then I can start moving.
        if (IsDiving)
        {
            if (CurrentDiveTime > 0)
            {
                Controller.Move(RollDirection * DiveSpeed * Time.deltaTime);
                CurrentDiveTime -= Time.deltaTime;
            }
            else if (CurrentDiveTime <= 0)
            {
                IsDiving = false;
                ResetDiving = true;
            }
        }
        if (!IsDiving && ResetDiving)
        {
            if (IsGrounded)
            {
                IsSprinting = false;
                UnSprinting = true;
                IsStanding = false;
                ResetDiving = false;
                StillDiving = false;
                CrouchDown();
                if (StillRolling)
                {
                    IsRolling = true;
                    StillRolling = false;
                }
            }
            else
            {
                Controller.Move(RollDirection * DiveSpeed * Time.deltaTime);
                StillDiving = true;
            }
        }
    }

    #endregion

    #region Push/Pull
    public void PushPullCheck(bool IsNearPushPull, int CheckSpeed)
    {
        if (!IsSprinting && !IsCrouching)
        {
            if (CheckSpeed == 1)
            {
                CurrentSpeed = PushPullLightSpeed;
                IsPushPull = true;
            }
            else if (CheckSpeed == 2)
            {
                CurrentSpeed = PushPullMediumSpeed;
                IsPushPull = true;
            }
            else if (CheckSpeed == 3)
            {
                CurrentSpeed = PushPullHeavySpeed;
                IsPushPull = true;
            }
        }

        if (!IsNearPushPull && !IsCrouching && !IsSprinting && !IsSliding && !IsRolling && !IsDiving)
        {
            CurrentSpeed = WalkingSpeed;
            IsPushPull = false;
        }
    }

    #endregion

    #region Ground
    //---GROUNDCHECK---//
    void GroundCheck()
    {
        //Debug.DrawRay(Controller.transform.position + Controller.center, Vector3.down, Color.red, Controller.height / 2  + 0.1f);
        //Physics.Raycast(Controller.transform.position + Controller.center, Vector3.down, Controller.height / 2  + 0.1f)
        Vector3 groundCheck = new Vector3(transform.position.x, transform.position.y - (StandardHeight / 2.6f), transform.position.z);
        Test = groundCheck;
        //StandardHeight / 4
        if (Physics.CheckSphere(groundCheck, StandardHeight / 6f, mask, QueryTriggerInteraction.Ignore))
        {
            IsGrounded = true;
            Jumping = false;
            animationController.IsPlayerJumping(Jumping);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Test, StandardHeight / 6f);
        Gizmos.color = Color.blue;
    }

    #endregion

    #region Covered
    //---COVEREDCHECK---//
    void CoveredCheck()
    {
        //---USE-SOMETHING-THAT-ISN'T-RAYCAST---//
        if (Physics.Raycast(transform.position, Vector3.up, Controller.height / 2 + 0.1f) && IsGrounded)
        {
            IsStanding = false;
            IsCrouching = true;
            IsCovered = true;
            return;
        }
        else if (!IsSliding)
        {
            StandUp();
            IsCovered = false;
            if (IsCrouching)
            {
                IsCrouching = false;
            }
        }

        if (IsStanding == false && IsGrounded)
        {
            CrouchDown();
        }
    }

    #endregion

    #region Sprinting
    //---SPRINTING---//
    void Sprinting()
    {
        if (Direction == Vector3.zero && CurrentSpeed > WalkingSpeed)
        {
            CurrentSpeed -= Deceleration * Time.deltaTime;
        }
        else if (Direction == Vector3.zero && CurrentSpeed <= WalkingSpeed)
        {
            CurrentSpeed = WalkingSpeed;
        }
        else if (CurrentSpeed < RunningSpeed)
        {
            CurrentSpeed += Acceleration * Time.deltaTime;
        }
        else if (CurrentSpeed >= RunningSpeed)
        {
            CurrentSpeed = RunningSpeed;
        }
    }
    #endregion

    #region Rolling
    void Rolling()
    {
        if (IsRolling)
        {
            if (CurrentDelayTime > 0)
            {
                CurrentDelayTime -= Time.deltaTime;
            }
            else
            {
                if (CurrentRollTime > 0)
                {
                    Controller.Move(RollDirection * RollingSpeed * Time.deltaTime);
                    CurrentRollTime -= Time.deltaTime;
                }
                else if (CurrentRollTime <= 0)
                {
                    IsRolling = false;
                }
            }

        }
        if (!IsRolling && CurrentRollTime < RollingTime)
        {
            CurrentRollTime += Time.deltaTime;
            CurrentDelayTime = DelayTime;
            if (CurrentRollTime > RollingTime)
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
        playerCollider.height = StandardHeight;
        Controller.height = StandardHeight;
        Controller.center = new Vector3(0f, SetCenterHeight, 0f);
        playerCollider.center = new Vector3(0f, SetCenterHeight, 0f);
        GroundHeight = HeightFromGround;
    }

    #endregion

    #region CrouchDown
    //---CROUCH-DOWN---//
    void CrouchDown()
    {
        CurrentSpeed = CrouchSpeed;
        playerCollider.height = CrouchingHeight;
        Controller.height = CrouchingHeight;
        Controller.center = new Vector3(0f, -(CrouchingHeightFromGround), 0f);
        playerCollider.center = new Vector3(0f, -(CrouchingHeightFromGround), 0f);
        GroundHeight = CrouchingHeightFromGround;
        IsCrouching = true;
    }

    #endregion

    #region UnCrouched Check
    void UnCrouchedCheck()
    {
        IsUncovered = true;
    }

    #endregion

    #region Player Sound Controller
    void PlayerSound()
    {
        if ((Idle || IdleCrouch || Crouching) && !CrouchRoll && !Jumping && !Slide && !Running)
        {
            CurrentLevel = SilentLevel;
        }
        else if (((Moving || Jumping || CrouchRoll) || (Idle && Jumping)) && !Slide && !Running)
        {
            CurrentLevel = QuietLevel;
        }
        else if (Slide)
        {
            CurrentLevel = MediumLevel;
        }
        else if ((Running && Moving) || Diving)
        {
            CurrentLevel = LoudLevel;
        }

        SusMan.AlertGuards(transform.position, transform.position, CurrentLevel);
    }

    #endregion

    #region Animation States
    //---ANIMATIONSTATES---//
    void AnimationStates()
    {
        //---IDLE---//
        if (Direction == Vector3.zero && IsGrounded && !IsCrouching && !IsSliding && !IsRolling && !IsStunned)
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
        if (Direction == Vector3.zero && IsGrounded && IsCrouching && !IsSliding && !IsRolling && !IsStunned)
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
        if (Direction != Vector3.zero && IsGrounded && !IsSliding && !IsRolling && !IsStunned)
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
        if (IsSprinting && !IsSliding && !IsDiving && !ResetDiving && !IsStunned && Direction != Vector3.zero)
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
        if (IsCrouching && !IsSliding && !IsStunned && Direction != Vector3.zero)
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
        if (IsRolling)
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
        if (IsSliding)
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
        if (IsDiving || ResetDiving)
        {
            Diving = true;
            animationController.IsPlayerDiving(Diving);
        }
        else
        {
            Diving = false;
            animationController.IsPlayerDiving(Diving);
        }

        //---STUNNED---//
        if (IsStunned)
        {
            Stunned = true;
            animationController.IsPlayerStunned(Stunned);
        }
        else
        {
            Stunned = false;
            animationController.IsPlayerStunned(Stunned);
        }
    }

    #endregion

    public IEnumerator IBreakFreeDelay()
    {
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    #endregion
}
