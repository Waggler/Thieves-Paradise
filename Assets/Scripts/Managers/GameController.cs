using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public static GameController gameControllerInstance;

    public Vector3 lastCheckPoint;

    [SerializeField]private Transform playerTransform;

    public ItemInterface[] savedInventory;

    public bool SAVE;

    private void Awake()
    {
        if (gameControllerInstance == null)
        {
            gameControllerInstance = this;
            DontDestroyOnLoad(gameControllerInstance);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }
    public void Update()
    {
        if (playerTransform == null)
        {
            playerTransform = FindObjectOfType<PlayerMovement>().gameObject.transform;
        }

        if (gameControllerInstance.lastCheckPoint == new Vector3(0,0,0))
        {
            gameControllerInstance.lastCheckPoint = playerTransform.position;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SaveCheckPoint();
        }

        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            LoadCheckPoint();
        }
    }


    public void SaveCheckPoint()
    {
        gameControllerInstance.lastCheckPoint = playerTransform.position;
        //gameControllerInstance.savedInventory = playerTransform.GetComponent<InventoryController>().SaveInventory();
    }

    public void LoadCheckPoint()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
