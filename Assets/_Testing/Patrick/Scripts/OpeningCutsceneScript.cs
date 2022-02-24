using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OpeningCutsceneScript : MonoBehaviour
{
    [SerializeField] private GameObject CMFreeLookCam;
    [SerializeField] private CinemachineVirtualCamera CMCutsceneCam;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float timeAtWaypoints = 3;

    void Awake()
    {
        //disable main camera
        //CMFreeLookCam.SetActive(false);
        //enable cutscene camera
        CMCutsceneCam.MoveToTopOfPrioritySubqueue();

        //start cutscene coroutine
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        //play transition from loading screen to cutscene

        foreach (GameObject i in waypoints)
        {
            CMCutsceneCam.transform.position = i.transform.position;
            CMCutsceneCam.transform.rotation = i.transform.rotation;
            yield return new WaitForSeconds(timeAtWaypoints);
        }
        //play transition to get back to normal cam

        //when cutscene is over swap back to main cam
        //CMCutsceneCam.SetActive(false);
        //CMFreeLookCam.SetActive(true);
    }

    void OnDrawGizmos()
    {
        foreach (GameObject i in waypoints)
        {
            Gizmos.DrawSphere(i.transform.position, 0.3f);
            Gizmos.DrawRay(i.transform.position, i.transform.forward*2);
        }
    }
}
