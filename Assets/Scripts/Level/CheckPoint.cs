using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() == true)
        {
            GameController.gameControllerInstance.SaveCheckPoint();
        }
        Destroy(this);
    }
}
