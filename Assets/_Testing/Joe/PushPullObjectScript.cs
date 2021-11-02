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
    private MasterPushPullScript masterPushPullScript;

    #endregion

    void Awake()
    {
        masterPushPullScript.GetComponent<MasterPushPullScript>();
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
                //SetValue(Active);
                break;

            case WeightClasses.MEDIUM:
                Active = 2;
                //SetValue(Active);
                break;

            case WeightClasses.HEAVY:
                Active = 3;
                //SetValue(Active);
                break;

            default:
                Weight = WeightClasses.LIGHT;
                break;
        }
    }

    #endregion

}
