using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioSource struggleSource;


    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] walkClips;
    [SerializeField] private AudioClip[] ventClips;
    [SerializeField] private AudioClip[] runClips;

    [Header("Other")]
    [SerializeField] private AudioClip slideClip;
    [SerializeField] private AudioClip rollClip;
    [SerializeField] private AudioClip diveClip;

    [SerializeField] private AudioClip struggleClip;


    //-----------------------//
    public void WalkingFootStep()
    //-----------------------//
    {
        int i = Random.Range(0, walkClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(walkClips[i]);


    }//END WalkingFootStep

    //-----------------------//
    public void RunningFootStep()
    //-----------------------//
    {
        int i = Random.Range(0, runClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(runClips[i]);


    }//END RunningFootStep

    //-----------------------//
    public void VentFootStep()
    //-----------------------//
    {
        int i = Random.Range(0, ventClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(ventClips[i]);


    }//END VentFootStep

    //-----------------------//
    public void BeginStruggle()
    //-----------------------//
    {
        playerSource.pitch = 1;
        struggleSource.Play();

    }//END BeginStruggle

    //-----------------------//
    public void EndStruggle()
    //-----------------------//
    {
        struggleSource.Stop();

    }//END BeginStruggle

    //-----------------------//
    public void Slide()
    //-----------------------//
    {
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(slideClip);

    }//END Slide

    //-----------------------//
    public void Dive()
    //-----------------------//
    {
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(diveClip);

    }//END Dive

    //-----------------------//
    public void Roll()
    //-----------------------//
    {
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(rollClip);

    }//END Roll

}//END PlayerAudio
