using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public float Speed;
    Rigidbody rigidbody3D;
    public bool isOnGround = true;
    public float distance = 5f;
    public GameObject Camera;
    public GameObject door;
    public bool buttonPressed= false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody3D = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * Speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);
        // transform.position = transform.position + Camera.transform.forward * distance * Time.deltaTime;
    }

    void FixedUpdate()
    {
        if(Input.GetButtonDown("Jump") && isOnGround == true)
        {
            rigidbody3D.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Ground")
        {
            isOnGround = true;
        }
    }
}
