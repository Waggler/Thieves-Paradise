using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkWay : MonoBehaviour
{
    [SerializeField] private float Move;
    private Color DefaultColor = Color.white;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Renderer renderer = GetComponent<Renderer>();

            DefaultColor = renderer.material.color;
            renderer.material.color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.color = DefaultColor;
        }
    }
}
