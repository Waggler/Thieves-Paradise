using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class OpeningCutsceneScript : MonoBehaviour
{
    [SerializeField] private CinemachineStateDrivenCamera CMStateCam;
    [SerializeField] private CinemachineVirtualCamera CMCutsceneCam;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerInput inputManager;
    [SerializeField] private GameObject[] waypoints;
    [Tooltip("Ensure that the wait time array is the same length")]
    [SerializeField] private float[] waitTimes;
    //[SerializeField] private float timeAtWaypoints = 3;
    private InventoryVisualController ic;
    private bool isPlayingCutscene;

    void Awake()
    {
        if (waitTimes.Length != waypoints.Length)
        {
            Debug.Log("Tempe make sure the wait time array is the same length");
            Application.Quit();
        }

        ic = (InventoryVisualController)FindObjectOfType(typeof(InventoryVisualController));
        ic.SetupCutscene();
        //swap to cutscene camera
        anim.Play("Cutscene Camera");

        //lock player movement
        inputManager.enabled = false;

        //start cutscene coroutine
        StartCoroutine(PlayCutscene());
    }
    // void Update()
    // {
    //     if (isPlayingCutscene)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Space))
    //         {
    //             StopCoroutine(PlayCutscene());
    //             isPlayingCutscene = false;
    //             StartPlay();
    //         }
    //     }
    // }

    public void SkipCutscene(InputAction.CallbackContext context)
    {
        //if (context.started)
        {
            if (isPlayingCutscene)
            {
                StopCoroutine(PlayCutscene());
                isPlayingCutscene = false;
                StartPlay();
            }
        }
    }

    private IEnumerator PlayCutscene()
    {
        isPlayingCutscene = true;
        yield return new WaitForSeconds(0.1f);
        //play transition from loading screen to cutscene
        CMStateCam.GetComponent<CinemachineCollider>().enabled = false;

        int camNum = 1;
        int oldCamNum = 1;

        //yield return new WaitForSeconds(2);

        for(int i = 0; i < waypoints.Length; i++)
        {
            if (!isPlayingCutscene) break;
            //play camera transition noise here
            this.GetComponent<AudioSource>().Play();

            CMCutsceneCam.transform.position = waypoints[i].transform.position;
            CMCutsceneCam.transform.rotation = waypoints[i].transform.rotation;

            while(camNum == oldCamNum)
            {   //prevents the same number showing up twice in a row
                camNum = Random.Range(10,99);
            }
            oldCamNum = camNum;

            ic.camCount.text = "Cam " + camNum;

            if (camNum == 69)
            {
                ic.camCount.text = "Cam " + camNum + " nice";
            }

            yield return new WaitForSeconds(waitTimes[i]);
        }

        isPlayingCutscene = false;
        //when cutscene is over swap back to main cam to start play
        StartPlay();
    }

    void StartPlay()
    {
        CMStateCam.GetComponent<CinemachineCollider>().enabled = true;
        //change back to game camera
        anim.Play("FreeLook");
        //re-enable input
        inputManager.enabled = true;

        ic.ReturnToGameplay();

        this.gameObject.GetComponent<OpeningCutsceneScript>().enabled = false; //disable this script because it's not needed anymore
    }

    void OnDrawGizmos()
    {
        if (waypoints.Length != 0 || waypoints[0] != null)
        {
            foreach (GameObject i in waypoints)
            {
                Gizmos.DrawSphere(i.transform.position, 0.3f);
                Gizmos.DrawRay(i.transform.position, i.transform.forward*2);
            }
        }
    }
}
