using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDRAudioHandler : MonoBehaviour
{
    [Header("Components")]
    [Header("Audio")]
    [SerializeField] private AudioSource announceSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] announcerClips;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip dDRStinger;
    [Header("Lights")]
    [SerializeField] private Light spotlight;
    [SerializeField] private Color[] lightColor;
    [SerializeField] private float lightWaitTime;

    //---------------------------//
    public void PlayGroovin()
    //---------------------------//
    {
        StopPlaying();

        int i = Random.Range(0, announcerClips.Length);
        int j = Random.Range(0, musicClips.Length);

        announceSource.clip = announcerClips[i];
        musicSource.clip = musicClips[j];

        announceSource.Play();

        spotlight.gameObject.SetActive(true);
        
        StartCoroutine(IGetSchmoovin());
        StartCoroutine(IChangeColor(lightWaitTime));


    }//END PlayGroovin

    //---------------------------//
    private IEnumerator IGetSchmoovin()
    //---------------------------//
    {
        while (announceSource.isPlaying == true)
        {
            yield return new WaitForSeconds(announceSource.clip.length);
        }
        musicSource.Play();
        StopCoroutine(IGetSchmoovin());
        StopCoroutine(IChangeColor(lightWaitTime));

    }//END IGetSchmoovin

    //---------------------------//
    private IEnumerator IChangeColor(float time)
    //---------------------------//
    {
        int i = Random.Range(0, lightColor.Length);
        spotlight.color = lightColor[i];

        yield return new WaitForSeconds(time);
        StartCoroutine(IChangeColor(lightWaitTime));

    }//END IGetSchmoovin


    //---------------------------//
    public void StopPlaying()
    //---------------------------//
    {
        StopAllCoroutines();
        spotlight.gameObject.SetActive(false);

        announceSource.Stop();
        musicSource.Stop();
        announceSource.PlayOneShot(dDRStinger);


    }//END StopPlaying


}//END DDRAudioHandler
