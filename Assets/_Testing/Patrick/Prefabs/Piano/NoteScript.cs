using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public float semitones;
    private float pitchModifier;
    public AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        pitchModifier = Mathf.Pow(1.0594631f,semitones);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayNote();
    }

    void OnCollisionCnter(Collision other)
    {
        PlayNote();
    }

    public void PlayNote()
    {
        print("something touched me!");
        aud.pitch = pitchModifier;
        aud.Play();
    }
}
