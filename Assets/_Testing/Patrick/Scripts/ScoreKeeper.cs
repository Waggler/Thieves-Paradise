using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private ScoreScreenManager scoreScreenManager;
    public bool guardSawPlayer;
    public bool cameraSawPlayer;
    public bool trippedWire;
    public bool unlockedDoor;//used a keycard to unlock a door
    public bool jumped;
    public bool crouched;
    public int itemUseCount;

    public bool gameisOver;
    
    void Start()
    {
        if (scoreScreenManager == null)
        {
            scoreScreenManager = FindObjectOfType<ScoreScreenManager>();
        }
    }
    public void SendScores()
    {
        gameisOver = true;
        ScoreData score;

        if(!guardSawPlayer && !cameraSawPlayer && !trippedWire)
        {
            score = new ScoreData(ScoreType.BONUS, 3000, "Silent Ninja");
            scoreScreenManager.ReportScore(score);
        }else
        {
            if (!guardSawPlayer)
            {
                score = new ScoreData(ScoreType.BONUS, 1000, "Anti-Social");
                scoreScreenManager.ReportScore(score);
            }
            if (!cameraSawPlayer)
            {
                score = new ScoreData(ScoreType.BONUS, 500, "Camera Shy");
                scoreScreenManager.ReportScore(score);
            }
            if(!trippedWire)
            {
                score = new ScoreData(ScoreType.BONUS, 500, "Cool Cruiser");
                scoreScreenManager.ReportScore(score);
            }
        }

        if(unlockedDoor)
        {
            score = new ScoreData(ScoreType.BONUS, 500, "Security Expert");
            scoreScreenManager.ReportScore(score);
        }
        if(!jumped)
        {
            score = new ScoreData(ScoreType.BONUS, 1500, "Grounded");
            scoreScreenManager.ReportScore(score);
        }
        if(!crouched)
        {
            score = new ScoreData(ScoreType.BONUS, 1000, "Loud & Proud");
            scoreScreenManager.ReportScore(score);
        }

        if (itemUseCount == 0)
        {
            score = new ScoreData(ScoreType.BONUS, 250, "Light Pockets");
            scoreScreenManager.ReportScore(score);
        }else if (itemUseCount > 4)
        {
            score = new ScoreData(ScoreType.BONUS, 450 + (itemUseCount * 10), "Sticky Fingers");
            scoreScreenManager.ReportScore(score);
        }

    }
}
