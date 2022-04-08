using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class MidGameCustsceneScript : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [Tooltip("Ensure that the wait time array is the same length")]
    [SerializeField] private float[] cameraTimings;
    private CinemachineStateDrivenCamera CMStateCam;
    private CinemachineVirtualCamera CMCutsceneCam;
    private Animator anim;
    private PlayerInput playerInput;
    private PlayerMovement movementManager;
    private InventoryVisualController ivc;

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

        //setting up so many references, but at least since it's an item deep in the level this shouldn't matter

        ivc = (InventoryVisualController)FindObjectOfType(typeof(InventoryVisualController));
        

        
        movementManager = (PlayerMovement)FindObjectOfType(typeof(PlayerMovement));
        playerInput = movementManager.gameObject.GetComponent<PlayerInput>();

        CMStateCam = (CinemachineStateDrivenCamera)FindObjectOfType(typeof(CinemachineStateDrivenCamera));

        anim = CMStateCam.gameObject.GetComponent<Animator>();

        CutsceneTarget tempObj = (CutsceneTarget)FindObjectOfType(typeof(CutsceneTarget));
        CMCutsceneCam = tempObj.gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private IEnumerator StartCutscene()
    {
        hasPlayedCutscene = true;

        //change camera to cutscene cam
        ivc.InventoryUI.SetActive(false);
        CMStateCam.GetComponent<CinemachineCollider>().enabled = false;
        anim.Play("Cutscene Camera");

        //stop the player
        movementManager.Movement(Vector3.zero);

        //initalize cutscene camera position
        CMCutsceneCam.transform.position = waypoints[0].transform.position;
        CMCutsceneCam.transform.rotation = waypoints[0].transform.rotation;
        yield return new WaitForSeconds(0.1f);

        //restrict player movement
        playerInput.enabled = false;
        movementManager.enabled = false;

        //make player invulnerable
        movementManager.isInvulnurable = true;

        //cycle through cutscene cameras
        for (int i = 0; i < waypoints.Length; i++)
        {
            CMCutsceneCam.transform.position = waypoints[i].transform.position;
            CMCutsceneCam.transform.rotation = waypoints[i].transform.rotation;

            yield return new WaitForSeconds(cameraTimings[i]);
        }

        //reset camera
        anim.Play("FreeLook");
        ivc.InventoryUI.SetActive(true);

        //reset player movement and vulnerability
        playerInput.enabled = true;
        movementManager.enabled = true;
        movementManager.isInvulnurable = false;
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
