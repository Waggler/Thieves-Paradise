using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GuardAudio : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource guardSource;

    [SerializeField] private float normalVolume;
    [SerializeField] private float loudVolume;

    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;

    [Header("Vocalizations")]
    [SerializeField] private AudioClip[] spotBarks;

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
        guardSource.volume = normalVolume;
        int i = Random.Range(0, walkClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(walkClips[i]);


    }//END WalkingFootStep

    //-----------------------//
    public void RunningFootStep()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        int i = Random.Range(0, runClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(runClips[i]);


    }//END RunningFootStep

    //-----------------------//
    public void SpotPlayer()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        int i = Random.Range(0, spotBarks.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(spotBarks[i]);

    }//END SpotPlayer

    //-----------------------//
    public void StruggleFall()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(fallClip);

    }//END StruggleFall

    //-----------------------//
    public void StruggleHit()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(hitClip);

    }//END StruggleHit

    //-----------------------//
    public void Chew()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(chewingClip);

    }//END Chew


}//END GuardAudio
