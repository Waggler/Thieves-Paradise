using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemoetryGoofery : MonoBehaviour
{
    [SerializeField] private float xLength;
    
    [SerializeField] private float zWidth;
    
    [SerializeField] private Vector3 genratedPatrolPoint;

    private Vector3 origin;

    private Vector3 xEndpoint;
    
    private Vector3 zEndpoint;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Limiting myself to this one line of code to figure out the point generation / raycast down and seeing if hit.point is a valid point on the navmesh
        //Random.Range(min.x, max.x)

        //genratedPatrolPoint = GeneratePlanarPoint();

        //Copypasta the generaterandompoint method from the guard manager. It is different from the current GenerateRandomPoint method seen here, and the one found here should probably get renamed
    }


    //Ripped off from EyeballScript
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        origin = transform.position;

        xEndpoint = new Vector3(origin.x + xLength, origin.y, origin.z);

        zEndpoint = new Vector3(origin.x, origin.y, origin.z + zWidth);

        //Drawing the spheres
        Gizmos.color = Color.white;
        //Rectangle Origin
        Gizmos.DrawSphere(origin, 0.5f);

        Gizmos.color = Color.red;
        //Rectangle Length endpoint
        Gizmos.DrawSphere(xEndpoint, 0.2f);

        Gizmos.color = Color.blue;
        //Rectangle Width endpoint
        Gizmos.DrawSphere(zEndpoint, 0.2f);

        Gizmos.color = Color.yellow;
        //Random point visualization
        Gizmos.DrawSphere(genratedPatrolPoint, .5f);


        //Drawing the Rays
        Gizmos.color = Color.red;
        //Length
        Gizmos.DrawRay(origin, Vector3.right * xLength);

        Gizmos.color = Color.blue;
        //Width
        Gizmos.DrawRay(origin, Vector3.forward * zWidth);
    }//End OnDrawGizmos
    #endif

    //Should probably get renamed again
    private Vector3 GeneratePlanarPoint()
    {
        Vector3 calculatedPoint;

        calculatedPoint.x = Random.Range(origin.x, xEndpoint.x);

        calculatedPoint.y = 0;

        calculatedPoint.z = Random.Range(origin.z, zEndpoint.z);

        //print($"Generated point = {calculatedPoint}");

        return calculatedPoint;

        ////Generates the initial random point
        //Vector3 randpoint = Random.insideUnitSphere * randPointRad;

        ////Returns a bool
        ////First portion tests the randomly generated point to see if it can be reached.
        ////Second portion tests the path to the genreated point and to see if it's possible to reach that point
        //if (NavMesh.SamplePosition(randpoint + transform.position, out NavMeshHit hit, randPointRad, 1) && NavMesh.CalculatePath(transform.position, hit.position, NavMesh.AllAreas, path))
        //{
        //    searchLoc = hit.position;
        //    return searchLoc;
        //}
        //else
        //{
        //    print("Point or Path is invalid");
        //    return transform.position;
        //}       
        
        //Generates the initial random point
    }
}
