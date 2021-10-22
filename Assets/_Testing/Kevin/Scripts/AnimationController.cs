using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    private void Awake()
    {
        Init();

    }// END Awake

    // // TO DO Methods should be called by PlayerMovement Through PlayerController
    // private void Update()
    // {
    //     DebugAnim();

    // }// END Update

    private void Init()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }

    }// END Init


    #region DebugMethods

    //-------------------------------------------------//
    public void IsPlayerIdle(bool isPlayerIdle)
    //-------------------------------------------------//
    {
        if (isPlayerIdle == true)
        {
            playerAnimator.SetBool("isIdle", true);
        }
        else
        {
            playerAnimator.SetBool("isIdle", false);
        }

    }// END IsPlayerIdle

    //-------------------------------------------------//
    public void IsPlayerCrouchIdle(bool isPlayerCrouchIdle)
    //-------------------------------------------------//
    {
        if (isPlayerCrouchIdle == true)
        {
            playerAnimator.SetBool("isCrouchIdle", true);
        }
        else
        {
            playerAnimator.SetBool("isCrouchIdle", false);
        }

    }// END IsPlayerCrouchIdle

    //-------------------------------------------------//
    public void IsPlayerWalking(bool isPlayerWalking)
    //-------------------------------------------------//
    {
        if (isPlayerWalking == true)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }

    }// END IsPlayerWalking

    //-------------------------------------------------//
    public void IsPlayerSprinting(bool isPlayerSprinting)
    //-------------------------------------------------//
    {
        if (isPlayerSprinting == true)
        {
            playerAnimator.SetBool("isSprinting", true);
        }
        else
        {
            playerAnimator.SetBool("isSprinting", false);
        }

    }// END IsPlayerSprinting

    //-------------------------------------------------//
    public void IsPlayerCrouching(bool isPlayerCrouching)
    //-------------------------------------------------//
    {
        if (isPlayerCrouching == true)
        {
            playerAnimator.SetBool("isCrouching", true);
        }
        else
        {
            playerAnimator.SetBool("isCrouching", false);
        }

    }// END IsPlayerCrouching

    //-------------------------------------------------//
    public void IsPlayerJumping(bool IsPlayerJumping)
    //-------------------------------------------------//
    {
        if (IsPlayerJumping == true)
        {
            playerAnimator.SetBool("isJumping", true);
        }
        else
        {
            playerAnimator.SetBool("isJumping", false);
        }

    }// END IsPlayerJumping

    //-------------------------------------------------//
    public void IsPlayerRolling(bool isPlayerRolling)
    //-------------------------------------------------//
    {
        if (isPlayerRolling)
        {
            playerAnimator.SetBool("isRolling", true);
        }
        else
        {
            playerAnimator.SetBool("isRolling", false);
        }

    }// END IsPlayerRolling

    //-------------------------------------------------//
    public void IsPlayerSliding(bool isPlayerSliding)
    //-------------------------------------------------//
    {
        if (isPlayerSliding)
        {
            playerAnimator.SetBool("isSliding", true);
        }
        else
        {
            playerAnimator.SetBool("isSliding", false);
        }

    }// END IsPlayerSliding

    //-------------------------------------------------//
    public void IsPlayerDiving(bool isPlayerDiving)
    //-------------------------------------------------//
    {
        if (isPlayerDiving)
        {
            playerAnimator.SetBool("isDiving", true);
        }
        else
        {
            playerAnimator.SetBool("isDiving", false);
        }

    }// END IsPlayerDiving

    #endregion // END DebugMethods

}// END Class
