using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private ScoreScreenManager scoreScreenManager;
    [SerializeField] private AudioSource explosionSource;
    [SerializeField] private Animator explosionAnimator;
    [SerializeField] private AudioSource musicSource;
    private PlayerMovement playerMov;

    //-----------------------//
    private void Start()
    //-----------------------//
    {
        if (scoreScreenManager = null)
        {
            scoreScreenManager = FindObjectOfType<ScoreScreenManager>();
        }
        playerMov = FindObjectOfType<PlayerMovement>();

    }//END Start

    //-----------------------//
    public void TriggerExplosion()
    //-----------------------//
    {
        playerMov.hp = 9999; //prevent the player from dying during explosion

        musicSource.Stop();
        explosionAnimator.SetBool("isExploding", true);
        explosionSource.Play();

    }//END TriggerExplosion

    //-----------------------//
    public void OpenScoreBoard()
    //-----------------------//
    {
        explosionAnimator.SetBool("isDoneExploding", true);

        //scoreScreenManager.Open();

    }//END OpenScoreBoard


}//END CLASS ExplosionHandler
