using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPushPullScript : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool NearPushPullObject = false;
    /*
        bool for checking if crouching, running, or jumping. Call the player movement most likly for that. Call that from the player?
        Now how do I have the other script come here and to the player?
    */

    #endregion

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {
            print("IN!");
            NearPushPullObject = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {
            print("OUT!");
            NearPushPullObject = false;
        }
    }
}
