using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBillboard : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool isDebug = true;
    [SerializeField] private GameObject self;

    #region Awake
    private void Awake()
    {
        if (target == null)
        {
            target = FindObjectOfType<Camera>().gameObject.transform;
        }
    }

    #endregion


    #region Update
    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = lookRotation;

        if (isDebug == false)
        {
            self.SetActive(false);

            print("I can't see");
        }
    }

    #endregion
}
