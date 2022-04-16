using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera; //pointless?
    //[SerializeField] private PostProcessEffectSettings postProcessMaybe;
    [SerializeField] private AudioSource explosionSource;
    //reference Level/scoremanager and call its func to open up
    [SerializeField] private float waitTime;

    //-----------------------//
    private void Start()
    //-----------------------//
    {
        if (mainCamera = null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }


    }//END Start

    //-----------------------//
    public IEnumerator ITriggerExplosion()
    //-----------------------//
    {
        //do lerpy shit here

        yield return null;
    }//END TriggerExplosion


}//END CLASS ExplosionHandler
