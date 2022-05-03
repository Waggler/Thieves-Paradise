using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private ScoreScreenManager scoreScreenManager;
    [SerializeField] private AudioSource explosionSource;
    [SerializeField] private Animator explosionAnimator;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource combatMusicSource;


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        if (scoreScreenManager == null)
        {
            scoreScreenManager = FindObjectOfType<ScoreScreenManager>();
        }


    }//END Start

    //-----------------------//
    public void TriggerExplosion()
    //-----------------------//
    {
        musicSource.Stop();
        combatMusicSource.Stop();

        explosionAnimator.SetBool("isExploding", true);
        explosionSource.Play();

    }//END TriggerExplosion

    //-----------------------//
    public void FinishExplosion()
    //-----------------------//
    {
        explosionAnimator.SetBool("isDoneExploding", true);


    }//END OpenScoreBoard

    public void OpenScoreBoard()
    {
        scoreScreenManager.StartScoring();
    }


}//END CLASS ExplosionHandler
