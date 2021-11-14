using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//fix issues caused by having the class be static

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
    #endregion Enumerations

    #region Coroutines

    #endregion Coroutines

    #region Lists & Arrays

    #region Guard List
    [Header("Guards List")]
    [Tooltip("Shows the list of guards")]
    [SerializeField] private List<GameObject> guardsList;
    //Try to generate the list by getting the children of an object


    #endregion

    #region Camera List
    [SerializeField] private List<GameObject> camerasInLevel;
    //Try to generate the list by getting the children of an object


    #endregion

    #endregion Lists & Arrays

    #region Variables
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

    [Header("Debug / Testing Variables")]


    //Be sure to make variables that will be manipulated by other scripts/classes PUBLIC
    [Header("Communicated Variables")]
    [SerializeField] public int testInt;


    #endregion Variables

    #region Awake & Update

    #region Awake
    //Callled when the object is spawned.
    void Awake()
    {
        Init();

        testInt = 0;

    }//End Awake
    #endregion Awake

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

        //print($"Current references are {enemyManager} & {cameraManager}");




    }//End Update
    #endregion Update

    #endregion Awake & Update

    #region General Functions

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(notifCenter.position, notifRad);
    }//End OnDrawGizmos



    //---------------------------------//
    //Used on the Awake() function to initialize any values in one line
    private static void Init()
    {
        //secState = SecurityLvl.SecLVL0;
    }//End Init

    //---------------------------------//
    //Raises / Lowers the security level based on the given context
    private void AdjustSecurityLevel(SecurityLvl securityLvl)
    {

    }//End AdjustSecurityLevel

    public void AlertGuards(Vector3 targetLoc, Vector3 callerLoc, float callRadius)
    {
        //print("Alerting Guards");
        //Also generating an array of guards on the call of this function
        GenGuardList();

        //EnemyManager reference
        EnemyManager enemyManager;

        //Used to reference each guard
        foreach (GameObject guard in guardsList)
        {
            float 

            distance = Vector3.Distance(guard.transform.position, callerLoc);

            //Individual guard reference
            //DO NOT MOVE
            enemyManager = guard.GetComponent<EnemyManager>();

            //Radius Check
            if (distance <= callRadius /*&& GameObject.CompareTag("[Insert guard type here]")*/)
            {
                //Calls the EnemyManager script's Alert() function and feeds in the targetLoc variable
                enemyManager.Alert(targetLoc);
            }
            else
            {
                //Showing which guards are out of range (purely there for debug reasons)
                //print($"{guard} is outside of camera range.");
            }
        }
    }//End AlertGuards


    //---------------------------------//
    //Generates an array of guard instances in the scene
    public void GenGuardList()
    {
        guardsList = GameObject.FindGameObjectsWithTag("Guard").ToList();

        if (guardsList.Count == 0 || guardsList == null)
        {
            print("No guards in the level");
        }

    }//End GenGuardArray

    #endregion General Functions
}
