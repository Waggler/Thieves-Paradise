using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokebombScript : MonoBehaviour
{
    public float maxRadius;
    public float lifespan;

    [SerializeField] private GameObject occludingSphere;
    private float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (occludingSphere.transform.localScale.x < maxRadius && timer < lifespan)
        {
            occludingSphere.transform.localScale += Vector3.one * Time.deltaTime * 10;
        }else if (timer < lifespan)
        {
            timer += Time.deltaTime;
        }else if (transform.localScale.x > 0.1f)
        {
            transform.localScale -= Vector3.one * Time.deltaTime * 2;
        }else
        {
            Destroy(this.gameObject);
        }
    }
}
