using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SuspicionManager : MonoBehaviour
{
    #region Enumerations
    public enum SecurityLvl
    {
        SecLVL0,
        SecLVL1
    }

    [Header("Global Security Level")]
    //[SerializeField] public SecurityLvl secState;
    [SerializeField] public SecurityLvl secState;
    #endregion Enumerations

    #region Coroutines

    #endregion Coroutines

    #region Lists & Arrays

    [Header("Guards List")]

    [Tooltip("Shows the list of guards in the level")]
    [SerializeField] private List<GameObject> guardsList;

    [Header("Cameras List")]

    [Tooltip("Shows the list of cameras in the level")]
    [SerializeField] private List<GameObject> camerasList;

    #endregion Lists & Arrays

    #region Variables
    //---------------------------------------------------------------------------------------------------//

    [Header("Guard Refs")]

    [SerializeField] private EnemyManager enemyManager;

    //---------------------------------------------------------------------------------------------------//

    [Header("Camera Refs")]

    [SerializeField] private CameraManager cameraManager;

    //---------------------------------------------------------------------------------------------------//

    [Header("Debug / Testing Variables")]


    [Tooltip("Flag for Suspicion Level 0")]
    [HideInInspector] private bool susZeroFlag;

    [Tooltip("Flag for Suspicion Level 1")]
    [HideInInspector] private bool susOneFlag;

    private bool secFlag;

    private bool otherSecFlag;

    #endregion Variables

    #region Awake & Update

    #region Awake
    //Callled when the object is spawned.
    void Awake()
    {
        Init();

        GenGuardList();

        secFlag = false;
    }//End Awake
    #endregion Awake

    #region Update
    // Update is called once per frame
    void Update()
    {
        switch (secState)
        {
            #region Security Level 0
            case SecurityLvl.SecLVL0:
                //print("Security Level 0");
                //if (otherSecFlag == false)
                //{
                //    ModEyeSus(3.5f);

                //    otherSecFlag = true;

                //    secFlag = false;
                //}


                break;
            #endregion

            #region Security Level 1
            //Security station reaches this state when a guard interacts with it
            case SecurityLvl.SecLVL1:
                //print("Security Level 1");

                //Forcing all guards to have an eyeball sus level of [warySusMin]

                //if (secFlag == false)
                //{
                //    ModEyeSus(3.5f);

                //    otherSecFlag = false;

                //    secFlag = true;
                //}


                break;
            #endregion

            #region Default case / Bug Catcher
            default:
                print("Security Level not found! \a");
                break;
            #endregion

        }
    }//End Update
    #endregion Update

    #endregion Awake & Update

    #region General Functions

    //---------------------------------//
    //Draws Gizmos
    private void OnDrawGizmos()
    {

    }//End OnDrawGizmos


    //---------------------------------//
    //Used on the Awake() function to initialize any values in one line
    private static void Init()
    {
    }//End Init


    //---------------------------------//
    //Get's right into the sus level of all guards and modifies them
    public void ModEyeSus(float insertedValue)
    {
        /*
        TO DO:
        - Set the eyeball's minimum sus level to be 3.5

        - Add a flag for when the eyeball's sus level has been changed to 3.5 (middle of the wary field)
        */

        foreach (GameObject guard in guardsList)
        {
            enemyManager = guard.GetComponent<EnemyManager>();

            enemyManager.eyeball.minSusLevel = insertedValue;

            enemyManager.eyeball.susLevel = insertedValue;
        }
    }

    //---------------------------------//
    //Alerts available guards in a set radius
    public void AlertGuards(Vector3 targetLoc, Vector3 callerLoc, float callRadius)
    {
        //Also generating an array of guards on the call of this function
        GenGuardList();

        //EnemyManager reference
        EnemyManager enemyManager;

        //Used to reference each guard
        foreach (GameObject guard in guardsList)
        {
            float distance = Vector3.Distance(guard.transform.position, callerLoc);

            //Individual guard reference
            //DO NOT MOVE
            enemyManager = guard.GetComponent<EnemyManager>();

            //Radius Check
            if (distance <= callRadius && enemyManager.isStunned == false/*&& GameObject.CompareTag("[Insert guard type here]")*/)
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
            //throw
        }
    }//End GenGuardArray

    #endregion General Functions
}
