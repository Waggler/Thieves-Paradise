using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;

    [Header("Debug Values")]
    public bool isWalking;
    public bool isSprinting;
    public bool isCrouching;
    public bool isRolling;

    private void Awake()
    {
        Init();

    }// END Awake

    // TO DO Methods should be called by PlayerMovement Through PlayerController
    private void Update()
    {
        DebugAnim();

    }// END Update

    private void Init()
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponent<Animator>();
        }

    }// END Init


    #region DebugMethods

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
    public void IsPlayerJumping(bool isPlayerRolling)
    //-------------------------------------------------//
    {
    }// END IsPlayerJumping

    public void IsPlayerIdle(bool isPlayerRolling)
    //-------------------------------------------------//
    {
    }// END IsPlayerIdle

    public void IsPlayerCrouchIdle(bool isPlayerCrouchIdle)
    //-------------------------------------------------//
    {
        if (isPlayerCrouchIdle)
        {
            playerAnimator.SetBool("isCrouchIdle", true);
        }
        else
        {
            playerAnimator.SetBool("isCrouchIdle", false);
        }

    }// END IsPlayerCrouchIdle

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

    public void IsPlayerDiving(bool isPlayerDiving)
    //-------------------------------------------------//
    {
        if (isPlayerDiving)
        {
            playerAnimator.SetBool("isDiving", true);
        }
        else
        {
            playerAnimator.SetBool("isDviving", false);
        }
    }// END IsPlayerDiving

    public void IsPlayerStunned(bool isPlayerStunned)
    //-------------------------------------------------//
    {
        if (isPlayerStunned)
        {
            playerAnimator.SetBool("isStunned", true);
        }
        else
        {
            playerAnimator.SetBool("isStunned", false);
        }
    }// END IsPlayerStunned

    //-------------------------------------------------//
    private void DebugAnim()
    //-------------------------------------------------//
    {
        if (isWalking == true)
        {
            IsPlayerWalking(true);
        }
        else
        {
            IsPlayerWalking(false);
        }

        if (isCrouching == true)
        {
            IsPlayerCrouching(true);
        }
        else
        {
            IsPlayerCrouching(false);
        }

        if (isSprinting == true)
        {
            IsPlayerSprinting(true);
        }
        else
        {
            IsPlayerSprinting(false);
        }

        if (isRolling == true)
        {
            IsPlayerRolling(true);
        }
        else
        {
            IsPlayerRolling(false);
        }

    }// END DebugAnim


    #endregion // END DebugMethods


}// END Class
