using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhotoScreenManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject photoScreen;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private Image logoImage;

    [SerializeField] private Sprite[] logoSprites;

    [SerializeField] private TMP_Text tagText;

    [SerializeField] private string[] tagStrings;

    //-----------------------//
    public void RandomizeTag()
    //-----------------------//
    {
        int randomLogo = Random.Range(0, logoSprites.Length);
        int randomTag = Random.Range(0, tagStrings.Length);

        logoImage.sprite = logoSprites[randomLogo];
        tagText.text = tagStrings[randomTag];

    }//END RandomizeTag

    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {
        pauseMenu.SetActive(true);
        photoScreen.SetActive(false);

    }//END ChangeScreen


}//END PhotoScreenManager
