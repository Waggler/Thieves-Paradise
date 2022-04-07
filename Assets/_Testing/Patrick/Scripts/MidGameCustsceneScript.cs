using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGameCustsceneScript : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float[] cameraTimings;

    private bool hasPlayedCutscene; //only trigger once
    private bool ErrorCheck = false;

    private GameObject player;

    void Start()
    {
        if (waypoints.Length != cameraTimings.Length)
        {
            ErrorCheck = true;
            hasPlayedCutscene = true;
            Debug.Log("ERROR: Array Lengths Don't Match!");
        }
    }

    private IEnumerator StartCutscene()
    {
        hasPlayedCutscene = true;
        //change camera to cutscene cam

        //restrict player movement

        //make player invulnerable

        //cycle through cutscene cameras
        for (int i = 0; i < waypoints.Length; i++)
        {
            yield return new WaitForSeconds(cameraTimings[i]);
        }

        //reset camera

        //reset player movement and vulnerability
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasPlayedCutscene && other.tag == "Player")
        {
            player = other.gameObject;
            StartCoroutine(StartCutscene());
        }else if (ErrorCheck)
        {
            Debug.Log("ERROR: Array Lengths Don't Match!");
        }
    }
}
