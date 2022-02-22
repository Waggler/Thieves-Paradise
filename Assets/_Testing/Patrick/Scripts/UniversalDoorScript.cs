using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalDoorScript : MonoBehaviour
{
    public enum doorStates
    {
        [Tooltip("Automatic: Opens without player interaction")]
        Automatic,
        [Tooltip("Unlocked: Opens freely with interaction")]
        Unlocked,
        [Tooltip("Locked: Cannot Open")]
        Locked,
    }

    public doorStates doorMode;
    [SerializeField] private GameObject doorParent;
    [SerializeField] private float maxOpenAngle = 90;

    private Vector3 maxOpenRotation;
    private Vector3 otherPos;
    private bool isOpen;
    private bool moveDirection; //true = forwards, false = backwards
    private float t = 0.5f;

    void Start()
    {
        maxOpenRotation = new Vector3 (0, maxOpenAngle, 0);
    }
    
    void Update()
    {
        if (isOpen)
        {
            if (moveDirection)
            {
                t += Time.deltaTime;
                MoveDoor();
            }else
            {
                t -= Time.deltaTime;
                MoveDoor();
            }
        } else
        {
            
        }
    }

    private void MoveDoor()
    {
        doorParent.transform.rotation = Quaternion.Euler( Vector3.Slerp(-maxOpenRotation, maxOpenRotation, t) );
    }

    public void OpenDoor()
    {

    }

    public void CloseDoor()
    {
        //reset orientation to default
    }
    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player")
        {
            otherPos = other.transform.position;

            //figure out where the player is relative to the door 
            float direction = this.transform.InverseTransformPoint(otherPos).z;
            if (direction > 0)
            {
                moveDirection = true;
            }else
            {
                moveDirection = false;
            }

            if (doorMode == doorStates.Automatic)
            {
                OpenDoor();
            }
        }

        if (other.tag == "Guard")
        {
            //open the door no matter what state it's in
        }
    }

}
