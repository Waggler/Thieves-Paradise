using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripwireManager : MonoBehaviour
{

    [SerializeField] [Range (0, 10)]private float rayDistance;
    [SerializeField] private Vector3 initialHitRecord;

    #region Start & Update
    // Start is called before the first frame update
    void Start()
    {
        //Hit instance of the raycast
        RaycastHit hit;

        //Instantiation of the ray
        Ray ray = new Ray(transform.position, transform.forward);

        //Logic for handlin ray collision
        if (Physics.Raycast(ray, out hit))
        {
            //debugging hit
            print(hit.collider.gameObject.name);

            //Use this in an if statement in Update()
            //If it's not the initial hit record then alert guards
            //Include code checkin to see if the laser was tripped by a guard.
            initialHitRecord = hit.point; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Hit instance of the raycast
        RaycastHit hit;

        //Instantiation of the ray
        Ray ray = new Ray(transform.position, transform.forward);

        //Visualization of the made ray (visible in the scene view)
        Debug.DrawRay(transform.position, transform.forward * rayDistance, Color.magenta);


        //Logic for handlin ray collision
        if (Physics.Raycast(ray, out hit))
        {
            //debugging hit
            print(hit.collider.gameObject.name);


            ////if (hit.collider != null  /*Replace with cehckin for the player tag*/)
            //if (hit.collider.gameObject.CompareTag("Player"))
            //{
            //    //Temp code
            //    //Insert guard alert for the location of the hit collider
            //    //hit.collider.enabled = false;
            //    print("I can't see shit, move out of the way.");
            //}

            if (hit.point != initialHitRecord)
            {
                //Checking to see if the hit game object is NOT a guard
                if (!hit.collider.gameObject.CompareTag("Guard"))
                {
                    

                }
            }
        }



    }
    #endregion Start & Update

    #region General Functions
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Gizmos.DrawRay(transform.position, Vector3.forward);
    //}



    #endregion General Functions
}
