using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] [Range (1, 30)]private float timeLeft = 1.1f;

    private float timeLeftReset;

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        timeLeftReset = timeLeft;
    }
}
