using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerActionControls controls;
    PlayerActionControls.InputActions playerControls;

    Vector2 HoziontalInput;

    void Awake()
    {
        controls = new PlayerActionControls();
        playerControls = controls.InputActions;

        playerControls.Movement.preformed += context => HoziontalInput = context.ReadValue<Vector2>();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

}
