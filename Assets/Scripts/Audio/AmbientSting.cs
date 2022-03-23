using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSting : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] AudioClip ambientStingClip;

    [SerializeField] private AudioSource aSource;

    [SerializeField] private bool isDestroyedAfterEntry;
    [SerializeField] private float stingWaitTime;
    bool isWaiting = false;
    //-----------------------//
    private void OnTriggerEnter(Collider other) //Use this for the player walking into a collider
    //-----------------------//
    {
        if (isWaiting == false)
        {
            if (other.tag == "Player")
            {
                aSource.PlayOneShot(ambientStingClip);

                Debug.Log("Yes");

                if (isDestroyedAfterEntry == true)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        StartCoroutine(IStartWait());
    }//END OnTriggerEnter

    //-----------------------//
    IEnumerator IStartWait()
    //-----------------------//
    {
        isWaiting = true;
        yield return new WaitForSeconds(stingWaitTime);
        isWaiting = false;
        StopAllCoroutines();

    }//END IStartWait



}//END CLASS AmbientSting
