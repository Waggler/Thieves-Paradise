using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator sceneTransition;
    [SerializeField] private float waitTime;

    int currentSceneIndex;


    //-----------------------//
    void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    //-----------------------//
    void Init()
    //-----------------------//
    {
        currentSceneIndex = PlayerPrefs.GetInt("currentScene");
        sceneTransition.SetBool("isClosing", true);
        sceneTransition.SetBool("isClosing", true);


    }//END Init

    //-----------------------//
    public void ChangeLevel(int sceneIndex)
    //-----------------------//
    {
        StartCoroutine(IChangeScene());
        currentSceneIndex = sceneIndex;

    }//END NextLevel

    //-----------------------//
    public IEnumerator IChangeScene()
    //-----------------------//
    {
        sceneTransition.SetBool("isClosing", true);

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(currentSceneIndex);

    }//END IChangeScene



}//END LevelManager
