using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private ScoreScreenManager scoreScreenManager;
    [SerializeField] private AudioSource explosionSource;
    [SerializeField] private Animator explosionAnimator;
    [SerializeField] private AudioSource musicSource;

    //-----------------------//
    private void Start()
    //-----------------------//
    {
        if (scoreScreenManager = null)
        {
            scoreScreenManager = FindObjectOfType<ScoreScreenManager>();
        }


    }//END Start

    //-----------------------//
    public void TriggerExplosion()
    //-----------------------//
    {
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
