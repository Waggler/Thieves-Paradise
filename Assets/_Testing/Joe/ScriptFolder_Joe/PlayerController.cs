using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private CamSwitch camSwitch;
    [SerializeField] private AnimationController animationController;
}
