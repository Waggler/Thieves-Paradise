using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player;
    float mouseX, mouseY;
    public PlayerMovement pm;
    public DoorOpen trigger1;
    public DoorOpen2 trigger2;
    public CamSwitch cam;

    //Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void LateUpdate()
    {
        CamControl();
    }

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * RotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * RotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(Target);

        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        
        if(cam.firstPersonCam == true)
        {
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
        // if(pm.Crouching == true && trigger2.inArea == true)
        // {
        //     Player.rotation = Quaternion.Euler(0, mouseX, 0);
        // }

        // Player.rotation = Quaternion.Euler(0, mouseX, 0);
        //pm.Crouching == true && trigger1.inArea == true
   }
}
