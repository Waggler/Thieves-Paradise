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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Guard"))
        {
            //Theoretically the guard
            other.gameObject.GetComponent<EnemyManager>().stateMachine = EnemyManager.EnemyStates.STUNNED;

            //References itself (The puddle in this case)
            Destroy(gameObject);
        }
        
    }
}
