using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    [Tooltip("Seconds")]
    [SerializeField] private int maxTime = 180;
    [SerializeField] private bool dramaticTimerMode = true;
    private int timer;
    [HideInInspector] public string outputTime;

    [SerializeField] private TextMeshPro timerText;
    [SerializeField] private AudioClip tick;
    [Tooltip("This scene is loaded when the timer ends")]
    [SerializeField] private string sceneNameToLoad;
    private AudioSource Aud;
    private float pitchMod = 0.2f;

    private bool startedTimer;
    private bool stoppedTimer;
    

    [SerializeField] private TextMeshProUGUI timerUIText;

    void Start()
    {
        //initialize timer
        int minutes;
        int seconds;
        
        minutes = maxTime/60;
        seconds = maxTime - (minutes * 60);

        outputTime = minutes.ToString("00") + ":" + seconds.ToString("00");

        

        Aud = GetComponent<AudioSource>();
        Aud.clip = tick;
        Aud.pitch = 1.2f;

        //GameObject tempObj = GameObject.FindGameObjectWithTag("TimerText");
        //timerUIText = tempObj.GetComponent<TextMeshProUGUI>();

        //timerUIText.gameObject.SetActive(false);

        //display outputTime
        timerText.text = "<mspace=mspace=1.5>" + outputTime + "</mspace>";
        timerUIText.text = "<mspace=mspace=20>" + "" + "</mspace>";
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!startedTimer && other.tag == "Player")
        {
            StartCoroutine(RunTimer());
            startedTimer = true;
            timerUIText.gameObject.SetActive(true);
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
            if(stoppedTimer) break;

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
            timerUIText.text = "<mspace=mspace=16>" + outputTime + "</mspace>";
            //print(outputTime);

            //play ticking sound here
            
            pitchMod *= -1;
            Aud.pitch = 1.2f + pitchMod;
            Aud.Play();
        }

        //insert end of timer logic
        if(!stoppedTimer)
        {
            //game over
            SceneManager.LoadScene(sceneNameToLoad);
        }
        
    }

    public void StopTimer()
    {
        stoppedTimer = true;
    }
}
