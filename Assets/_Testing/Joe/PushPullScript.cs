using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushPullScript : MonoBehaviour
{
    #region Variables
    enum WeightClasses
    {
        LIGHT,
        MEDIUM,
        HEAVY
    }
    [Header("Push and Pull Variables.")]
    [Tooltip("This is for the weight of the object. Write either Light, Medium, or Heavy.")]
    [SerializeField] private WeightClasses Weight;
    [Tooltip("The Rigidboy of the object.")]
    [SerializeField] private Rigidbody RB;
    [SerializeField] private int Active;
    private PlayerMovement playerMovement;

    #endregion

    void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        PushPullCheck();
    }

    #region Giving Input
    public void PushPullCheck()
    {
        switch(Weight)
        {
            case WeightClasses.LIGHT:
                Active = 1;
                playerMovement.PushPull(Active);
                break;

            case WeightClasses.MEDIUM:
                Active = 2;
                playerMovement.PushPull(Active);
                break;

            case WeightClasses.HEAVY:
                Active = 3;
                playerMovement.PushPull(Active);
                break;

            default:
                Weight = WeightClasses.LIGHT;
                break;
        }
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
