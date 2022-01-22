using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Vector3 moveVector;
    public bool isSprinting;
    public bool isCrouching;
    public bool isRolling;
    public bool isPushPull;
    private PlayerMovement playerMovement;
    public float rollCooldownTime;
    private float cooldownTimer;

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
        }
        if(context.canceled)
        {
            moveVector = Vector3.zero;
        }
        playerMovement.Movement(moveVector);
    }// END MOVE
    #endregion

    #region JumpInput
    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerMovement.Jump();
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
