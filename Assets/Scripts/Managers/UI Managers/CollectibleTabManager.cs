using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CollectibleTabManager : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private GameObject itemPanel;
    [SerializeField] private GameObject worldPanel;
    [SerializeField] private GameObject notePanel;

    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject itemDescriptionPanel;
    [SerializeField] private GameObject worldDescriptionPanel;
    [SerializeField] private GameObject noteDescriptionPanel;
    [SerializeField] private Button itemButton;
    [SerializeField] private Button magazineButton;
    [SerializeField] private Button noteButton;
    [SerializeField] private Button backButton;



    public void ChangeScreen(int value)
    {
        if (value == 0)
        {
            itemPanel.SetActive(true);
            worldPanel.SetActive(false);
            notePanel.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(itemButton.gameObject);
        }
        if (value == 1)
        {
            itemPanel.SetActive(false);
            worldPanel.SetActive(true);
            notePanel.SetActive(false);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(magazineButton.gameObject);
        }
        if (value == 2)
        {
            itemPanel.SetActive(false);
            worldPanel.SetActive(false);
            notePanel.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(noteButton.gameObject);
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

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(backButton.gameObject);
        }
    }

}//END CLASS CollectibleTabManager
