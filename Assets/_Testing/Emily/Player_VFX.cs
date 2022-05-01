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
    
    private float runLifeTimeRecord;
    private float slideLifeTimeRecord;

    // Start is called before the first frame update
    void Awake()
    {
        Init();
    }

    [System.Obsolete]

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.CurrentSpeed > playerMovement.WalkingSpeed)
        {
            if (playerMovement.IsSprinting == true /*&& playerMovement.Controller.velocity.magnitude > 0*/)
            {
                runParticleSys.startLifetime = runLifeTimeRecord;
            }
            else
            {
                runParticleSys.startLifetime = 0f;
            }

            if (playerMovement.IsSliding == true)
            {
                slideParticleSys.startLifetime = slideLifeTimeRecord;
                runParticleSys.startLifetime = 0f;
            }
            else
            {
                slideParticleSys.startLifetime = 0f;
            }
        }
        else
        {
            runParticleSys.startLifetime = 0f;
            slideParticleSys.startLifetime = 0f;
        }
    }

    [System.Obsolete]

    private void Init()
    {
        runParticleSys = runParticles.GetComponent<ParticleSystem>();
        slideParticleSys = slideParticles.GetComponent<ParticleSystem>();

        runLifeTimeRecord = runParticleSys.startLifetime;
        slideLifeTimeRecord = slideParticleSys.startLifetime;
    }
}

