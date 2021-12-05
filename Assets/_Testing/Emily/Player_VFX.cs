using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VFX : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject runParticles;
    [SerializeField] private GameObject slideParticles;

    // Start is called before the first frame update
    void Start()
    {
        runParticles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.IsSprinting == true && playerMovement.Controller.velocity.magnitude > 0) 
        {
            runParticles.SetActive(true);
        }
        else
        {
            runParticles.SetActive(false);
        }
        if (playerMovement.IsSliding == true && playerMovement.Controller.velocity.magnitude > 0)
        {
            slideParticles.SetActive(true);
            runParticles.SetActive(false);
        }
        else
        {
            slideParticles.SetActive(false);
        }
    }
}
