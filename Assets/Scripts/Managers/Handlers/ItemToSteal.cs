using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemToSteal : MonoBehaviour
{
    public TMP_Text nameText;
    public string itemName;
    public ItemScript item;

    //-----------------------//
    public void Init(ItemScript target)
    //-----------------------//
    {
        item = target;
        name = item.name;
        nameText.text = item.name;


    }//END Init


}
