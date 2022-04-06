using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmackBoxManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject guard;

    [SerializeField] private GameObject player;

    private PlayerMovement playerMovement;

    private EnemyManager enemyManager;

    #endregion Variables

    private void Awake()
    {
        enemyManager = guard.GetComponent<EnemyManager>();

        player = GameObject.FindWithTag("Player");

    }//End Awake


    //Remember to put this back to OnTriggerStay
    private void OnTriggerEnter(Collider other)
    {
        // Checking the following:
        //  - Is the collided game object the player?

        //  - Is the player not stunned?

        //  - Is the guard not in their PASSIVE / SUSPICIOUS state?

        //Add timer for 



        if (other.gameObject == player)
        {
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement.IsStunned == false)
            {
                //Linecast
                //Physical line cast from point A to point B
                //If it reaches the second point, it will respond with true (I think)
                if (Physics.Linecast(transform.position, player.transform.position))
                {

                    //Add raycast between guard & player

                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    
                    //In the future change this to play the smack animation

                    other.gameObject.GetComponent<PlayerMovement>().IsStunned = true;

                    Debug.Log("(Meaty Thwak)");
                    Debug.Log("(Both Chuckle)");
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
