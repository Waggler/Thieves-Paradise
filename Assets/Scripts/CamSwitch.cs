using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamSwitch : MonoBehaviour
{
    [SerializeField]

    private InputAction action;

    private Animator animator;

    public bool Cam1 = true;

    public PlayerMovement pm;

    public DoorOpen door;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        action.performed += _ => SwitchState();
    }

    private void SwitchState()
    {
            if(Cam1 == false)
            {
                animator.Play("FreeLook");
            }
            else
            {
                animator.Play("VCam1");
            }
            Cam1 = !Cam1;
    }

    // Update is called once per frame
    void Update()
    {
        //if(pm.Crouching == true && door.inArea == true)
        //{
        //    animator.Play("FreeLook");
        //}
        // if(im.isCrouching == false && door.inArea == true)
        // {
        //     animator.Play("VCam1");
        // }
        // if(im.isCrouching == false && door.inArea == false)
        // {
        //     animator.Play("VCam1");
        // }
    }
}
