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
        if(Cam1)
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
        
    }
}
