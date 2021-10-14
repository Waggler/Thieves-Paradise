using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveDataContainer : MonoBehaviour
{
    [Header("Save Data")]
    public string saveName;
    public string saveDate;
    public string saveTime;

    //Empty Constructor
    //-------------------------//
    public SaveDataContainer() { }
    //-------------------------//

    //Filled Constructor
    //-------------------------//
    public SaveDataContainer(string _saveName, string _saveData, string _saveTime)
    //-------------------------//
    {
        saveName = _saveName;
        saveDate = _saveData;
        saveTime = _saveTime;

    }// END SaveDataContainer
}
