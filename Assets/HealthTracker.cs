using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image[] healthImages;
    [SerializeField] private int currentHitpoints = 3;

    //-----------------------//
    public void DeductHitPoint()
    //-----------------------//
    {
        currentHitpoints -= 1;

        if (currentHitpoints == 2)
        {
            healthImages[2].color = Color.grey;
        }
        else if (currentHitpoints == 1)
        {
            healthImages[1].color = Color.grey;
        }
        else
        {
            healthImages[0].color = Color.grey;
        }

    }//END DeductHitPoint

}//END HealthTracker
