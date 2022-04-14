using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreItemHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ScoreData _data;
    
    [SerializeField] private TMP_Text bonusText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private AudioSource scoreSource;
    [SerializeField] private float scoreScrollSpeed;

    //-----------------------//
    public void Init(ScoreData data)
    //-----------------------//
    {
        _data = data;
        bonusText.text = _data.bonusName;
        scoreText.text = _data.scoreAmount.ToString();

        //StartFlyIn();
        StartCoroutine(IScrollText());

    }//END Init

    //-----------------------//
    private void StartFlyIn()
    //-----------------------//
    {
        StartCoroutine(IScrollText());

    }//END StartFlyIn

    //-----------------------//
    private IEnumerator IScrollText()
    //-----------------------//
    {
        float multiplier = Time.deltaTime / scoreScrollSpeed;
        float scrollScore = 1;

        while (scrollScore <= _data.scoreAmount)
        {
            scrollScore *= multiplier;
            scoreText.text = scrollScore.ToString();
        }
        scrollScore = _data.scoreAmount;
        scoreText.text = scrollScore.ToString();
        
        yield return null;

    }//END IScrollText

    //-----------------------//
    public void PlayAudio()
    //-----------------------//
    {
        scoreSource.Play();
    }//END PlayAudio


}//END CLASS ScoreItemHandler
