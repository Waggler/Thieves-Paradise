using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripwireManager : MonoBehaviour

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

    float distance;

    [Header("Audio")]
    [SerializeField] private AudioSource laserAudioSource;

    //References the "Suspicion Manager" object in the scene
    [HideInInspector] private GameObject susManagerOBJ;

    //References the "SuspcionManager.cs" script found on the "Suspicion Manager" object
    [HideInInspector] private SuspicionManager susManagerRef;

    private bool isPlayAudio = false;

    private ScoreScreenManager scoreManagerRef;
    private ScoreKeeper scoreKeeper;

    #endregion Variables

    #region Start
    //Callled when the object is spawned. Used instead of Start() because the camera could be spawned after the game has started
    void Start()
    {
        Init();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    #endregion Start

    #region Update
    // Update is called once per frame
    void Update()
    {
        //Visualization of the made ray (visible in the scene view)
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.magenta);

        //Logic for handling ray collision
        if (Physics.Raycast(securityRay, out RaycastHit hit, distance, 12))
        {
            //Checking to see if the collided game object is NOT a guard, and if the hit point is not the initial record
            if (!hit.collider.gameObject.CompareTag("Guard") && hit.point != initialHitRecord)
            {
                //Alerts guards in a set radius (Guards List generated in method)
                susManagerRef.AlertGuards(hit.point, transform.position, callRadius);

                ScoreData scoreData = new ScoreData(ScoreType.DEDUCTIONS, 0, "TripWire");
                scoreManagerRef.ReportScore(scoreData);

                // Insert bool check for playing audio
                if (isPlayAudio == false)
                {
                    //play alert sound
                    laserAudioSource.Play();

                    //Flip the bool
                    isPlayAudio = true;
                    
                    Debug.Log(isPlayAudio);
                }
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    scoreKeeper.trippedWire = true;
                }
            }
            else
            {
                //Reset the bool the bool
                isPlayAudio = false;

                //Debug.Log(isPlayAudio);
            }
        }
    }
    #endregion Update

    //Instantiation of the ray
    private Ray securityRay; 

    #region General Methods
    public void Init()
    {
        //Might have to put this back in the coroutine
        securityRay = new Ray(transform.position, transform.forward);

        //Instantiation of the ray
        Ray initialRay = new Ray(transform.position, transform.forward);

        //Logic for handlin ray collision
        if (Physics.Raycast(initialRay, out RaycastHit hit, maxDistance /*layerMask*/))
        {
            //Records the initial hit point of the raycast
            initialHitRecord = hit.point;

            //hitObject = hit.rigidbody.gameObject;

            //Printing for debug purposes, feel free to comment out or delete
            //Debug.Log(hitObject);
        }

        //Creates a reference to the suspicion manager object
        susManagerOBJ = GameObject.FindGameObjectWithTag("SecurityStation");

        //creates a direct reference to the suspicion manager script
        susManagerRef = susManagerOBJ.GetComponent<SuspicionManager>();

        var main = laserVFX.main;

        main.startLifetime = startLFTM;

        layerMask = ~LayerMask.GetMask("Wall"); //get the wall
        
        //finding the distance, what distance exactly I have no fucking clue
        distance = Vector3.Distance(transform.position, initialHitRecord);

        startLFTM = distance;
    }
    #endregion General Methods
}
