using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
    NOTE: I got a lot of errors, must be because I just initalized for this area. Check to see if I am doing this right tomorrow, I wish to try and make a player controller with Patrick's help later tomorrow.
*/

public class ThirdPersonMovement1st : MonoBehaviour
{
    #region Variables
    [Header("Speed")]
    [SerializeField] private float RunningSpeed;
    [SerializeField] private float WalkingSpeed;
    [SerializeField] private float CrouchingSpeed;
    [SerializeField] private float AccelerationSpeed;
    private float CurrentSpeed;
    
    [Header("Physics")]
    [SerializeField] private float Gravity;
    [SerializeField] private float JumpHeight;
    private CharacterController controller;
    private Vector3 MoveDirection;
    private bool IsOnGround;

    //[Header("Crouch")]

    //[Header("Input")]
    private PlayerActionControls playerActionControls;

    //[SerializedField] private CapsuleCollider Capsule;
    #endregion

    void Awake()
    {
        playerActionControls = new PlayerActionControls();
    }

    void OnEnable()
    {
        playerActionControls.Enable();
    }

    void OnDisable()
    {
        playerActionControls.Disable();
    }

    void Start()
    {
        CurrentSpeed = WalkingSpeed;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        #region TBC
        #region Move
        //------------------MOVE----------------------//
        float PlayerMovement = playerActionControls.PlayerControls.Movement.ReadValue<float>();

        

        controller.Move(MoveDirection);

        #endregion

        // #region Jump
        // //------------------JUMP----------------------//
        // //--Change the get button down to new controls--//
        // if( && controller.isGrounded)
        // {
        //     playerVertical.y += Mathf.Sqrt(JumpHeight * -3.0f * Gravity);
        // }
        // #endregion

        // #region Sprint
        // //------------------SPRINT--------------------//
        // //--Change the get button down to new controls--//
        // if(Input.GetButtonDown("Sprint"))
        // {
        //     if(CurrentSpeed < RunningSpeed && Input.GetButtonDown("W"))
        //     {
        //         CurrentSpeed += AccelerationSpeed * Time.deltaTime;
        //     }
        //     else if(CurrentSpeed >= RunningSpeed && Input.GetButtonDown("W"))
        //     {
        //         CurrentSpeed = RunningSpeed;
        //     }
        // }
        // else if(Input.GetButtonUp("Sprint"))
        // {
        //     CurrentSpeed = WalkingSpeed;
        // }
        // #endregion

        // #region Crouch
        // //-------------------CROUCH----------------------//
        // //--Change the get button down to new controls--//
        // if(Input.GetButtonDown("Crouch"))
        // {
        //     CurrentSpeed = CrouchingSpeed;
        // }
        // else if(Input.GetButtonUp("Crouch"))
        // {
        //     CurrentSpeed = WalkingSpeed;
        // }
        // #endregion
        #endregion
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
