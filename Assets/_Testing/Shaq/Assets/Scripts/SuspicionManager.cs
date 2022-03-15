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

    private bool Gabagool;

    //---------------------------------------------------------------------------------------------------//

    [Header("Camera Refs")]

    [SerializeField] private CameraManager cameraManager;

    //---------------------------------------------------------------------------------------------------//

    #endregion Variables

    #region Awake & Update

    #region Awake
    //Callled when the object is spawned.
    void Awake()
    {
        Init();

        GenGuardList();

        secState = SecurityLvl.SecLVL0;
    }//End Awake
    #endregion Awake

    #region Update
    // Update is called once per frame
    void Update()
    {
        /*
            "Don't put a fucking timer in this part of the code. I want the security level to stay here for the rest of the game when triggered."
                - Tempe Zaliauskas
        */

        switch (secState)
        {
            #region Security Level 0
            case SecurityLvl.SecLVL0:

                //print("Entered Security Level 0");


                break;
            #endregion Security Level 0

            #region Security Level 1

            //Security station reaches this state when a guard interacts with it
            case SecurityLvl.SecLVL1:
                //Forcing all guards to have an eyeball sus level of [warySusMin]

                //print("Entered Security Level 1");

                ModEyeSus(enemyManager.warySusMin + .4f);

                break;
            #endregion Security Level 1

  
            default:
                print("Security Level not found!");
                break;
        }
    }//End Update
    #endregion Update

    #endregion Awake & Update

    #region General Functions

    //---------------------------------//
    //Draws Gizmos
    private void OnDrawGizmosSelected()
    {

    }//End OnDrawGizmosSelected


    public void DummyMethod()
    {
        secState = SecurityLvl.SecLVL1;
    }


    //---------------------------------//
    //Used on the Awake() function to initialize any values in one line
    private static void Init()
    {
    }//End Init


    //---------------------------------//
    //Get's right into the sus level of all guards and modifies them
    public void ModEyeSus(float insertedValue)
    {
        foreach (GameObject guard in guardsList)
        {
            enemyManager = guard.GetComponent<EnemyManager>();

            enemyManager.eyeball.minSusLevel = 3.1f;

            enemyManager.eyeball.susLevel = 3.5f;
        }
    }

    //---------------------------------//
    //Alerts available guards in a set radius
    // TAKES:
    //         - The location of where you want the guards to go (targetLoc)
    //         - The location of the caller of the method (callerLoc)
    //         - The call radius (callRadius)
    public void AlertGuards(Vector3 targetLoc, Vector3 callerLoc, float callRadius)
    {
        //Also generating an array of guards on the call of this function
        GenGuardList();

        //Inser floor check method call

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
            if (distance <= callRadius && enemyManager.isStunned == false)
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
    public void GenGuardList() => guardsList = GameObject.FindGameObjectsWithTag("Guard").ToList(); //End GenGuardList

    #endregion General Functions
}
