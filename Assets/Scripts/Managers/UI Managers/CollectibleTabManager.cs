using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTabManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private ItemCollectibleData[] itemCollectibleDatas;
    [SerializeField] private LocationCollectibleData[] locationCollectibleDatas;
    [SerializeField] private NoteCollectibleData[] noteCollectibleDatas;

    [SerializeField] private LevelManager levelManager;

    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject worldPanel;
    [SerializeField] private GameObject notePanel;

    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject itemDescriptionPanel;
    [SerializeField] private GameObject worldDescriptionPanel;
    [SerializeField] private GameObject noteDescriptionPanel;



    public void ChangeScreen(int value)
    {
        if (value == 0)
        {
            itemPanel.SetActive(true);
            worldPanel.SetActive(false);
            notePanel.SetActive(false);
        }
        if (value == 1)
        {
            itemPanel.SetActive(false);
            worldPanel.SetActive(true);
            notePanel.SetActive(false);
        }
        if (value == 2)
        {
            itemPanel.SetActive(false);
            worldPanel.SetActive(false);
            notePanel.SetActive(true);
        }
        if (value == 3)
        {
            levelManager.ChangeLevel(0);
        }
        if (value == 4)
        {
            itemDescriptionPanel.SetActive(false);
            worldDescriptionPanel.SetActive(false);
            noteDescriptionPanel.SetActive(false);
            fadePanel.SetActive(false);
        }
    }

}//END CLASS CollectibleTabManager
