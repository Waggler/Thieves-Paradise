using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] private bool isOpen = false;
    [Range(0,5)]
    [SerializeField] private float verticalMovement = 3;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private GameObject doorParent;

    private bool isMoving;

    void Start()
    {
        if (isOpen)
        {
            StartCoroutine(MoveGate());
        }
    }

    public void OpenGateFromButton()
    {
        isOpen = !isOpen;

        if (!isMoving)
        {   //don't move if it's already moving
            StartCoroutine(MoveGate());
        }
    }

    private IEnumerator MoveGate()
    {
        isMoving = true;
        float t = 0;
        if(isOpen)
        {
            while(doorParent.transform.localPosition.y < verticalMovement)
            {
                yield return new WaitForSeconds(Time.deltaTime);

                doorParent.transform.localPosition = new Vector3(0, Mathf.Lerp(0,verticalMovement,t), 0);

                t += Time.deltaTime * moveSpeed;
            }
        }else
        {
            while(doorParent.transform.localPosition.y > 0)
            {
                yield return new WaitForSeconds(Time.deltaTime);

                doorParent.transform.localPosition = new Vector3(0, Mathf.Lerp(verticalMovement,0,t), 0);

                t += Time.deltaTime * moveSpeed;
            }
        }

        isMoving = false;
    }
}
