using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterPushPullScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {

        }
    }
}
