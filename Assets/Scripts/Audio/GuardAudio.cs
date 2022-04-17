using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GuardAudio : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource guardSource;

    [SerializeField] private float idleWaitTimeMin;
    [SerializeField] private float idleWaitTimeMax;
    private float idleWaitTime;

    [SerializeField] private float normalVolume;
    [SerializeField] private float loudVolume;

    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;

    [Header("Vocalizations")]
    [SerializeField] private AudioClip[] spotBarks;
    [SerializeField] private AudioClip[] idleClips;
    [SerializeField] private AudioClip[] lostClips;
    [SerializeField] private AudioClip[] fallClips;
    [SerializeField] private AudioClip[] donutClips;

    [SerializeField] private AudioClip[] susClips;

    [SerializeField] private AudioClip[] laserAlertClips;
    [SerializeField] private AudioClip[] cameraAlertClips;
    [SerializeField] private AudioClip smokebombAlertClip;

    [Header("Footsteps")]
    [SerializeField] private AudioClip[] walkClips;
    [SerializeField] private AudioClip[] runClips;

    [Header("Attacking")]
    [SerializeField] private AudioClip taserFireClip;
    [SerializeField] private AudioClip[] meleeClips;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip[] hitPlayerClips;

    [Header("Other")]
    [SerializeField] private AudioClip chewingClip;
    [SerializeField] private AudioClip hitClip;


    #region Movement


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


    #endregion Movement


    #region Alerts


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
    public void CameraSpot()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, cameraAlertClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(cameraAlertClips[i]);

    }//END LaserSpot

    //-----------------------//
    public void SmokeBombSpot()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(smokebombAlertClip);

    }//END LaserSpot

    //-----------------------//
    public void LaserSpot()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, laserAlertClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(laserAlertClips[i]);

    }//END LaserSpot


    #endregion Alerts


    #region Donut


    //-----------------------//
    public void DonutSpotted()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, donutClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(donutClips[i]);

    }//END DonutSpotted

    //-----------------------//
    public void Chew()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(chewingClip);

    }//END Chew


    #endregion Donut


    #region Combat


    //-----------------------//
    public void TaserFired()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(taserFireClip);

    }//END TaserFired

    //-----------------------//
    public void MeleeHit()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        int i = Random.Range(0, meleeClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(meleeClips[i]);

    }//END MeleePunch

    //-----------------------//
    public void ReloadTaser()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(reloadClip);

    }//END ReloadTaser

    //-----------------------//
    public void HitPlayer()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, hitPlayerClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(hitPlayerClips[i]);

    }//END HitPlayer

    //-----------------------//
    public void Fall()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, fallClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(fallClips[i]);

    }//END Fall


    #endregion Combat


    //-----------------------//
    public void LostPlayer()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, lostClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(lostClips[i]);

    }//END LostPlayer

    //-----------------------//
    public IEnumerator IIdleBark()
    //-----------------------//
    {
        idleWaitTime = Random.Range(idleWaitTimeMin, idleWaitTimeMax);

        yield return new WaitForSeconds(idleWaitTime);

        guardSource.volume = normalVolume;
        int i = Random.Range(0, idleClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(idleClips[i]);

        StartCoroutine(IIdleBark());

    }//END IIdleBark

    //-----------------------//
    public void Suspicious()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, susClips.Length);
        guardSource.pitch = Random.Range(pitchMin, pitchMax);
        guardSource.PlayOneShot(susClips[i]);
    }//END Suspicious


}//END GuardAudio
