using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Vector3 moveVector;
    private Vector3 directionVector;
    public bool isSprinting;
    public bool isCrouching;
    public bool isRolling;
    public bool isPushPull;
    private PlayerMovement playerMovement;
    public float rollCooldownTime;
    private float cooldownTimer;
    private int jumpPressCounter = 1;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (cooldownTimer < rollCooldownTime)
        {
            cooldownTimer += Time.deltaTime;
        }
    }

   //DELETE ME

    #region Inputs

    #region MoveInput
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 contextValue = context.ReadValue<Vector2>();

        if(context.performed)
        {
            moveVector = new Vector3(contextValue.x, 0, contextValue.y);
            
            if (playerMovement.IsStunned)
            {
                playerMovement.BreakOutCounter += playerMovement.BreakOutValue;
            }

            if (moveVector != Vector3.zero)
            {
                directionVector = moveVector;
                playerMovement.Movement(moveVector);
                playerMovement.Inertia = false;
            }
            else if (moveVector == Vector3.zero)
            {
                //Have a new var be the last movement direction then set up a check as well for if the speed is sprinting or walking.
                //Have a timer so the player only moves that way for about a second after the button push or two if they are sprinting.
                //playerMovement.CurrentSpeed/2;
                
                playerMovement.Movement(directionVector);
                playerMovement.Inertia = true;
            }
            print(moveVector);
        }
    }// END MOVE
    #endregion

    #region JumpInput
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (jumpPressCounter == 1)
            {
                playerMovement.Jump();
                jumpPressCounter++;
            }
            else if(jumpPressCounter == 2)
            {
                jumpPressCounter = 1;
            }
        }
    }// END JUMP
    #endregion

    #region SprintInput
    public void Sprint(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isSprinting = true;
            playerMovement.Sprint(isSprinting);
        }
        if(context.canceled)
        {
            isSprinting = false;
            playerMovement.Sprint(isSprinting);
        }
    }// END SPRINT
    #endregion

    #region CrouchInput
    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isCrouching = true;
            playerMovement.Crouch(isCrouching);
        }
        if(context.canceled)
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
        if(context.started)
        {
            isRolling = true;
            playerMovement.Roll(isRolling);
            cooldownTimer = 0;
        }
        if(context.canceled)
        {
            isRolling = false;
            playerMovement.Roll(isRolling);
        }
    }//END ROLL

    #endregion

    #endregion
}
