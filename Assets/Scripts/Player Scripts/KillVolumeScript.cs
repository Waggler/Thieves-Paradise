using UnityEngine;

public class KillVolumeScript : MonoBehaviour
{
    // Public Variables
    [SerializeField] private GameObject RespawnLocation;

    //public GameObject KillVolume;
    public void OnTriggerEnter(Collider other)
    {
            other.gameObject.transform.position = RespawnLocation.transform.position;

            Debug.Log("You have fallen");
    }
}
