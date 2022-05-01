using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserManager : MonoBehaviour
{
    //---------------------------------------------------------------------------------------------------//

    [SerializeField] [Range(0f, 60f)] private float taserSpeed = 25f;

    [HideInInspector] public float accuracy;

    [SerializeField] private Collider sphereCollider;

    private LayerMask layerMask;

    private ScoreScreenManager scoreManagerRef;

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

                ScoreData scoreData = new ScoreData(ScoreType.DEDUCTIONS, 0, "Tased");
                scoreManagerRef.ReportScore(scoreData);
            }
        }

        //Destroys self
        Destroy(gameObject);
    }//End OnTriggerEnter

    private void Awake()
    {
    }

    //---------------------------------//
    // Called externally by the enemy manager-
    public void Init(ScoreScreenManager _screenManagerRef)
    {
        //Setting up the prefabs rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();

        //Prevents the prefab from colliding with the post processing volume

        //Functions as recoil / accuracy for the guard when they instantiate (fire) the taser prefab
        Vector3 randVec3 = (new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0)) * accuracy;

        //Moves the taser projectile forward (with accuracy taken into account)
        rb.velocity = (transform.forward + (new Vector3 (0, -.01f, 0) + randVec3)) * taserSpeed;

        scoreManagerRef = _screenManagerRef;

    }//End Init
}
