using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PushPullObjectScript : MonoBehaviour
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

    #endregion

    void Awake()
    {

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
                break;

            case WeightClasses.MEDIUM:
                Active = 2;
                break;

            case WeightClasses.HEAVY:
                Active = 3;
                break;

            default:
                Weight = WeightClasses.LIGHT;
                break;
        }
    }

    #endregion

    #region Receving Output
    public void ObjectMove(Vector3 DeltaMove)
    {
        if(DeltaMove != Vector3.zero)
        {
            RB.MovePosition(DeltaMove);
        }  
    }

    #endregion
}
