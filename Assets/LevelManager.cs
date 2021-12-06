using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator sceneTransition;
    [SerializeField] private float waitTime;

    Scene currentScene;

    int currentSceneIndex;
    int previousSceneIndex;
    int nextSceneIndex;

    bool isRestartingLevel;

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
        isRestartingLevel = false;

        currentScene = SceneManager.GetActiveScene();

        currentSceneIndex = currentScene.buildIndex;

        //sceneTransition.SetBool("isClosing", true);
        //sceneTransition.SetBool("isClosing", false);


        previousSceneIndex = PlayerPrefs.GetInt("previousScene");

    }//END Init

    //-----------------------//
    public void ChangeLevel(int sceneIndex)
    //-----------------------//
    {

        previousSceneIndex = currentSceneIndex;

        PlayerPrefs.SetInt("previousScene", previousSceneIndex);
        nextSceneIndex = sceneIndex;
        StartCoroutine(IChangeScene());

    }//END ChangeLevel

    //-----------------------//
    public void RestartLevel()
    //-----------------------//
    {

        isRestartingLevel = true;
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
        //sceneTransition.SetBool("isClosing", true);

        yield return new WaitForSeconds(waitTime);

        if (isRestartingLevel == true)
        {
            SceneManager.LoadScene(currentSceneIndex);

        }
        else
        {
            SceneManager.LoadScene(nextSceneIndex);

        }

    }//END IChangeScene

}//END LevelManager
