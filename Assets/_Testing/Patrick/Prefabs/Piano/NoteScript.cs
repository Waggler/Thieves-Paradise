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
        if (transform.GetComponent<PassiveReciever>() == null)
        {
            this.gameObject.AddComponent<PassiveReciever>();
            GetComponent<PassiveReciever>().triggeredEvent.AddListener(PlayNote);
            //GetComponent<PassiveReciever>().triggeredEvent.AddListener(PlayNote);

        }
    }
    
    void Start()
    {
        pitchModifier = Mathf.Pow(1.0594631f,semitones);
    }

    /*void OnTriggerEnter(Collider other)
    {
        PlayNote();
    }

    void OnCollisionCnter(Collision other)
    {
        PlayNote();
    }*/

    public void PlayNote()
    {
        print("Playing Note");
        aud.pitch = pitchModifier;
        aud.Play();
    }
}
