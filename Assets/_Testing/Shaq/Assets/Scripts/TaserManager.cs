using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserManager : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------------//

    [SerializeField] [Range(0f, 50f)] private float taserSpeed = 25f;

    [SerializeField] private Vector3 target;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject guard;

    [HideInInspector] public float accuracy;

    //---------------------------------------------------------------------------------------------------//

    //---------------------------------//
    // Collision check for when the taser prefab hit's the player 
    private void OnTriggerEnter(Collider collision)
    {
            //Checking if the collided object is the player
            if (collision.gameObject == player)
            {
                //Stuns the player
                player.GetComponent<PlayerMovement>().IsStunned = true;
            }

            ////Checking if the collided object is a guard
            //if (collision.gameObject == guard)
            //{
            //    //Puts the guard into the STUNNED state
            //    //This method will get the Enemy Manager component/script of the specific guard that was hit
            //    collision.gameObject.GetComponent<EnemyManager>().stateMachine = EnemyManager.EnemyStates.STUNNED;
            //}

        Debug.Log(collision.gameObject);
        //Destroys self
        Destroy(gameObject);
    }//End OnTriggerEnter

    private void Awake()
    {
        Physics.IgnoreLayerCollision(0, 10);
    }

    //---------------------------------//
    // Called on Awake and initializes everything that is finalized and needs to be done at awake
    public void Init()
    {
        //Setting up the prefabs rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();


        //Functions as recoil / accuracy for the guard when they instantiate (fire) the taser prefab
        Vector3 randVec3 = (new Vector3 (Random.Range(-1, 1), Random.Range(-1, 1), 0)) * accuracy;

        //Moves the taser projectile forward
        rb.velocity = (transform.forward + randVec3) * taserSpeed;

        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
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
}
