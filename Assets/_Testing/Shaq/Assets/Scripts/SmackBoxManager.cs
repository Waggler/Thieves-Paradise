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

    [SerializeField] private GuardAudio guardAudioScript;

    private PlayerMovement playerMovement;

    private EnemyManager enemyManager;

    private LayerMask layerMask;

    private float eyeballVisionAngleRecord;

    #endregion Variables

    private void Awake()
    {
        Init();

    }//End Awake


    //Remember to put this back to OnTriggerStay
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {

            enemyManager.eyeball.maxVisionAngle = enemyManager.eyeball.maxVisionAngle * 2;

            //Assigns player movement variable to the current instance of it
            playerMovement = other.gameObject.GetComponent<PlayerMovement>();

            if (playerMovement.IsStunned == false && enemyManager.ceaseFire == false && enemyManager.stateMachine == EnemyManager.EnemyStates.RANGEDATTACK)
            {
                //Linecast
                //Physical line cast from point A to point B
                //If it reaches the second point, it will respond with true (I think)
                if (!Physics.Linecast(guardEyeball.transform.position, playerVisionTarget.transform.position, layerMask) == true)
                {
                    enemyManager.guardAnim.EnterSmack();

                    other.gameObject.GetComponent<PlayerMovement>().IsStunned = true;

                    guardAudioScript.MeleeHit();
                }
            }
        }
    }//End OnTriggerEnter

    private void OnTriggerExit(Collider other)
    {
        enemyManager.guardAnim.ExitSmack();

        enemyManager.eyeball.maxVisionAngle = eyeballVisionAngleRecord;
    }//End OnTriggerExit

    private void Init()
    {
        enemyManager = guard.GetComponent<EnemyManager>();

        player = GameObject.FindWithTag("Player");

        playerVisionTarget = GameObject.Find("VisionTarget");

        layerMask = LayerMask.GetMask("Player") + LayerMask.GetMask("Ghost") + LayerMask.GetMask("Guard") + LayerMask.GetMask("Smack") + LayerMask.GetMask("Post Processing");
        layerMask = ~layerMask; //get the player layer to make sure they don't block themselves from vision

        eyeballVisionAngleRecord = enemyManager.eyeball.maxVisionAngle;
    }
}
