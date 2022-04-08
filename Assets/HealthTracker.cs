using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image[] healthImages;

    //-----------------------//
    public void DeductHitPoint(int value)
    //-----------------------//
    {
        if (value == 3)
        {
            healthImages[0].color = Color.white;
            healthImages[1].color = Color.white;
            healthImages[2].color = Color.white;

        }
        if (value == 2)
        {
            healthImages[2].color = Color.grey;
        }
        else if (value == 1)
        {
            healthImages[1].color = Color.grey;
        }
        else
        {
            healthImages[0].color = Color.grey;
        }

    }//END DeductHitPoint

}//END HealthTracker
