using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripwireManager : MonoBehaviour
{


    #region Start & Update
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion Start & Update

    #region General Functions
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Vector3.forward);
    }



    #endregion General Functions
}
