using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    private GameObject myPrefab;//reference to this object's prefab
    [SerializeField] private float noiseRadius = 15;

    [SerializeField] private GameObject deployableObject;
    [SerializeField] private AudioClip hitSFX;
    [SerializeField] private float SFXVolume = 1;
    [SerializeField] private bool isObjectiveItem;
    [SerializeField] private float UIScale = 1f;

    public bool isKeyItem
    {
        get {return isObjectiveItem;}
        set {isObjectiveItem = value;}
    }

    public GameObject myself
    {
        get {return myPrefab;}
        set {myPrefab = value;}
    }
    public string itemName
    {
        get {return objectName;}
        set {objectName = value;}
    }
    public float UIScalar
    {
        get{return UIScale;}
        set{UIScale = value;}
    }
    // Start is called before the first frame update
    void Start()
    {
        durability = itemDurability;
        itemName = objectName;
        
        if (noiseRadius != 0)
            thrownNoiseRadius = noiseRadius;

        myself = this.gameObject;
        myself.name = itemName;

        hitSoundClip = hitSFX;
        volumeLevel = SFXVolume;
    }

    // Update is called once per frame
    public void UseItem()
    {
        //print("Used Active Item");
    }
    public void UseItemEnd()
    {
        print("Stopped Using Active Item");
    }

    void OnCollisionEnter(Collision other)
    {
        //print(this.gameObject.name + " hit something " + isThrown + " " + durability);

        if (isThrown)
        {
            MakeNoise();
        }

        if (durability < 2 && deployableObject != null && isThrown)
        {
            //print("spawning object");
            Instantiate(deployableObject, transform.position, Quaternion.identity);
            UseDurability();
            Destroy(this.gameObject);
        }else if (isThrown)
        {
            UseDurability();
            isThrown = false;
        }
        
    }   
}
