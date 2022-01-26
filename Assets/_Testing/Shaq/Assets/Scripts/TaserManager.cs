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
    // Start is called before the first frame update

    #region Variables

    //---------------------------------------------------------------------------------------------------//

    [SerializeField] [Range(0f, 5f)] private float taserSpeed = 3.5f;

    [SerializeField] private Vector3 target;

    [SerializeField] [Range(0.5f, 5f)] private float killTimer;

    //[SerializeField] private EnemyManager enemyManager;




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
        killTimer -= Time.fixedDeltaTime;

        if (killTimer < 0)
        {
            Destroy(gameObject);
        }
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

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 0.1f);
    }//End FaceTarget

    #endregion Methods
}
