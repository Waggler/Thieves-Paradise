using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushPullScript : MonoBehaviour
{
    #region Variables
    [Header("Push and Pull Variables.")]
    [Tooltip("This is for the weight of the object. Write either Light, Medium, or Heavy.")]
    [SerializeField] private string Weight;
    [Tooltip("Check this off if you have written in the box above.")]
    [SerializeField] private bool IsWeighted;
    [Tooltip("The Rigidboy of the object.")]
    [SerializeField] private Rigidbody RB;
    [SerializeField] private int Active;
    private PlayerMovement playerMovement;

    #endregion

    void Awake()
    {
        Weight = Weight.ToUpper();
        if(Weight == "LIGHT" || Weight == "MEDIUM" || Weight == "Heavy")
        {
            IsWeighted = true;
        }
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {

    }

    #region Giving Input
    public void PushPullCheck()
    {
        if(IsWeighted)
        {
            if(Weight == "LIGHT")
            {
                Active = 1;
                playerMovement.PushPull(Active);
            }
        }
        /*
        if(Context.preformed && IsWeighted)
        {
            if(Weight == "LIGHT")
            {
                Active = 1;
            }
            else if(Weight == "MEDIUM")
            {
                Active = 2;
            }
            else if(Weight == "HEAVY")
            {
                Active = 3;
            }
        }
        if(Context.canceled && IsWeighted)
        {
            if(Weight == "LIGHT")
            {
                Active = 0;
            }
            else if(Weight == "MEDIUM")
            {
                Active = 0;
            }
            else if(Weight == "HEAVY")
            {
                Active = 0;
            }
        }
        */
    }

    #endregion

    #region Receving Output
    public void ObjectMove()
    {
        /*
        if(Movement != Vector3.zero)
        {
            RB.MovePosition(Movement * Time.deltaTime * WeightSpeed);
        }


        if(Moving)
        {
            Rigidboy.MovePosition(Direction * Time.deltaTime * WeightSpeed);
        }
        else
        {
            Rigidboy.MovePosition(Stay Still);
        }
        */
    }

    #endregion
}
