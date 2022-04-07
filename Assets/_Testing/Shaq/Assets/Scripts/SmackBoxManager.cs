using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackBoxManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject guard;

    [SerializeField] private GameObject guardEyeball;

    [SerializeField] private GameObject player;

    [SerializeField] private GameObject playerVisionTarget;

    private PlayerMovement playerMovement;

    private EnemyManager enemyManager;

    private LayerMask layerMask;

    #endregion Variables

    private void Awake()
    {
        enemyManager = guard.GetComponent<EnemyManager>();

        player = GameObject.FindWithTag("Player");

        playerVisionTarget = GameObject.Find("VisionTarget");



        layerMask = LayerMask.GetMask("Player") + LayerMask.GetMask("Ghost") + LayerMask.GetMask("Guard") + LayerMask.GetMask("Smack") + LayerMask.GetMask("Post Processing");
        layerMask = ~layerMask; //get the player layer to make sure they don't block themselves from vision

    }//End Awake


    //Remember to put this back to OnTriggerStay
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //Assigns player movement variable to the current instance of it
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement.IsStunned == false && enemyManager.ceaseFire == false)
            {
                //Linecast
                //Physical line cast from point A to point B
                //If it reaches the second point, it will respond with true (I think)
                //VERIFY THAT THIS WORKS
                if (!Physics.Linecast(guardEyeball.transform.position, playerVisionTarget.transform.position, layerMask) == true)
                {
                    // In the event that we need the guard to face what they are smacking
                    //enemyManager.FaceTarget(enemyManager.target);
                    
                    
                    //Stop the player's movement

                    // Play smack animation

                    //Check for animation end

                    //At animation's end, stun the player

                    //Either guard goes about normal business or force them into HOSTILE state

                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    //In the future change this to play the smack animation

                    other.gameObject.GetComponent<PlayerMovement>().IsStunned = true;

                    Debug.Log("(Meaty Thwak)");
                    Debug.Log("(Both Chuckle)");


                    //Add time delay

                    //Generate point behind guard for moving back

                }
            }
        }
        else
        {
            Debug.Log("There was no meaty thwack heard for quite some time...");
        }
    }//End OnTriggerStay

    private void OnTriggerExit(Collider other)
    {
        
    }
}
