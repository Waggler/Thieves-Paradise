using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickUp : MonoBehaviour
{
    public string keyName;
    public AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField] private ScoreScreenManager scoreScreenManager;

    void Awake()
    {
        if  (scoreScreenManager == null)
        {
            scoreScreenManager = FindObjectOfType<ScoreScreenManager>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            PlayerPrefs.SetInt(keyName, 1);
            audioSource.PlayOneShot(audioClip);
            ScoreData scoreData = new ScoreData(ScoreType.COLLECTABLES, 0, null);
            scoreScreenManager.ReportScore(scoreData);
            Destroy(this.gameObject);
        }
    }
}
