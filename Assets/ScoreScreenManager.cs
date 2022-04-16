using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private VerticalLayoutGroup scoreItemHolder;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private LevelManager levelManager;

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
    void AddScore()
    //-----------------------//
    {

    }//END AddScore


}//END ScoreScreenManager
