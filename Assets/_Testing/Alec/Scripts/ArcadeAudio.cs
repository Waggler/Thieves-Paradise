using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ArcadeAudio : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource arcadeSource;

    [SerializeField] private float noiseLevelOneVolume;
    [SerializeField] private float noiseLevelTwoVolume;
    [SerializeField] private float noiseLevelThreeVolume;

    [SerializeField] private float pitchMin;
    [SerializeField] private float pitchMax;

    [Header("Trigger")]
    [SerializeField] private AudioClip triggerClip;


    //-----------------------//
    public void OnTriggerEnter(Collider other)
    //-----------------------//
    {
        arcadeSource.volume = noiseLevelTwoVolume;
        arcadeSource.pitch = Random.Range(pitchMin, pitchMax);
        arcadeSource.PlayOneShot(triggerClip);
    }//END Trigger

}
