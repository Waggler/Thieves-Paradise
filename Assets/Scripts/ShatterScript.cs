using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterScript : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(IDestroySelf());
    }

    private IEnumerator IDestroySelf()
    {
        yield return new WaitForSeconds(1);

        Destroy(this.gameObject);
    }
}
