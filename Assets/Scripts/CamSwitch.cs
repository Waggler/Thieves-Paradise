using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamSwitch : MonoBehaviour
{
    [SerializeField]

    private InputAction action;

    private Animator animator;

    private bool Cam1 = true;

    public InputManager im;

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
        if(Cam1 && im.isCrouching == false)
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
        if(im.isCrouching == true)
        {
            animator.Play("FreeLook");
        }
        if(im.isCrouching == false)
        {
            animator.Play("VCam1");
        }
    }
}
