using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public Vector3 moveVector;
    private Vector3 directionVector;
    public bool isSprinting;
    public bool isCrouching;
    public bool isRolling;
    public bool isPushPull;
    private PlayerMovement playerMovement;
    private InventoryController inventoryController;
    [SerializeField] private CamSwitch camSwitch;
    public float rollCooldownTime;
    private float cooldownTimer;
    public bool jumpPressCounter;
    public bool StopTheJump;
    private bool GroundCheck;
    [HideInInspector] public bool IsZoomed;
    [HideInInspector] public bool IsThrowing;
    [HideInInspector] public float ZoomLookSensitivity = 1;

    
    private PauseTarget PauseMenu;
    private SettingTarget Settings;
    private PhotoModeTarget Camera;
    private ScoreKeeper scoreKeeper;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        inventoryController = GetComponent<InventoryController>();
        Cursor.lockState = CursorLockMode.Locked;
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        if (cooldownTimer < rollCooldownTime)
        {
            cooldownTimer += Time.deltaTime;
        }

        GroundCheck = playerMovement.IsGrounded;
        if(GroundCheck)
        {
            jumpPressCounter = false;
            StopTheJump = false;
        }

        PauseMenu = (PauseTarget)FindObjectOfType(typeof(PauseTarget));
        Settings = (SettingTarget)FindObjectOfType(typeof(SettingTarget));
        Camera = (PhotoModeTarget)FindObjectOfType(typeof(PhotoModeTarget));
    }

    #region Inputs

    #region MoveInput
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 contextValue = context.ReadValue<Vector2>();
        //print(contextValue.magnitude);

        

        if (contextValue.magnitude < 0.75f && playerMovement.IsSprinting)
        {
            //print("Ending sprint due to lack of movement");
            isSprinting = true;
            playerMovement.Sprint(isSprinting);
            isSprinting = false;
            playerMovement.Sprint(isSprinting);
        }

        if (context.performed)
        {
            moveVector = new Vector3(contextValue.x, 0, contextValue.y);

            if (playerMovement.IsStunned)
            {
                playerMovement.BreakOutCounter += playerMovement.BreakOutValue;
            }
                
            directionVector = moveVector;
            playerMovement.Movement(moveVector);
        }
    }// END MOVE
    #endregion

    #region JumpInput
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            if (!jumpPressCounter && !StopTheJump)
            {
                playerMovement.Jump();
                jumpPressCounter = true;
                StopTheJump = true;
            }
            scoreKeeper.jumped = true;
        }
    }// END JUMP
    #endregion

    #region SprintInput
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isSprinting = true;
            playerMovement.Sprint(isSprinting);
        }
        if (context.canceled)
        {

            isSprinting = false;
            playerMovement.Sprint(isSprinting);
        }
    }// END SPRINT
    #endregion

    #region CrouchInput
    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCrouching = true;
            playerMovement.Crouch(isCrouching);

            scoreKeeper.crouched = true;
        }
        if (context.canceled)
        {
            isCrouching = false;
            playerMovement.Crouch(isCrouching);
        }
    }// END CROUCH
    #endregion

    #region RollInput
    public void Roll(InputAction.CallbackContext context)
    {
        if (cooldownTimer < rollCooldownTime)
        {
            return;
        }
        if (context.started)
        {
            isRolling = true;
            playerMovement.Roll(isRolling);
            cooldownTimer = 0;
        }
        if (context.canceled)
        {
            isRolling = false;
            playerMovement.Roll(isRolling);
        }
    }//END ROLL

    #endregion

    #region ZoomIn
    public void ZoomIn(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsZoomed = true;
            camSwitch.SwitchState(IsZoomed);
        }
        else if(context.canceled)
        {
            IsZoomed = false;
            camSwitch.SwitchState(IsZoomed);
        }
    }
    public void ZoomCancel()
    {
        IsZoomed = false;
        IsThrowing = false;
        camSwitch.SwitchState(IsZoomed);
    }
    // END ZOOM IN

    public void StartThrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (inventoryController.IsActiveSlotEmpty() || inventoryController.CheckSlotForKeyItem(inventoryController.GetActiveItemIndex()))
            {
                IsThrowing = false;
                //return;
            }else
            {
                IsThrowing = true;
            }
        }
        if (context.canceled)
        {
            IsThrowing = false;
        }
    }

    #endregion

    #region ZoomCamControls

    public void ZoomLook(InputAction.CallbackContext context)
    {
        if (IsZoomed && (!PauseMenu && !Settings && !Camera)) //only use this when zoomed in
        {
            if (context.performed)
            {
                Vector2 contextValue = context.ReadValue<Vector2>();
                //print(contextValue);
                if (contextValue.x != 0)
                {
                    transform.Rotate(Vector3.up, contextValue.x * ZoomLookSensitivity, Space.Self);
                }
                if (contextValue.y != 0)
                {
                    inventoryController.throwForce += contextValue.y * ZoomLookSensitivity * 2f;
                }
            }
        }
    }

    public void ChangeZoomLookSensitivity(float newSens)
    {
        ZoomLookSensitivity = newSens;
    }
    #endregion
    
    #endregion
}