using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointofEntryManager : MonoBehaviour
{
    #region Components


    [Header("Components")]
    [SerializeField] private Button entryPoint1;
    [SerializeField] private Button entryPoint2;

    public enum Mission
    {
        LADY,
        UNION,
        MASSES,
        MAFIA,
        CIA,
        VOICES
    }

    public Mission currentMission;


    #endregion Components


    #region Methods


    //-----------------------//
    void Init()                     
    //-----------------------//
    {
        switch (currentMission)                 //TODO Refactor and adjust buttons based on level entry points
        {
            case (Mission.LADY):
                entryPoint1.interactable = true;
                entryPoint2.interactable = false;


                break;
            case (Mission.UNION):
                entryPoint1.interactable = false;
                entryPoint2.interactable = true;

                break;
            case (Mission.MASSES):
                entryPoint1.interactable = false;
                entryPoint2.interactable = false;


                break;
            case (Mission.MAFIA):
                entryPoint1.interactable = false;
                entryPoint2.interactable = false;


                break;
            case (Mission.CIA):
                entryPoint1.interactable = false;
                entryPoint2.interactable = false;


                break;
            case (Mission.VOICES):
                entryPoint1.interactable = false;
                entryPoint2.interactable = false;

                break;
        }


    }//END Init

    //-----------------------//
    public void EntryPoint1()
    //-----------------------//
    {
        Debug.Log("Entry Point 1 chosen.");

    }//END EntryPoint1


    //-----------------------//
    public void EntryPoint2()
    //-----------------------//
    {
        Debug.Log("Entry Point 2 chosen.");

    }//END EntryPoint2


    #endregion Methods


}//END PointofEntryManager
