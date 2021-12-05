using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleAppearScript : MonoBehaviour
{
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.localScale = Vector3.one / 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1f)
        {
            this.gameObject.transform.localScale = Vector3.one * Mathf.Lerp(0,1,timer);
            timer += Time.deltaTime * 5;
        }
    }
}
