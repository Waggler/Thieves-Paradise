using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPushPullScript : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool NearPushPullObject = false;
    private int SetCheck;
    private int ActivationCheck;
    private PlayerMovement playerMovement;

    #endregion
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PushPullObjectScript>();
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {
            NearPushPullObject = true;
            ActivationCheck = SetCheck;
            playerMovement.PushPullCheck(NearPushPullObject, ActivationCheck);
            other.gameObject.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {
            NearPushPullObject = false;
            ActivationCheck = 0;
            playerMovement.PushPullCheck(NearPushPullObject, ActivationCheck);
        }
    }
    public void SetValue(int Weight)
    {
        SetCheck = Weight;
    }
}
