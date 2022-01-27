using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteScript : MonoBehaviour
{
    public float semitones;
    private float pitchModifier;
    public AudioSource aud;
    
    private void Awake() 
    {

    }
    
    void Start()
    {
        pitchModifier = Mathf.Pow(1.0594631f,semitones);
    }

    public void PlayNote()
    {
        //print("Playing Note");
        aud.pitch = pitchModifier;
        aud.Play();
        this.GetComponent<Renderer>().material.color = Color.cyan;
    }

    public void EndNote()
    {
        //print("Stopping Note");
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}
