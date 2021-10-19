using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPreferencesHandler : MonoBehaviour
{



    #region Methods


    #region Startup


    //-----------------------//
    private void Start()
    //-----------------------//
    {
        Init();

    }//END Start

    //-----------------------//
    private void Init()
    //-----------------------//
    {
        

    }//END Init



    #endregion Startup

    #region Setters


    //-----------------------//
    public void SetString(string keyName, string value)
    //-----------------------//
    {
        PlayerPrefs.SetString(keyName, value);

    }//END SetString

    //-----------------------//
    public void SetFloat(string keyName, float value)
    //-----------------------//
    {
        PlayerPrefs.SetFloat(keyName, value);

    }//END SetFloat

    //-----------------------//
    public void SetInt(string keyName, int value)
    //-----------------------//
    {
        PlayerPrefs.SetInt(keyName, value);

    }//END SetInt


    #endregion Setters


    #region Getters


    //-----------------------//
    public string GetString(string KeyName)
    //-----------------------//
    {
        return PlayerPrefs.GetString(KeyName);

    }//END GetString



    #endregion Getters


    #endregion Methods


}//END PlayerPreferencesHandler
