using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioSource struggleSource;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] private float noiseLevelOneVolume;
    [SerializeField] private float noiseLevelTwoVolume;
    [SerializeField] private float noiseLevelThreeVolume;

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
    [SerializeField] private AudioClip shockClip;
    [SerializeField] private AudioClip throwClip;
    [SerializeField] private AudioClip pickupClip;

    //-----------------------//
    private void Start()
    //-----------------------//
    {
        if (playerAnimator == null)
        {
            playerAnimator = GetComponentInParent<Animator>();
        }
    }

    //-----------------------//
    public void WalkingFootStep()
    //-----------------------//
    {
        if (playerAnimator.GetBool("isSprinting") == false)
        {
            playerSource.volume = noiseLevelOneVolume;
            int i = Random.Range(0, walkClips.Length);
            playerSource.pitch = Random.Range(pitchMin, pitchMax);
            playerSource.PlayOneShot(walkClips[i]);

        }


    }//END WalkingFootStep

    //-----------------------//
    public void RunningFootStep()
    //-----------------------//
    {
        playerSource.volume = noiseLevelThreeVolume;
        int i = Random.Range(0, runClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(runClips[i]);


    }//END RunningFootStep

    //-----------------------//
    public void VentFootStep()
    //-----------------------//
    {
        playerSource.volume = noiseLevelOneVolume;
        int i = Random.Range(0, ventClips.Length);
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(ventClips[i]);


    }//END VentFootStep

    //-----------------------//
    public void BeginStruggle()
    //-----------------------//
    {
        struggleSource.volume = noiseLevelThreeVolume;
        struggleSource.Play();

    }//END BeginStruggle

    //-----------------------//
    public void EndStruggle()
    //-----------------------//
    {
        struggleSource.volume = noiseLevelThreeVolume;
        struggleSource.Stop();

    }//END BeginStruggle

    //-----------------------//
    public void Slide()
    //-----------------------//
    {
        playerSource.volume = noiseLevelTwoVolume;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(slideClip);

    }//END Slide

    //-----------------------//
    public void Dive()
    //-----------------------//
    {
        playerSource.volume = noiseLevelThreeVolume;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(diveClip);

    }//END Dive

    //-----------------------//
    public void Roll()
    //-----------------------//
    {
        playerSource.volume = noiseLevelOneVolume;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(rollClip);

    }//END Roll
    //-----------------------//
    public void Throw()
    //-----------------------//
    {
        playerSource.volume = noiseLevelOneVolume;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(throwClip);
    }//END Throw

    //-----------------------//
    public void Pickup()
    //-----------------------//
    {
        playerSource.volume = noiseLevelOneVolume;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(pickupClip);
    }//END Pickup

    //-----------------------//
    public void Shock()
    //-----------------------//
    {
        playerSource.volume = noiseLevelTwoVolume;
        playerSource.pitch = Random.Range(pitchMin, pitchMax);
        playerSource.PlayOneShot(shockClip);
    }//END Pickup

}//END PlayerAudio
