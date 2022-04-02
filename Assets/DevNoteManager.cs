using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DevNoteManager : MonoBehaviour
{
    [Header("Components")]

    public Button playFirstButton;



    #region Methods


    //-----------------------//
    public void ChangeScreen()
    //-----------------------//
    {
        // Have to null before reset
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(playFirstButton.gameObject);


    }//END ChangeScreen


    #endregion Methods


}
