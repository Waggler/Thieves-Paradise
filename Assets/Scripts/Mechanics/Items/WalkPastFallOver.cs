using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPastFallOver : MonoBehaviour
{
    public PlayerMovement pm;
    public InputManager im;
    public GameObject fallingObject;
    public float Timer = 0f;
    public bool inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange == true)
        {
            float minAngle = 0f;
            float maxAngle = -90f;
            Timer = Timer + Time.deltaTime;
            // float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
            // fallingObject.transform.eulerAngles = new Vector3(angle, 0, 0);

            if(Timer <= 1.05f)
            {
                float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
                fallingObject.transform.eulerAngles = new Vector3(angle, 0, 0);
            }

            if(Timer >= 1.05f)
            {
                Timer = 1.05f;
            }
        }
    }

    public void OnTriggerStay()
    {
        //pm.IsCrouching == true || im.isCrouching == true || 
        if(pm.IdleCrouch == true || pm.Crouching == true)
        {
            Debug.Log("Walked Past!");
            inRange = false;
        }

        if(pm.IsCrouching == false || im.isCrouching == false || pm.IdleCrouch == false || pm.Crouching == false)
        {
            inRange = true;
            Debug.Log("Sound Made!");
            // float minAngle = 0f;
            // float maxAngle = -90f;
            // Timer = Timer + Time.deltaTime;
            // // float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
            // // fallingObject.transform.eulerAngles = new Vector3(angle, 0, 0);

            // if(Timer <= 1.05f)
            // {
            //     float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
            //     fallingObject.transform.eulerAngles = new Vector3(angle, 0, 0);
            // }

            // if(Timer >= 1.05f)
            // {
            //     Timer = 1.05f;
            // }
        }
    }

    public void OnTriggerEnter()
    {
        //pm.IsCrouching == true || im.isCrouching == true || 
        if(pm.IdleCrouch == true || pm.Crouching == true)
        {
            Debug.Log("Walked Past!");
            inRange = false;
        }

        if(pm.IsCrouching == false || im.isCrouching == false || pm.IdleCrouch == false || pm.Crouching == false)
        {
            inRange = true;
            Debug.Log("Sound Made!");
            // float minAngle = 0f;
            // float maxAngle = -90f;
            // Timer = Timer + Time.deltaTime;
            // // float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
            // // fallingObject.transform.eulerAngles = new Vector3(angle, 0, 0);

            // if(Timer <= 1.05f)
            // {
            //     float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
            //     fallingObject.transform.eulerAngles = new Vector3(angle, 0, 0);
            // }

            // if(Timer >= 1.05f)
            // {
            //     Timer = 1.05f;
            // }
        }
    }
}
