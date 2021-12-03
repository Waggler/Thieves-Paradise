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
        sceneTransition.SetBool("isClosing", false);

    }//END Init

    //-----------------------//
    public void PlayGame()
    //-----------------------//
    {
        currentSceneIndex++;
        PlayerPrefs.SetInt("currentScene", currentSceneIndex);
        StartCoroutine(INextLevel());

    }//END PlayGame

    //-----------------------//
    public void MainMenu()
    //-----------------------//
    {
        StartCoroutine(IMainMenu());


    }//END MainMenu

    //-----------------------//
    public void NextLevel()
    //-----------------------//
    {
        StartCoroutine(INextLevel());
        currentSceneIndex++;

    }//END NextLevel

    //-----------------------//
    public IEnumerator IMainMenu()
    //-----------------------//
    {
        sceneTransition.SetBool("isClosing", true);

        yield return new WaitForSeconds(waitTime);

        PlayerPrefs.SetInt("currentScene", 0);
        SceneManager.LoadScene(0);

    }//END IMainMenu

    //-----------------------//
    public IEnumerator INextLevel()
    //-----------------------//
    {
        sceneTransition.SetBool("isClosing", true);

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(currentSceneIndex);

    }//END INextLevel



}//END LevelManager
