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
    int previousSceneIndex;


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
        previousSceneIndex = PlayerPrefs.GetInt("previousScene");
        currentSceneIndex = PlayerPrefs.GetInt("currentScene");
        sceneTransition.SetBool("isClosing", true);
        sceneTransition.SetBool("isClosing", false);


    }//END Init

    //-----------------------//
    public void ChangeLevel(int sceneIndex)
    //-----------------------//
    {
        previousSceneIndex = currentSceneIndex;
        PlayerPrefs.SetInt("previousScene", previousSceneIndex);

        StartCoroutine(IChangeScene());
        currentSceneIndex = sceneIndex;

    }//END ChangeLevel

    //-----------------------//
    public void RestartLevel()
    //-----------------------//
    {
        currentSceneIndex = previousSceneIndex;

        StartCoroutine(IChangeScene());

    }//END RestartLevel

    //-----------------------//
    public void QuitGame()
    //-----------------------//
    {

        Debug.Log("Quit Game!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif

    }//END QuitGame

    //-----------------------//
    public IEnumerator IChangeScene()
    //-----------------------//
    {
        sceneTransition.SetBool("isClosing", true);

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(currentSceneIndex);

    }//END IChangeScene



}//END LevelManager
