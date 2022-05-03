using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePickUp : MonoBehaviour
{
    [SerializeField] private AudioManager aManager;
    public string keyName;

    [SerializeField] private ScoreScreenManager scoreScreenManager;

    void Awake()
    {
        if  (scoreScreenManager == null)
        {
            scoreScreenManager = FindObjectOfType<ScoreScreenManager>();
        }

        if (aManager == null)
        {
            aManager = FindObjectOfType<AudioManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            PlayerPrefs.SetInt(keyName, 1);
            aManager.PlayCollectiblePickup();
            ScoreData scoreData = new ScoreData(ScoreType.COLLECTABLES, 0, null);
            scoreScreenManager.ReportScore(scoreData);
            Destroy(this.gameObject);
        }
    }
}
