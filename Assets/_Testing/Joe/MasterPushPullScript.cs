using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPushPullScript : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool NearPushPullObject = false;
    //TestVar for now.
    [SerializeField] private int SetCheck;
    private int ActivationCheck;
    private PlayerMovement playerMovement;
    /*
        bool for checking if crouching, running, or jumping. Call the player movement most likly for that. Call that from the player?
        Now how do I have the other script come here and to the player?
    */

    #endregion
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {
            NearPushPullObject = true;
            ActivationCheck = SetCheck;
            playerMovement.PushPullCheck(NearPushPullObject, ActivationCheck);
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
}
