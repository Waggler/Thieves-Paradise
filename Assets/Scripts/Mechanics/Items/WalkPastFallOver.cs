using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkPastFallOver : MonoBehaviour
{
    public PlayerMovement pm;
    public InputManager im;
    public GameObject fallingObject;
    public float Timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay()
    {
        if(pm.IsCrouching == true || im.isCrouching == true)
        {
            Debug.Log("Walked Past!");
        }

        if(pm.IsCrouching == false || im.isCrouching == false)
        {
            Debug.Log("Sound Made!");
            float minAngle = 0f;
            float maxAngle = -90f;
            Timer = Timer + Time.deltaTime;
            float angle = Mathf.LerpAngle(minAngle, maxAngle, Timer);
            fallingObject.transform.eulerAngles = new Vector3(angle, 0, 0);
        }
    }
}
