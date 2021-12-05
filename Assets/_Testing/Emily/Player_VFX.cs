using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_VFX : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private GameObject particles;

    // Start is called before the first frame update
    void Start()
    {
        particles.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.IsSprinting == true)
        {
            particles.SetActive(true);
        }
        else
        {
            particles.SetActive(false);
        }
    }
}
