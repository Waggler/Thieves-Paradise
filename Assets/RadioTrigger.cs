using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] RadioDialogueManager radioManager;


    private void OnTriggerEnter(Collider other) //Use this for the player walking into a collider
    {
        radioManager.Init();
    }

}//END RadioTrigger
