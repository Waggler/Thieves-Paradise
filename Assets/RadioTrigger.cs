using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTrigger : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] RadioDialogueManager radioManager;
    [SerializeField] private int radioDialogueIndex;
    public bool isDestroyedAfterEntry;

    private void OnTriggerEnter(Collider other) //Use this for the player walking into a collider
    {
        radioManager.currentDialogueIndex = radioDialogueIndex;
        radioManager.Init();

        if (isDestroyedAfterEntry == true)
        {
            Destroy(this.gameObject);

        }
    }

}//END RadioTrigger
