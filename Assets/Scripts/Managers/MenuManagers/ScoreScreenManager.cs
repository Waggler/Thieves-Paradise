using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class ScoreScreenManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private Animator scorePanelAnimator;
    [SerializeField] private Button continueButton;
    public int nextSceneIndex;

    [Header("Layout Groups")]
    [SerializeField] private GridLayoutGroup bonusGroup;
    [SerializeField] private VerticalLayoutGroup deductionGroup;

    [Header("Text")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text collectableText;
    [SerializeField] private TMP_Text heistText;
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text rankText;

    [Header("Data")]
    [SerializeField] private int totalScore;
    [SerializeField] private int totalBonus = 0;
    [SerializeField] private int totalDeduction = 0;
    [SerializeField] private Rank rank;

    [Header("Data Lists")]
    public List<ScoreData> scoreDataReports;

    public List<ScoreData> timeDataReports;
    public List<ScoreData> collectablesDataReports;
    public List<ScoreData> itemsDataReports;
    public List<ScoreData> bonusDataReports;
    public List<ScoreData> deductionsDataReports;

    public enum Rank
    {
        FELONYFELLA = 8001,
        MASTEROFMISAPPOPRIATION = 6501,
        LARCENOUSLOOTER = 5001,
        SNEAKYSHOPLIFTER = 3501,
        NINJANABBER = 2001,
        STEALYSTOOGE = 1001,
        BUMBLINGBURGLAR = 0,
    }

    //-----------------------//
    void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    private void Awake()
    {
    }

    void Init()
    {

        if (levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

    }//END Init

    //-----------------------//
    public void StartScoring()
    //-----------------------//
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        scorePanelAnimator.SetBool("isOpen", true);

        CalculateScore();
        ScoreTime();
        ScoreCollectables();
        ScoreItems();
        ScoreBonus();
        ScoreDeductions();
        OutputScore();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(continueButton.gameObject);

    }//ErankText;ND AddScore

    //-----------------------//
    public void ContinueGame()
    //-----------------------//
    {
        Time.timeScale = 1;
        levelManager.ChangeLevel(nextSceneIndex);


    }//END ContinueGame

    public void ReportScore(ScoreData scoreData)
    {
        scoreDataReports.Add(scoreData);
    }


    private void CalculateScore()
    {
        foreach (ScoreData scoreData in scoreDataReports)
        {
            switch (scoreData.scoreType)
            {
                case ScoreType.TIME:
                    timeDataReports.Add(scoreData);
                    break;
                case ScoreType.COLLECTABLES:
                    collectablesDataReports.Add(scoreData);
                    break;
                case ScoreType.ITEMS:
                    itemsDataReports.Add(scoreData);
                    break;
                case ScoreType.BONUS:
                    bonusDataReports.Add(scoreData);
                    break;
                case ScoreType.DEDUCTIONS:
                    deductionsDataReports.Add(scoreData);
                    break;
                default:
                    break;
            }
        }
    }


    private void ScoreTime()
    {
        foreach (ScoreData scoreData in timeDataReports)
        {
            int timeScoreTotal;

            if (500 - scoreData.scoreAmount > 0)
            {
                timeScoreTotal = 20 * (240 - scoreData.scoreAmount);
            }
            else
            {
                timeScoreTotal = 0;
            }

            timeText.text = (" +" + timeScoreTotal.ToString());

            totalScore += timeScoreTotal;
        }

        return;
    }

    private void ScoreCollectables()
    {
        int collectablesScoreTotal = 0;

        foreach (ScoreData scoreData in collectablesDataReports)
        {
            collectablesScoreTotal += 500;
        }

        collectableText.text = (" +" + collectablesScoreTotal.ToString());

        totalScore += collectablesScoreTotal;
    }

    private void ScoreItems()
    {
        int itemsScoreTotal = 0;

        foreach (ScoreData scoreData in itemsDataReports)
        {
            itemsScoreTotal += 500;
        }

        heistText.text = (" +" + itemsScoreTotal.ToString());
        totalScore += itemsScoreTotal;
    }

    private void ScoreBonus()
    {
        foreach (ScoreData scoreData in bonusDataReports)
        {

            TMP_Text tempText = Instantiate(scorePrefab, bonusGroup.transform).GetComponent<TMP_Text>();
            tempText.text = (scoreData.bonusName.ToString() + ": +" + scoreData.scoreAmount);
            totalBonus += scoreData.scoreAmount;
        }
    }

    private void ScoreDeductions()
    {
        int tasedDeductionTotal = 0;
        int alertedDeductionTotal = 0;
        int tripWireDeductionTotal = 0;

        foreach (ScoreData scoreData in deductionsDataReports)
        {
            if (scoreData.bonusName == "Alert")
            {
                if (alertedDeductionTotal < 3000)
                {
                    alertedDeductionTotal += 250;
                }
                else
                {
                    alertedDeductionTotal = 3000;
                }
            }
            else if (scoreData.bonusName == "Tased")
            {
                tasedDeductionTotal += 500;
            }
            else if (scoreData.bonusName == "TripWire")
            {
                tripWireDeductionTotal = 500;
            }
        }

        TMP_Text tempText = Instantiate(scorePrefab, deductionGroup.transform).GetComponent<TMP_Text>();
        tempText.text = ("Tased: -" + tasedDeductionTotal);

        TMP_Text tempText2 = Instantiate(scorePrefab, deductionGroup.transform).GetComponent<TMP_Text>();
        tempText2.text = ("Alerted: -" + alertedDeductionTotal);

        TMP_Text tempText3 = Instantiate(scorePrefab, deductionGroup.transform).GetComponent<TMP_Text>();
        tempText3.text = ("Trip Wire: -" + tripWireDeductionTotal);

        totalDeduction = (alertedDeductionTotal + tasedDeductionTotal + tripWireDeductionTotal) * -1;
    }

    private void OutputScore()
    {
        totalScore += totalBonus + totalDeduction;

        totalScoreText.text = totalScore.ToString();

        if (totalScore > (int)Rank.FELONYFELLA)
        {
            rank = Rank.FELONYFELLA;
            rankText.text = ("Felony Fella");
        }
        else if (totalScore > (int)Rank.MASTEROFMISAPPOPRIATION)
        {
            rank = Rank.MASTEROFMISAPPOPRIATION;
            rankText.text = ("Master of Misappropriation");
        }
        else if (totalScore > (int)Rank.LARCENOUSLOOTER)
        {
            rank = Rank.LARCENOUSLOOTER;
            rankText.text = ("Larcenous Looter");
        }
        else if (totalScore > (int)Rank.SNEAKYSHOPLIFTER)
        {
            rank = Rank.SNEAKYSHOPLIFTER;
            rankText.text = ("Sneaky Shoplifter");
        }
        else if (totalScore > (int)Rank.NINJANABBER)
        {
            rank = Rank.NINJANABBER;
            rankText.text = ("Ninja Nabber");
        }
        else if (totalScore > (int)Rank.STEALYSTOOGE)
        {
            rank = Rank.STEALYSTOOGE;
            rankText.text = ("Stealy Stooge");
        }
        else if (totalScore > (int)Rank.BUMBLINGBURGLAR)
        {
            rank = Rank.BUMBLINGBURGLAR;
            rankText.text = ("Bumbling Burglar");
        }
    }
}//END ScoreScreenManager
