using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GuardAudio : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource guardSource;

    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] walkClips;
    [SerializeField] private AudioClip[] runClips;

    [Header("Other")]
    [SerializeField] private AudioClip spottedClip;
    [SerializeField] private AudioClip chewingClip;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip fallClip;


    //-----------------------//
    public void WalkingFootStep()
    //-----------------------//
    {
        //guardSource.volume = 0.03f;
        int i = Random.Range(0, walkClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(walkClips[i]);


    }//END WalkingFootStep

    //-----------------------//
    public void RunningFootStep()
    //-----------------------//
    {
        //guardSource.volume = 0.05f;
        int i = Random.Range(0, runClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(runClips[i]);


    }//END RunningFootStep

    //-----------------------//
    public void SpotPlayer()
    //-----------------------//
    {
        //guardSource.volume = 0.05f;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(spottedClip);

    }//END SpotPlayer

    //-----------------------//
    public void StruggleFall()
    //-----------------------//
    {
        //guardSource.volume = 0.035f;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(fallClip);

    }//END StruggleFall

    //-----------------------//
    public void StruggleHit()
    //-----------------------//
    {
        //guardSource.volume = 0.035f;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(hitClip);

    }//END StruggleHit

    //-----------------------//
    public void Chew()
    //-----------------------//
    {
        //guardSource.volume = 0.03f;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(chewingClip);

    }//END Chew


}//END GuardAudio
