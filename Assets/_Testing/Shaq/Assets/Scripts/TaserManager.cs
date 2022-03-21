using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserManager : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------------//

    [SerializeField] [Range(0f, 200f)] private float taserSpeed = 25f;

    [HideInInspector] public float accuracy;

    //---------------------------------------------------------------------------------------------------//

    //---------------------------------//
    // Collision check for when the taser prefab hit's the player 
    private void OnTriggerEnter(Collider collision)
    {
        //Checking if the collided object is the player
        if (collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            if (!collision.gameObject.GetComponent<PlayerMovement>().isInvulnurable)
            {
                //Stuns the player if they aren't invulnerable
                collision.gameObject.GetComponent<PlayerMovement>().IsStunned = true;
            }
        }

        ////Checking if the collided object is a guard
        //if (collision.gameObject == guard)
        //{
        //    //Puts the guard into the STUNNED state
        //    //This method will get the Enemy Manager component/script of the specific guard that was hit
        //    collision.gameObject.GetComponent<EnemyManager>().stateMachine = EnemyManager.EnemyStates.STUNNED;
        //}

        //Debug.Log(collision.gameObject);
        
        //Destroys self
        Destroy(gameObject);
    }//End OnTriggerEnter

    private void Awake()
    {
    }

    //---------------------------------//
    // Called externally by the enemy manager-
    public void Init()
    {
        //Setting up the prefabs rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();

        //Prevents the prefab from colliding with the post processing volume
        Physics.IgnoreLayerCollision(0, 10);

        //Functions as recoil / accuracy for the guard when they instantiate (fire) the taser prefab
        Vector3 randVec3 = (new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0)) * accuracy;

        //Moves the taser projectile forward (with accuracy taken into account)
        rb.velocity = (transform.forward + (new Vector3 (0, -.01f, 0) + randVec3)) * taserSpeed;

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
