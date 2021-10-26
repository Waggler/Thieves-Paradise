using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Have prototype by Friday

public class SuspicionManager : MonoBehaviour
{
    #region Enumerations
    private enum SecurityLvl
    {
        SecLVL0,
        SecLVL1,
        SecLVL2,
        SecLVL3,
        SecLVL4
    }

    [Header("AI State")]

    [SerializeField] SecurityLvl secState;
    #endregion

    #region Coroutines

    #endregion

    #region Lists & Arrays

    #region Guard List
    [Header("AI Lists")]
    [SerializeField] private List<GameObject> guardsInLevel;
    //Try to generate the list by getting the children of an object


    #endregion

    #region Camera List
    [SerializeField] private List<GameObject> camerasInLevel;
    //Try to generate the list by getting the children of an object

    
    #endregion


    #endregion

    #region Inspector Variables
    [Header("Suspicion Manager Variables")]
    [SerializeField] private Text susText;
    [SerializeField] private float susInc;
    [SerializeField] private float susDec;
    [SerializeField] private float susDecTimer;


    #endregion

    #region Debug Variables

    #endregion

    #region Awake & Update

    #region Awake
    //Callled when the object is spawned.
    void Awake()
    {
        Init();

    }//End Awake
    #endregion

    #region Update
    // Update is called once per frame
    void Update()
    {
        switch (secState)
        {
            case SecurityLvl.SecLVL0:
                susText.text = "Level 0";
                break;

            case SecurityLvl.SecLVL1:
                susText.text = "Level 1";
                break;

            case SecurityLvl.SecLVL2:
                susText.text = "Level 2";
                break;

            case SecurityLvl.SecLVL3:
                susText.text = "Level 3";
                break;

            case SecurityLvl.SecLVL4:
                susText.text = "Level 4";
                break;

            default:
                susText.text = "LEVEL NOT FOUND";
                break;
        }
    }//End Update
    #endregion

    #endregion

    #region General Functions

    //---------------------------------//
    //Used on the Awake() function to initialize any values in one line
    private void Init()
    {
        secState = SecurityLvl.SecLVL0;
    }

    //---------------------------------//
    //Raises / Lowers the security level based on the given context
    private void AdjustSecurityLevel(SecurityLvl securityLvl)
    {

    }

    #endregion
}
