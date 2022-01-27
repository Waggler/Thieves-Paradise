using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeistCollectionManager : MonoBehaviour
{


    #region Components


    public enum CurrentHeist
    {
        LADY,
        MASSES,
        UNION,
        MAFIA,
        CIA,
        FINALE
    }

    public CurrentHeist currentHeist;

    [Header("Components")]
    [SerializeField] TMP_Text titleText;
    [Header("Heist 1")]
    [SerializeField] Button collectible1Button;
    [SerializeField] Button collectible2Button;
    [SerializeField] Button collectible3Button;
    [Header("Heist 2")]                 
    [SerializeField] Button collectible4Button;
    [SerializeField] Button collectible5Button;
    [SerializeField] Button collectible6Button;
    [Header("Heist 3")]
    [SerializeField] Button collectible7Button;
    [SerializeField] Button collectible8Button;
    [SerializeField] Button collectible9Button;


    #endregion Components


    #region Methods

    //-----------------------//
    public void Init()
    //-----------------------//
    {

    }//END Init



    #endregion Methods


}//END CLASS HeistCollectionManager
