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
    public bool firstPersonCam = false;

    public PlayerMovement pm;
    public InputManager im;

    public DoorOpen trigger1;
    public DoorOpen2 trigger2;

    public Vents vent;

    public VentInTrigger ventIn;
    public VentOutTrigger ventOut;

    // public Camera mainCamera;
    // public Camera Camera2;

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
        // Camera2.enabled = false;
        // mainCamera.enabled = true;
    }

    private void SwitchState()
    {
            if(Cam1 == false)
            {
                animator.Play("FreeLook");
                firstPersonCam = false;
            }
            else
            {
                animator.Play("VCam1");
                firstPersonCam = false;
            }
            Cam1 = !Cam1;
    }

    // private void CloseState()
    // {
    //     animator.Play("VCam2");
    //     mainCamera.enabled = false;
    //     Camera2.enabled = true;
    // }

    // private void MainState()
    // {
    //     mainCamera.enabled = true;
    //     Camera2.enabled = false;
    // }

    // Update is called once per frame
    void Update()
    {
        if(im.isCrouching == true && trigger1.inArea == true)
        {
           animator.Play("VCam2");
           firstPersonCam = true;
        }
        if(im.isCrouching == true && trigger2.inArea == true)
        {
           animator.Play("VCam2");
           firstPersonCam = true;
        }

        if(ventIn.inVent == true)
        {
            animator.Play("VCam2");
            firstPersonCam = true;
        }
        if(ventOut.outVent == true)
        {
            animator.Play("FreeLook");
            firstPersonCam = false;
        }
        // if(im.isCrouching == true && vent.isOnVents == true)
        // {
        //    animator.Play("VCam2");
        //    firstPersonCam = true;
        // }
        // if(vent.isOnVents == false)
        // {
        //    firstPersonCam = false;
        // }
        // if(im.isCrouching == false && trigger1.inArea == true)
        // {
        //     firstPersonCam = false;
        // }
        // if(im.isCrouching == true && trigger1.inArea == false)
        // {
        //     firstPersonCam = false;
        // }
        // if(im.isCrouching == false && trigger2.inArea == true)
        // {
        //     firstPersonCam = false;
        // }
        // if(im.isCrouching == true && trigger2.inArea == false)
        // {
        //     firstPersonCam = false;
        // }
    }
}
