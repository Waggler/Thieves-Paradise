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
    [HideInInspector] public int Active;

    #endregion

    void Awake()
    {
        PushPullCheck();
    }

    #region Setting the Active Number.
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
                Active = 1;
                break;
        }
    }

    #endregion

}
