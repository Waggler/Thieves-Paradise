using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBillboard : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<Camera>().gameObject.transform;
        }
    }

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = lookRotation;
    }
}
