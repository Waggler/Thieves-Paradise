using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Current Bugs:
//    - 
//    - 
//    - 

//Things to add:
//    - 
//    - 
//    - 

//Done:
//    - 
//    - 
//    - 

//Suspicion Manager Notes:
//  - Look at Among Us task manager / meter for reference/inspiration on the overall suspicion manager

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
    //Note: The green squigglies just mean it's given a value in the inspector instead of in the script
    [Header("Suspicion Manager Variables")]
    [SerializeField] private Text susText;
    [SerializeField] private float susInc;
    [SerializeField] private float susDec;
    [SerializeField] private float susDecTimer;

    [Header("Notification Radius Variables")]
    [SerializeField] private Transform notifCenter;
    [SerializeField] private float notifRad;


    [Header("Guard Refs")]
    [SerializeField] private EnemyManager enemyManager;


    [Header("Camera Refs")]
    [SerializeField] private CameraManager cameraManager;




    //Be sure to make variables that will be manipulated by other scripts/classes PUBLIC
    [Header("Communicated Variables")]
    [SerializeField] public int testInt;


    #endregion

    #region Debug Variables

    #endregion

    #region Awake & Update

    #region Awake
    //Callled when the object is spawned.
    void Awake()
    {
        Init();

        testInt = 0;

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

        if (testInt != 0)
        {
            //print("Int has changed");

        }

        //print($"{secState}");

        print($"Current references are {enemyManager} & {cameraManager}");




    }//End Update
    #endregion

    #endregion

    #region General Functions

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(notifCenter.position, notifRad);
    }



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
