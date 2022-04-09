using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VFX : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject runParticles;
    [SerializeField] private GameObject slideParticles;

    private ParticleSystem runParticleSys;
    private ParticleSystem slideParticleSys;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
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

        if (playerMovement.IsSliding == true)
        {
            slideParticles.SetActive(true);
            runParticles.SetActive(false);
        }
        else
        {
            slideParticles.SetActive(false);
        }
    }

    private void Init()
    {
        runParticles.SetActive(false);
        slideParticles.SetActive(false);

        runParticleSys = runParticles.GetComponent<ParticleSystem>();
        slideParticleSys = slideParticles.GetComponent<ParticleSystem>();
    }
}

