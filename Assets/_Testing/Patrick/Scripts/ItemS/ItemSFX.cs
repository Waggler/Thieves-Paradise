using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSFX : MonoBehaviour
{
    private AudioSource aud;
    public AudioClip audClip;
    public float vol = 1;
    
    // Start is called before the first frame update
    void Awake()
    {
        aud = this.gameObject.AddComponent<AudioSource>();
    }

    public IEnumerator PlaySound()
    {
        aud.clip = audClip;
        aud.volume = vol;
        aud.Play();
        yield return new WaitForSeconds(audClip.length);
        Destroy(this.gameObject);
    }
}
