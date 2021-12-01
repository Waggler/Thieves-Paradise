using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripwireManager : MonoBehaviour

    //REMINDER:
    //  - RE-ENABLE TRIPWIRE PREFABS IN SCENE BEFORE SUBMITTING A MERGE REQUEST



{
    #region Variables
    [HideInInspector] [Range (0, 10)]private float rayDistance;
    [HideInInspector] private Vector3 initialHitRecord;


    [Header("Tripwire Variables")]
    [Tooltip("Radius in which guards can be  'called'  by the camera")]
    [SerializeField] [Range (0, 50)] private float callRadius;

    [Tooltip("Radius in which guards can be  'called'  by the camera")]
    [SerializeField] [Range (0, 100)]private float maxDistance = 60;

    
    [SerializeField] [Range (0, 7)] private int layerMask = 0 >> 7;


    [Header("Particle Vars")]
    [SerializeField] private ParticleSystem laserVFX;
    private float startLFTM = 90;


    //References the "Suspicion Manager" object in the scene
    [HideInInspector] private GameObject susManagerOBJ;
    //References the "SuspcionManager.cs" script found on the "Suspicion Manager" object
    [HideInInspector] private SuspicionManager susManagerRef;

    #endregion Variables

    #region Awake & Update

    #region Awake
    //Callled when the object is spawned. Used instead of Start() because the camera could be spawned after the game has started
    void Awake()
    {
        ////Instantiation of the ray
        //Ray ray = new Ray(transform.position, transform.forward);

        ////Logic for handlin ray collision
        //if (Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    //debugging hit
        //    //print(hit.collider.gameObject.name);

        //    //Use this in an if statement in Update()
        //    //If it's not the initial hit record then alert guards
        //    //Include code checkin to see if the laser was tripped by a guard.
        //    initialHitRecord = hit.point;
        //}

        Init();
    }

    #endregion Awake

    #region Update
    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, initialHitRecord);

        //print($"Distance = {distance}");

        startLFTM = distance;

        //print($"startLFTM = {startLFTM}");

        //Hit instance of the raycast
        RaycastHit hit;

        //Instantiation of the ray
        Ray ray = new Ray(transform.position, transform.forward);

        //Visualization of the made ray (visible in the scene view)
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.magenta);

        float initHitDistance = Vector3.Distance(transform.position, initialHitRecord);

        //Logic for handling ray collision
        if (Physics.Raycast(ray, out hit, initHitDistance))
        {
            if (hit.point != initialHitRecord)
            {
                //Checking to see if the hit game object is NOT a guard
                if (!hit.collider.gameObject.CompareTag("Guard"))
                {
                    //Add logic for what to do when an object that isn't the guard sets off the alarm / tripwire
                    print(hit.collider.gameObject.name);

                    //Always generate the guard list before alerting guards
                    susManagerRef.GenGuardList();

                    susManagerRef.AlertGuards(hit.point, transform.position, callRadius);
                }
            }
        }
    }

    #endregion Update

    #endregion Awake & Update

    #region General Functions

    //---------------------------------//
    //Draws Gizmos / shapes in editor
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position, transform.forward);

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, callRadius);
    //}//End OnDrawGizmos

    //---------------------------------//
    //Used on the Awake() function to initialize any values in one line
    public void Init()
    {
        //Instantiation of the ray
        Ray ray = new Ray(transform.position, transform.forward);

        //Logic for handlin ray collision
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
        {
            //Records the initial hit point of the raycast
            initialHitRecord = hit.point;
        }
        else
        {
            print("Either the distance or the layer mask is broken");
        }

        //Note: This method of referencing the suspicion manager is stupid and I should find a way to do it in one line
        //Creates a reference to the suspicion manager object
        susManagerOBJ = GameObject.FindGameObjectWithTag("GameController");

        //creates a direct reference to the suspicion manager script
        susManagerRef = susManagerOBJ.GetComponent<SuspicionManager>();

        var main = laserVFX.main;

        main.startLifetime = startLFTM;

        layerMask = ~LayerMask.GetMask("Wall"); //get the wall

    }

    #endregion General Functions
}
