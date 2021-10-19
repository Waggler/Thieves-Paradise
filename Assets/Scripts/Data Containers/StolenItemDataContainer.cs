using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StolenItemDataContainer : MonoBehaviour
{
    [Header("Data")]
    public string itemName;
    public string itemDescription;

    public Image itemImage;

    //-------------------------//
    public StolenItemDataContainer() { }
    //-------------------------//

    //Filled Constructor
    //-------------------------//
    public StolenItemDataContainer(string _itemName, string _itemDescription, Image _itemImage)
    //-------------------------//
    {
        itemName = _itemName;
        itemDescription = _itemDescription;
        itemImage = _itemImage;

    }// END FacilityOptionData

}//END StolenItemDataContainer
