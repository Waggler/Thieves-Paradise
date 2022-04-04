using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [Tooltip("Seconds")]
    [SerializeField] private int maxTime = 180;
    [SerializeField] private bool dramaticTimerMode = true;
    private int timer;
    [HideInInspector] public string outputTime;

    [SerializeField] private TextMeshPro timerText;

    void Start()
    {
        //initialize timer
        int minutes;
        int seconds;
        
        minutes = maxTime/60;
        seconds = maxTime - (minutes * 60);

        outputTime = minutes.ToString("00") + ":" + seconds.ToString("00");

        //display outputTime
        timerText.text = "<mspace=mspace=1.5>" + outputTime + "</mspace>";
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(RunTimer());
        }
    }

    private IEnumerator RunTimer()
    {
        timer = maxTime;
        int minutes;
        int seconds;

        float timeMod = 1;

        while(timer > 0)
        {
            if (dramaticTimerMode)
            {
                timeMod = (float)timer / (float)maxTime; //percentage of timer from 0 to 1
                timeMod -= 0.5f; //change range to -0.5 through +0.5
                timeMod *= -1; //make lower time have a positive mod and vice versa
                timeMod /= 2;

                yield return new WaitForSeconds(1 + timeMod);
                //print(1 + timeMod);

                // if (timer > maxTime * 0.66)
                // {
                //     yield return new WaitForSeconds(0.75f);
                // }else if (timer > maxTime * 0.33)
                // {
                //     yield return new WaitForSeconds(1f);
                // }else
                // {
                //     yield return new WaitForSeconds(1.25f);
                // }
            }else
            {
                yield return new WaitForSeconds(1f); //wait for 1 second between ticks
            }
            
            timer --;

            minutes = timer/60;
            seconds = timer - (minutes * 60);

            outputTime = minutes.ToString("00") + ":" + seconds.ToString("00");

            //display outputTime
            timerText.text = "<mspace=mspace=1.5>" + outputTime + "</mspace>";
            //print(outputTime);

            //play ticking sound here
        }

        //insert end of timer logic
        //game over
    }
}
