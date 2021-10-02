using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Vector3 moveVector;
    bool isSprinting;
    bool isCrouching;
    private PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 contextValue = context.ReadValue<Vector2>();

        if(context.performed)
        {
            moveVector = new Vector3(contextValue.x, 0, contextValue.y);
        }
        if(context.canceled)
        {
            moveVector = Vector3.zero;
        }
        playerMovement.Movement(moveVector);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context. performed)
        {
            playerMovement.CallJumpFunctionHere();
        }
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isSprinting = true;
            playerMovement.Sprint(isSprinting);
        }
        if(context.canceled)
        {
            isSprinting = false;
            playerMovement.Sprint(isSprinting);
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isCrouching = true;
            playerMovement.Crouch(isCrouching);
        }
        if(context.canceled)
        {
            isCrouching = false;
            playerMovement.Crouch(isCrouching);
        }
    }

}
