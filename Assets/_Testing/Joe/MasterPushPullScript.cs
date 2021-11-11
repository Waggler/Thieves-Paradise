using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPushPullScript : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool NearPushPullObject = false;
    [SerializeField] private int SetCheck;
    [SerializeField] private bool PushPullOn = false;
    [SerializeField] private bool PushPullOff = false;
    [SerializeField] private bool Crouching;
    [SerializeField] private bool Sprinting;
    [SerializeField] private bool IsInteracting = false;
    private int ActivationCheck;
    private PlayerMovement playerMovement;

    #endregion
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull" && !IsInteracting)
        {
            SetCheck = other.gameObject.GetComponent<PushPullObjectScript>().Active;
            IsInteracting = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        Crouching = GetComponent<PlayerMovement>().IsCrouching;
        Sprinting = GetComponent<PlayerMovement>().IsSprinting;
        
        if(other.gameObject.tag == "Push&Pull" && PushPullOn && !Crouching && !Sprinting)
        {
            NearPushPullObject = true;
            ActivationCheck = SetCheck;
            playerMovement.PushPullCheck(NearPushPullObject, ActivationCheck);
            other.gameObject.transform.parent = transform;
        }
        else
        {
            NearPushPullObject = false;
            ActivationCheck = 0;
            playerMovement.PushPullCheck(NearPushPullObject, ActivationCheck);
            other.gameObject.transform.parent = null;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {
            NearPushPullObject = false;
            ActivationCheck = 0;
            playerMovement.PushPullCheck(NearPushPullObject, ActivationCheck);
            other.gameObject.transform.parent = null;
            PushPullOn = false;
            PushPullOff = false;
        }

        if(IsInteracting && !PushPullOn)
        {
            IsInteracting = false;
        }
    }

    public void PushPullInput(bool PushingPulling)
    {
        if(PushingPulling)
        {
            PushPullOn = true;
            if(PushPullOff)
            {
                PushPullOn = false;
                PushPullOff = false;
            }
            else if(!PushPullOff)
            {
                PushPullOff = true;
            }
        }
    }
}
