using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserManager : MonoBehaviour
{
    #region Variables

    //---------------------------------------------------------------------------------------------------//

    [SerializeField] [Range(0f, 50f)] private float taserSpeed = 25f;

    [SerializeField] private Vector3 target;

    [SerializeField] private GameObject player;

    [SerializeField] private PlayerMovement playerMovement;

    public float accuracy;

    //---------------------------------------------------------------------------------------------------//
    #endregion Variabels

    #region Awake & Update
    //private void Awake()
    //{
    //    Init();
    //}

    // Update is called once per frame
    void Update()
    {
    }

    #endregion Awake & Update

    #region Collision Check
    //Taser Collision Check


    private void OnTriggerEnter(Collider collision)
    {
        //Checking if the collided object is the player
        if (collision.gameObject == player)
        {
            //INSERT CODE TO STUN THE PLAYER
            playerMovement.GetComponent<PlayerMovement>().IsStunned = true;
        }

        //Destroys self
        Destroy(gameObject);
    }
    #endregion


    #region Methods
    //---------------------------------//
    // Called on Awake and initializes everything that is finalized and needs to be done at awake
    public void Init()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag("Player").transform.position;

        FaceTarget(target);

        //"Recoil"
        //Vector3 randVec3 = new Vector3 (Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        Vector3 randVec3 = new Vector3 ((Random.Range(-1, 1) * accuracy), (Random.Range(-1, 1) * accuracy), 0);

        //Moves the taser projectile forward
        rb.velocity = (transform.forward + randVec3) * taserSpeed;

        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
        }

        if (playerMovement == null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }//End Init

    //---------------------------------//
    // Function for facing the target
    void FaceTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;

        Quaternion lookRotation = Quaternion.identity;
        if (direction.x != 0 && direction.z != 0)
        {
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        }

        transform.rotation = lookRotation;
    }//End FaceTarget

    #endregion Methods
}
