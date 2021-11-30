using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    Transform me;
    Vector3 modifier;
    void Start()
    {
        me = GetComponent<Transform>();

        //randomize the rotation to make it less boring
        modifier = new Vector3(0,Random.Range(5,10),0);
        me.Rotate(45,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        me.Rotate(modifier * Time.deltaTime);
        //transform.Rotate(Vector3.one * Time.deltaTime * 100, Space.Self);
    }
}
