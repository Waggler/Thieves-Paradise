using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    NOTE: I got a lot of errors, must be because I just initalized for this area. Check to see if I am doing this right tomorrow, I wish to try and make a player controller with Patrick's help later tomorrow.
*/

// public class ThirdPersonMovement : MonoBehaviour
// {
//     [Header("Speed")]
//     [SerializedField] private float RunningSpeed;
//     [SerializedField] private float Speed;
//     [SerializedField] private float CrouchingSpeed;
//     private float CurrentSpeed;
    
//     [Header("Physics")]
//     [SerializedField] private Rigidbody rigidbody3D;
//     [SerializedField] private bool isOnGround = true;

//     //[SerializedField] private CapsuleCollider Capsule;

//     void Start()
//     {
//         rigidbody3D = GetComponent<Rigidbody>();
//         CurrentSpeed = Speed;
//     }

//     void Update()
//     {
//         PlayerMovement();
//     }

//     void PlayerMovement()
//     {
//         #region Move
//         float hor = Input.GetAxis("Horizontal");
//         float ver = Input.GetAxis("Vertical");
//         Vector3 playerMovement = new Vector3(hor, 0f, ver) * CurrentSpeed * Time.deltaTime;
//         transform.Translate(playerMovement, Space.Self);
//         #endregion

//         #region Jump
//         //------------------JUMP----------------------//
//         if(Input.GetButtonDown("Jump") && isOnGround == true)
//         {
//             rigidbody3D.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
//             isOnGround = false;
//         }
//         #endregion

//         #region Sprint
//         //------------------SPRINT--------------------//
//         if(Input.GetButtonDown("Sprint"))
//         {
//             if(CurrentSpeed < RunningSpeed && Input.GetButtonDown("W"))
//             {
//                 CurrentSpeed += 0.25;
//             }
//             else if(CurrentSpeed >= RunningSpeed && Input.GetButtonDown("W"))
//             {
//                 CurrentSpeed = RunningSpeed;
//             }
//         }
//         else if(Input.GetButtonUp("Sprint"))
//         {
//             CurrentSpeed = Speed;
//         }
//         #endregion

//         #region Crouch
//         //-------------------CROUCH----------------------//
//         if(Input.GetButtonDown("Crouch"))
//         {
//             rigidbody3D.height = 1f;
//             CurrentSpeed = CrouchingSpeed;
//         }
//         else if(Input.GetButtonUp("Crouch"))
//         {
//             CurrentSpeed = Speed;
//             rigidbody3D.height = 2f;
//         }
//         #endregion
//     }

//     private void OnCollisionEnter(Collision collision)
//     {
//         if(collision.gameObject.name == "Ground")
//         {
//             isOnGround = true;
//         }
//     }
// }
