using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HeistCollectibleData : MonoBehaviour
{

    [Header("Collectible Info")]
    public string collectibleName;
    public string collectibleDescription;

    public Sprite collectibleSilhouetteSprite;
    public Sprite collectibleSprite;

}//END CLASS HeistCollectibleData
