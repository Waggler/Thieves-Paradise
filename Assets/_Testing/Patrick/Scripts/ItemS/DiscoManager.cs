using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoManager : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float rotateSpeed = 20;
    private bool isHanging = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        //rotate the ball
        if (isHanging) //only while it's hanging tho
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime, Space.World);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            isHanging = false;

            transform.gameObject.tag = "Item";
        }
    }
}
