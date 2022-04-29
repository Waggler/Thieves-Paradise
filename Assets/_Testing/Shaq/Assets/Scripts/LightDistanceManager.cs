using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDistanceManager : MonoBehaviour
{
    [Tooltip("In case the player spawns within the collider for the light, thus being unable to trigger it to turn on")]
    [SerializeField] private bool isStartLit;

    [Tooltip("Player reference, automatically generated")]
    [SerializeField] private GameObject player;

    [Tooltip("References the light that this script is attatched to, please fill in")]
    [SerializeField] private Light lightRef;

    //References the cube collider attatched to the light
    [SerializeField] private Collider cubeColliderRef;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    //Entering the collider / turning ON the light
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            lightRef.enabled = true;
        }
    }


    //Exiting the collider  / turning OFF the light
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            lightRef.enabled = false;
        }
    }

    private void Init()
    {
        if (lightRef == null)
        {
            lightRef = GetComponent<Light>();
        }

        //Auto references player object in scene
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        //Auto generates reference to cube collider of light
        if (cubeColliderRef == null)
        {
            cubeColliderRef = GetComponent<BoxCollider>();
        }

        cubeColliderRef.isTrigger = true;

        if (isStartLit == true)
        {
            lightRef.enabled = true;
        }
        else
        {
            lightRef.enabled = false;
        }
    }
}
