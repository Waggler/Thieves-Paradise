using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaserManager : MonoBehaviour
{
    /*
    "UNHOLSTER"
    
    Receive Orientation
        -By receiving information via FaceTarget?

    Launch

    Check for collision with the PLAYER

    COLLIDE 

    DO WHATEVER IS SUPPOSED TO HAPPEN ON COLLISION

    RETURN TO THE GUARD'S "HOLSTER"

    */

    #region Variables

    //---------------------------------------------------------------------------------------------------//

    [SerializeField] [Range(0f, 5f)] private float taserSpeed = 3.5f;

    [SerializeField] private Vector3 target;

    //---------------------------------------------------------------------------------------------------//
    #endregion Variabels

    #region Awake & Update
    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
    }

    #endregion Awake & Update

    #region Collision Check
    //Taser Collision Check


    private void OnTriggerEnter(Collider collision)
    {
        //Checking if the collided object has the tag of player
        if (collision.gameObject.CompareTag("Player"))
        {
            //INSERT CODE TO STUN THE PLAYER

        }

        //Destroys self
        Destroy(gameObject);
    }

    #endregion


    #region Methods
    private void Init()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag("Player").transform.position;

        FaceTarget(target);

        rb.velocity = transform.forward * taserSpeed;
    }

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

        transform.rotation = lookRotation; //Quaternion.Slerp(transform.rotation, lookRotation, 1);
        //Slerp is used for rotating something over time, we just want to set the rotation, so simply setting the rotation to lookRotation was all you needed. 
    }//End FaceTarget

    #endregion Methods
}
