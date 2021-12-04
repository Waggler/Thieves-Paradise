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
        playerSource.volume = 0.015f;
        int i = Random.Range(0, walkClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(walkClips[i]);


    }//END WalkingFootStep

    //-----------------------//
    public void RunningFootStep()
    //-----------------------//
    {
        playerSource.volume = 0.015f;
        int i = Random.Range(0, runClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(runClips[i]);


    }//END RunningFootStep

    //-----------------------//
    public void VentFootStep()
    //-----------------------//
    {
        playerSource.volume = 0.015f;
        int i = Random.Range(0, ventClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(ventClips[i]);


    }//END VentFootStep

    //-----------------------//
    public void BeginStruggle()
    //-----------------------//
    {
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
        playerSource.volume = .02f;
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
        playerSource.volume = 0.02f;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(rollClip);

    }//END Roll

}//END PlayerAudio
