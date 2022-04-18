using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GuardAudio : MonoBehaviour
{
    [SerializeField] private enum guardType
    {
        BLART,
        HOGG
    }
    [SerializeField] private guardType security;

    [Header("Components")]
    [SerializeField] private AudioSource guardSource;

    [SerializeField] private float idleWaitTimeMin;
    [SerializeField] private float idleWaitTimeMax;
    private float idleWaitTime;

    [SerializeField] private float normalVolume;
    [SerializeField] private float loudVolume;

    [SerializeField] private float blartPitchMin;
    [SerializeField] private float blartPitchMax;
    [SerializeField] private float hoggPitchMin;
    [SerializeField] private float hoggPitchMax;

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
        guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        guardSource.PlayOneShot(walkClips[i]);


    }//END WalkingFootStep

    //-----------------------//
    public void RunningFootStep()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        int i = Random.Range(0, runClips.Length);
        guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
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
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);

        }
        guardSource.PlayOneShot(spotBarks[i]);

    }//END SpotPlayer

    //-----------------------//
    public void CameraSpot()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, cameraAlertClips.Length);
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(cameraAlertClips[i]);

    }//END LaserSpot

    //-----------------------//
    public void SmokeBombSpot()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(smokebombAlertClip);

    }//END LaserSpot

    //-----------------------//
    public void LaserSpot()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, laserAlertClips.Length);
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
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
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(donutClips[i]);

    }//END DonutSpotted

    //-----------------------//
    public void Chew()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(chewingClip);

    }//END Chew


    #endregion Donut


    #region Combat


    //-----------------------//
    public void TaserFired()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(taserFireClip);

    }//END TaserFired

    //-----------------------//
    public void MeleeHit()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        int i = Random.Range(0, meleeClips.Length);
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(meleeClips[i]);

    }//END MeleePunch

    //-----------------------//
    public void ReloadTaser()
    //-----------------------//
    {
        guardSource.volume = loudVolume;
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(reloadClip);

    }//END ReloadTaser

    //-----------------------//
    public void HitPlayer()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, hitPlayerClips.Length);
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(hitPlayerClips[i]);

    }//END HitPlayer

    //-----------------------//
    public void Fall()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, fallClips.Length);
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(fallClips[i]);

    }//END Fall


    #endregion Combat


    //-----------------------//
    public void LostPlayer()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, lostClips.Length);
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
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
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(idleClips[i]);

        StartCoroutine(IIdleBark());

    }//END IIdleBark

    //-----------------------//
    public void Suspicious()
    //-----------------------//
    {
        guardSource.volume = normalVolume;
        int i = Random.Range(0, susClips.Length);
        if (security == guardType.BLART)
        {
            guardSource.pitch = Random.Range(blartPitchMin, blartPitchMax);
        }
        else
        {
            guardSource.pitch = Random.Range(hoggPitchMin, hoggPitchMax);
        }
        guardSource.PlayOneShot(susClips[i]);
    }//END Suspicious


}//END GuardAudio
