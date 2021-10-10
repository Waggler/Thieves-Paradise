// You can also use transform.LookAt

using UnityEngine;
using System.Collections;

public class GhostCamera : MonoBehaviour
{
    public Transform target;

    // Angular speed in radians per sec.
    // public float speed = 1.0f;

    void Update()
    {
        Vector3 relativePos = target.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

        // Determine which direction to rotate towards
        // Vector3 targetDirection = target.position - transform.position;

        // The step size is equal to speed times frame time.
        // float singleStep = speed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        // Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        // Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        // transform.rotation = Quaternion.LookRotation(newDirection);
    }
}