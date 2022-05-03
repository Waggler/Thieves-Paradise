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
    [SerializeField] private int sceneIndexToLoad;

    //-----------------------//
    private void Start()
    //-----------------------//
    {
        if (scoreScreenManager == null)
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
    public void FinishExploding()
    //-----------------------//
    {

        explosionAnimator.SetBool("isDoneExploding", true);

    }//END FinishExploding

    //-----------------------//
    public void OpenScoreBoard()
    //-----------------------//
    {
        scoreScreenManager.nextSceneIndex = sceneIndexToLoad;
        scoreScreenManager.StartScoring();

    }//END OpenScoreBoard


}//END CLASS ExplosionHandler
