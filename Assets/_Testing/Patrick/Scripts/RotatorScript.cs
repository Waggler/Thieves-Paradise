using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform me;
    void Start()
    {
        me = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        me.Rotate(Vector3.one * Time.deltaTime * 10, Space.Self);
        //transform.Rotate(Vector3.one * Time.deltaTime * 100, Space.Self);
    }
}
