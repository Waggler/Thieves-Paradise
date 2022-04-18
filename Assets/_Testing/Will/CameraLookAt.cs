using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] private Transform crouchTransform;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private float crouchTime;
    private Transform followTransform;
    private Transform originalPosition;


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        followTransform = this.gameObject.transform;
        originalPosition = this.gameObject.transform;

        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }

    }//END Start

    //-----------------------//
    private void Update()
    //-----------------------//
    {
        if (playerMovement.IsCrouching == true)
        {
            StartCoroutine(IMoveCamDown(crouchTime));
        }
        else if (playerMovement.IsCrouching == false)
        {
            StartCoroutine(IMoveCamUp(crouchTime));
        }

    }//END Update

    //-----------------------//
    private IEnumerator IMoveCamDown(float moveTime)
    //-----------------------//
    {
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            followTransform.position =
                Vector3.MoveTowards(originalPosition.position, crouchTransform.position, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;

        }

        yield return null;

    }//END IMoveCamDown

    //-----------------------//
    private IEnumerator IMoveCamUp(float moveTime)
    //-----------------------//
    {
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            followTransform.position =
                Vector3.MoveTowards(crouchTransform.position, originalPosition.position, elapsedTime / moveTime);
            elapsedTime += Time.deltaTime;

        }

        yield return null;

    }//END IMoveCamUp

}//END CLASS CameraLookAt
