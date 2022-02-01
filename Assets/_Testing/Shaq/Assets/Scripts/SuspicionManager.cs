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

public class SuspicionManager : MonoBehaviour
{
    #region Enumerations
    private enum SecurityLvl
    {
        SecLVL0,
        SecLVL1
    }

    [Header("AI State")]

    [SerializeField] SecurityLvl secState;
    #endregion Enumerations

    #region Coroutines

    #endregion Coroutines

    #region Lists & Arrays

    [Header("Guards List")]
    [Tooltip("Shows the list of guards in the level")]
    [SerializeField] private List<GameObject> guardsList;

    [Header("Cameras List")]
    [Tooltip("Shows the list of cameras in the level")]
    [SerializeField] private List<GameObject> camerasInLevel;

    #endregion Lists & Arrays

    #region Variables
    //---------------------------------------------------------------------------------------------------//

    //Note: The green squigglies just mean it's given a value in the inspector instead of in the script
    [Header("Suspicion Manager Variables")]
    [SerializeField] private Text susText;

    //---------------------------------------------------------------------------------------------------//

    [Header("Guard Refs")]
    [SerializeField] private EnemyManager enemyManager;

    //---------------------------------------------------------------------------------------------------//

    [Header("Camera Refs")]
    [SerializeField] private CameraManager cameraManager;

    //---------------------------------------------------------------------------------------------------//

    [Header("Debug / Testing Variables")]
    [Tooltip("This thing literally has not legitimate purpose other than to occupy space for a header")]
    [SerializeField] private bool stupidBoolToMakeCSharpShutTheHellUp;


    #endregion Variables

    #region Awake & Update

    #region Awake
    //Callled when the object is spawned.
    void Awake()
    {
        Init();

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
                print("Security Level 0");

                break;
            #endregion

            #region Security Level 1
            case SecurityLvl.SecLVL1:
                print("Security Level 1");

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
        //secState = SecurityLvl.SecLVL0;
    }//End Init



    //---------------------------------//
    //Raises / Lowers the security level based on the given context
    private void AdjustSecurityLevel(SecurityLvl securityLvl)
    {

    }//End AdjustSecurityLevel



    //---------------------------------//
    //Alerts available guards in a set radius
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
            print("No guards in the level");
        }

    }//End GenGuardArray

    #endregion General Functions
}
