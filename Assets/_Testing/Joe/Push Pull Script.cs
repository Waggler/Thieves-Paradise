using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPullScript : MonoBehaviour
{
    #region Variables
    [Header("Push and Pull Variables.")]
    [Tooltip("This is for the weight of the object. Write either Light, Medium, or Heavy.")]
    [SerializeField] private string Weight;
    [Tooltip("Check this off if you have written in the box above.")]
    [SerializeField] private bool IsWeighted;
    private int Active;
    private PlayerMovement playerMovement;

    #endregion

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    #region Giving Input
    public void PushPullCheck()
    {
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
        if(Moving)
        {
            Rigidboy.Move(Direction * Time.deltaTime * WeightSpeed);
        }
        else
        {
            Rigidboy.Move(Stay Still);
        }
        */
    }

    #endregion
}
