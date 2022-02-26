using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class OpeningCutsceneScript : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera CMCutsceneCam;
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerInput inputManager;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float timeAtWaypoints = 3;
    private InventoryVisualController ic;
    
    
    

    void Awake()
    {

        ic = (InventoryVisualController)FindObjectOfType(typeof(InventoryVisualController));
        ic.SetupCutscene();
        //swap to cutscene camera
        anim.Play("Cutscene Camera");

        //lock player movement
        inputManager.enabled = false;

        //start cutscene coroutine
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(0.1f);
        //play transition from loading screen to cutscene

        int camNum = 1;
        int oldCamNum = 1;

        //yield return new WaitForSeconds(2);

        foreach (GameObject i in waypoints)
        {
            //play camera transition noise here

            CMCutsceneCam.transform.position = i.transform.position;
            CMCutsceneCam.transform.rotation = i.transform.rotation;

            while(camNum == oldCamNum)
            {   //prevents the same number showing up twice in a row
                camNum = Random.Range(10,99);
            }
            oldCamNum = camNum;

            ic.camCount.text = "Cam " + camNum;
            yield return new WaitForSeconds(timeAtWaypoints);
        }

        //when cutscene is over swap back to main cam to start play
        StartPlay();
    }

    void StartPlay()
    {
        //change back to game camera
        anim.Play("FreeLook");
        //re-enable input
        inputManager.enabled = true;

        ic.ReturnToGameplay();
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
