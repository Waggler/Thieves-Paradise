using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCamera : MonoBehaviour
{
    [SerializeField] private float camSpeed;
    [SerializeField] private Vector3 rotationMax;

    //-----------------------//
    private void Update()
    //-----------------------//
    {
        MoveCam();

    }//END Update

    //-----------------------//
    private void MoveCam()
    //-----------------------//
    {
        transform.Rotate(new Vector3(0, camSpeed, 0) * Time.deltaTime, Space.Self);

        if (transform.localRotation.eulerAngles.y >= rotationMax.y)
        {
            //Inverts the camera's turn speed
            camSpeed = -camSpeed;
        }

    }//END MoveCam

}//END FakeCamera
