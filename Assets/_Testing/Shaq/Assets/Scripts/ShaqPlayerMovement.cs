using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class ShaqPlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }
}


    ////List of public variables
    //public CharacterController charController;

    //public float speed = 12f;

    //Vector3 velocity;

    //public float gravity;

    //public float jumpHeight;

    //public Transform groundCheck;

    //public float groundDistance = 0.4f;

    //public LayerMask groundMask;

    ////List of private variables
    //private bool isGrounded;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    print("I (THE PLAYER) LIVE");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //checks to see if the player is grounded or not
    //    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    //    //checks to see if the player is on the ground. If the player is not grounded, they will fall.
    //    if (isGrounded && velocity.y < 0)
    //    {
    //        velocity.y = -2f;
    //    }

    //    //getting player input using variables exclusive to the Update function (privatized within the function)
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");

    //    //converts input into a direction the player moves in
    //    Vector3 move = transform.right * x + transform.forward * z;

    //    //telling the player controller to actually move the player at the end of the function
    //    //all of the code above needs to be run through before this can be reached and movement is in fact possible, might do this in fixedUpdate instead of a regular Update function
    //    //move * speed will move the player at a magnitude of whatever the value of the 'speed' variable is at
    //    //Time.deltaTime is added so that the changes in player speed are not dependant on the systems framerate
    //    //controller.Move(move * speed * Time.deltaTime);

    //    //if statement that checks to see if the player is grounded and if they have pressed the "Jump" button
    //    if (Input.GetButtonDown("Jump") && isGrounded)
    //    {
    //        //if the player is grounded and they press the 'jump' button, their velocity on the y vectore will be modified by the equation below
    //        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //    }
    //    else if (Input.GetButtonDown("Jump") && isGrounded != true)
    //    {
    //        print("You cannot jump at the moment");

    //    }

    //    //I honestly don't remember what this does, maybe mess with it and see what breaks?
    //    velocity.y += gravity * Time.deltaTime;

    //    //controller.Move(velocity * Time.deltaTime);

    //    //allows the player to exit the game when the 'escape key is pressed'
    //    if (Input.GetKey("escape"))
    //    {
    //        Application.Quit();
    //    }

    //    //Checking to see if the LeftShift key is down
    //    if (Input.GetKeyDown(KeyCode.LeftShift))
    //    {
    //        //when it is down, player's speed is double to 12
    //        speed = 12f;
    //    }
    //    //Checking to see if the LeftShift key is up
    //    if (Input.GetKeyUp(KeyCode.LeftShift))
    //    {
    //        //when it is up, the player's speed is set to 6
    //        speed = 6f;
    //    }
