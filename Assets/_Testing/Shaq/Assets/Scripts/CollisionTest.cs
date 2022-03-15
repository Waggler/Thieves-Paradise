using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollisionTest : MonoBehaviour
{
    bool m_Started;
    public LayerMask m_LayerMask;

    //The converted list
    [SerializeField] List<Collider> guardCollisions;

    [SerializeField] [Range(1, 2)]private int selfFloorNumber;

    //Start does start things
    void Start()
    {
        //Use this to ensure that the Gizmos are being drawn when in Play Mode.
        m_Started = true;

    }

    void FixedUpdate()
    {
        MyCollisions();
    }

    void MyCollisions()
    {
        //Use the OverlapBox to detect if there are any other colliders within this box area.
        //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
        guardCollisions = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask).ToList();
        int i = 0;
        //Check when there is a new collider coming into contact with the box
        while (i < guardCollisions.Count)
        {
            //Output all of the collider names
            Debug.Log("Hit : " + guardCollisions[i].name + i);

            //Sets each guard in the list to have the ID number of the floor or some shit, just got back to coding after five days so it's gonna be rough for a bit
            guardCollisions[i].gameObject.GetComponent<EnemyManager>().floorNumber = selfFloorNumber;

            //Increase the number of Colliders in the array
            i++;
        }

    }

    //Draw the Box Overlap as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
