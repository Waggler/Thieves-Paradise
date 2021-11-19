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
    [Tooltip("This will set the mass for light objects.")]
    [SerializeField] private float LightMass = 50;
    [Tooltip("This will set the mass for medium objects.")]
    [SerializeField] private float MediumMass = 100;
    [Tooltip("This will set the mass for heavy objects.")]
    [SerializeField] private float HeavyMass = 200;
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
                this.GetComponent<Rigidbody>().mass = LightMass;
                break;

            case WeightClasses.MEDIUM:
                Active = 2;
                this.GetComponent<Rigidbody>().mass = MediumMass;
                break;

            case WeightClasses.HEAVY:
                Active = 3;
                this.GetComponent<Rigidbody>().mass = HeavyMass;
                break;

            default:
                Active = 1;
                this.GetComponent<Rigidbody>().mass = LightMass;
                break;
        }
    }

    #endregion

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Push&Pull")
        {
            Active = 3;
        }
    }

    void OnCollisionExit(Collision other)
    {
        PushPullCheck();
    }

}
