using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCameramanager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] cameras;
    [SerializeField] private GameObject mainCamera;
    
    [SerializeField] private Image recordingElement;

    [SerializeField] private float flickerTime;
    private float currentFlickerTime;

    private bool isRecordingOn = true;


    private int currentCam;


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    //-----------------------//
    private void Update()
    //-----------------------//
    {
        UpdateCameraPosition();
        UpdateRecordingElement();

    }//END Start

    //-----------------------//
    private void Init()
    //-----------------------//
    {
        //mainCamera = this.gameObject; //In case we want to put this script on the camera itself
        PlaceCamera();

    }//END Init

    //-----------------------//
    private void PlaceCamera()
    //-----------------------//
    {
        currentCam = Random.Range(0, cameras.Length);

        mainCamera.transform.position = cameras[currentCam].transform.position;

    }//END PlaceCamera

    //-----------------------//
    private void UpdateCameraPosition()
    //-----------------------//
    {
        mainCamera.transform.position = cameras[currentCam].transform.position;
        mainCamera.transform.rotation = cameras[currentCam].transform.rotation;

    }//END UpdateCameraPosition

    //-----------------------//
    private void UpdateRecordingElement()
    //-----------------------//
    {
        if (isRecordingOn == true)
        {
            currentFlickerTime -= Time.deltaTime;
            if (currentFlickerTime <= 0)
            {
                currentFlickerTime = flickerTime;

                recordingElement.gameObject.SetActive(false);

                isRecordingOn = false;
            } 
        }
        else if (isRecordingOn == false)
        {
            currentFlickerTime -= Time.deltaTime;
            if (currentFlickerTime <= 0)
            {
                currentFlickerTime = flickerTime;

                recordingElement.gameObject.SetActive(true);

                isRecordingOn = true;
            }
        }


    }//END UpdateRecordingElement


}//END MainMenuCameramanager
