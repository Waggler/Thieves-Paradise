using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAlerter : MonoBehaviour
{
    public float alertRange;

    SuspicionManager susManager;

    void Awake()
    {
        susManager = (SuspicionManager)FindObjectOfType(typeof(SuspicionManager));
    }

    public void AlertNearGuards()
    {
        print("Alerting Guards");
        susManager.AlertGuards(transform.position, transform.position, alertRange);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, alertRange);
    }
}
