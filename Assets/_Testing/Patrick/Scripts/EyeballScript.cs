using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeballScript : MonoBehaviour
{    
    //player information
    private Transform player;
    private LayerMask layerMask;


    //vision stats
    [Header("Vision Stats")]
    [SerializeField] private float sightRange;
    [Range(1.0f, 180.0f)]
    [SerializeField]private float maxVisionAngle; // 0-180, 0 = directly in front, 90 = left/right, 180 = directly behind
    [SerializeField]private float susGrowthMultiplier = 1;
    [SerializeField]private float susDecreaseMultiplier = 1;

    //Player Detection output
    [HideInInspector] public float sightAngle;
    [HideInInspector] public float susLevel; //how suspicious the eyeball currently is
    [HideInInspector] public Vector3 lastKnownLocation;
    [HideInInspector] public bool canCurrentlySeePlayer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        layerMask = ~(1 << 3);

        lastKnownLocation = transform.position; //set the last known location to the location of the guard to start to prevent potential weirdness
    }//End Start

    // Update is called once per frame
    void Update()
    {
        canCurrentlySeePlayer = FindPlayer();
        ChangeSus(canCurrentlySeePlayer);
    }//END Update

    private bool FindPlayer()
    {
        Vector3 direction =  player.position - this.transform.position;
        if (direction.magnitude > sightRange)
        {
            print("Too Far Away");
            return false; //do nothing if the player is too far away
        }

        Debug.DrawLine(transform.position, player.position, Color.red);
        if (Physics.Linecast(transform.position, player.position, layerMask))
        {
            print("Can't See Player");
            return false; //do nothing if something is in the way
        }

        direction = direction.normalized;

        sightAngle = Vector3.Angle(direction, this.transform.forward);
        

        if (sightAngle > maxVisionAngle)
        {
            print("Outside of Periphery");
            return false; //do nothing if player is too far out of focus
        }

        //only update known location after confirming they can see the player
        //this means that the known location will remain where it was whenever they can't
        lastKnownLocation = player.position;
        return true;
    }//END FindPlayer

    private void ChangeSus(bool increase)
    {
        if (increase)
        {
            float focus = sightAngle/maxVisionAngle; //percent based on center of vision
            focus = 1 - focus; //invert it because closer to 0 is better
            focus = (focus / 2) + 0.5f; //set minimum multiplier to 50% when at edge of periphery
            susLevel += focus * susGrowthMultiplier * Time.deltaTime;

        } else 
        {
            susLevel -= Time.deltaTime * susDecreaseMultiplier;
        }
        //set bounds
        if (susLevel > 10)
        {
            susLevel = 10;
        } else if (susLevel < 0)
        {
            susLevel = 0;
        }
        print (susLevel);
    }//END ChangeSus
    
    #if UNITY_EDITOR
    //Debug information to display vision information in editor
    //should not be visible in game
    private void OnDrawGizmos()
    {
        if (canCurrentlySeePlayer)
        {
            Gizmos.color = Color.red;
        } else{
            Gizmos.color = Color.green;
        }
        
        Gizmos.DrawSphere(lastKnownLocation, 0.5f);

        Gizmos.matrix = this.transform.localToWorldMatrix; //sets the position and rotation and such of the gizmo to the parent object

        //draw vision cone limits
        Gizmos.DrawRay(Vector3.zero, Vector3.forward * sightRange);

        Vector3 angleEdge = Quaternion.AngleAxis(maxVisionAngle, Vector3.up) * Vector3.forward;
        Gizmos.DrawRay(Vector3.zero, angleEdge * sightRange);

        angleEdge = Quaternion.AngleAxis(-maxVisionAngle, Vector3.up) * Vector3.forward;
        Gizmos.DrawRay(Vector3.zero, angleEdge * sightRange);

        angleEdge = Quaternion.AngleAxis(maxVisionAngle, Vector3.left) * Vector3.forward;
        Gizmos.DrawRay(Vector3.zero, angleEdge * sightRange);
    }
    #endif
}
