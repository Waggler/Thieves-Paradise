using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class KeycardAudio : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource keycardSource;

    [SerializeField] private AudioClip approvedClip;
    [SerializeField] private AudioClip deniedClip;

    //-----------------------//
    public void ApproveCard()
    //-----------------------//
    {
        keycardSource.PlayOneShot(approvedClip);

    }//END ApproveCard

    //-----------------------//
    public void DenyCard()
    //-----------------------//
    {
        keycardSource.PlayOneShot(deniedClip);

    }//END DenyCard

}//END KeycardAudio
