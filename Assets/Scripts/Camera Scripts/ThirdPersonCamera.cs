using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public float RotationSpeed = 1;
    public Transform Target, Player;
    private float mouseX, mouseY;
    public InputManager im;
    public DoorOpen door;
    public DoorOpen door2;
    public DoorOpen lockedDoor1;
    public DoorOpen lockedDoor2;

    //Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //DELETE ME!
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

        
        if(im.isCrouching == true && door.inArea == true || im.isCrouching == true && door2.inArea == true)
        {
            Player.rotation = Quaternion.Euler(0, -mouseX, 0);
        }

        if(im.isCrouching == true && lockedDoor1.inArea == true || im.isCrouching == true && lockedDoor2.inArea == true)
        {
            Player.rotation = Quaternion.Euler(0, -mouseX, 0);
        }
        
        // Player.rotation = Quaternion.Euler(0, mouseX, 0);
   }
}
