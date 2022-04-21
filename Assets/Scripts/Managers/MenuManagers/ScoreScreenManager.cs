using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScreenManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private Animator scorePanelAnimator;
    [SerializeField] private int nextSceneIndex;
   
    [Header("Layout Groups")]
    [SerializeField] private GridLayoutGroup bonusGroup;
    [SerializeField] private VerticalLayoutGroup deductionGroup;

    [Header("Text")]
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text collectibletext;
    [SerializeField] private TMP_Text heistText;
    [SerializeField] private TMP_Text totalScoreText;
    [SerializeField] private TMP_Text rankText;



    //-----------------------//
    void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    void Init()
    {

        if(levelManager == null)
        {
            levelManager = FindObjectOfType<LevelManager>();
        }

    }//END Init

    //-----------------------//
    public void AddScore()
    //-----------------------//
    {
        //timeText.text = 
        //collectibletext.text = 
        //heistText.text = 
        //totalScoreText.text = 

        //foreach (scoreThing in scorething)
        //{
        //    TMP_Text tempText = Instantiate(scorePrefab, bonusGroup.transform).GetComponent<TMP_Text>();

        //    tempText.text = scoreThing.ToString();

        //}

        //foreach (scoreThing in scorething)
        //{
        //    TMP_Text tempText = Instantiate(scorePrefab, deductionGroup.transform).GetComponent<TMP_Text>();

        //    tempText.text = scoreThing.ToString();

        //}

        scorePanelAnimator.SetBool("isOpen", true);

    }//ErankText;ND AddScore

    //-----------------------//
    public void ContinueGame(int scene)
    //-----------------------//
    {
        levelManager.ChangeLevel(scene);


    }//END ContinueGame

}//END ScoreScreenManager
