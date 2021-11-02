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
    public InputManager im;

    public DoorOpen trigger1;
    public DoorOpen2 trigger2;

    public Vents vent;

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
        if(im.isCrouching == true && trigger1.inArea == true)
        {
           animator.Play("FreeLook");
        }
        if(im.isCrouching == true && trigger2.inArea == true)
        {
           animator.Play("FreeLook");
        }
        if(im.isCrouching == false && trigger1.inArea == true)
        {
            animator.Play("VCam1");
        }
        if(im.isCrouching == false && trigger1.inArea == false)
        {
            animator.Play("VCam1");
        }
        if(im.isCrouching == false && trigger2.inArea == true)
        {
            animator.Play("VCam1");
        }
        if(im.isCrouching == false && trigger2.inArea == false)
        {
            animator.Play("VCam1");
        }
    }
}
