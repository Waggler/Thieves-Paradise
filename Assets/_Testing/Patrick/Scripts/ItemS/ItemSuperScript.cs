using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSuperScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int durability
    {
        get;
        set;
    }
    /* public string itemName
    {
        get;
        set;
    } */

    public float thrownNoiseRadius
    {
        get;
        set;
    }

    public AudioClip hitSoundClip
    {
        get;
        set;
    }
    public float volumeLevel
    {
        get;
        set;
    }

    public bool isThrown;
    private float timeCounter;
    private SuspicionManager susManager;
    public SfxController sfxController;

    void Awake()
    {
        if (gameObject.GetComponent<Outline>() == null)
        {
            var outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 5f;
            outline.enabled = false;
            GetComponent<MeshRenderer>().material.renderQueue = 1900;
        }
        
        susManager = (SuspicionManager)FindObjectOfType(typeof(SuspicionManager));
        
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ThrowItem()
    {
        isThrown = true;
        //print("throwing item " + isThrown);
    }

    public void UseDurability()
    {
        if (durability > 1)
        {
            //print("Used Item \nDurability: " + durability);
            durability--; //decriment durability
        } else
        {
            //print("Item Broke");
            Destroy(this.gameObject);
        }
    }
    public void MakeNoise()
    {
        //Alert Guards here
        //print("making noise");
        susManager.AlertGuards(transform.position, transform.position, thrownNoiseRadius);

        //create lingering sound object
        GameObject obj = new GameObject("TEMP Item SFX Holder");
        obj.transform.position = transform.position;
        obj.AddComponent<ItemSFX>();
        obj.GetComponent<ItemSFX>().audClip = hitSoundClip;
        obj.GetComponent<ItemSFX>().vol = volumeLevel;
        StartCoroutine(obj.GetComponent<ItemSFX>().PlaySound());
        //object destroys itself after playing the clip

        /* 
        sfxController = FindObjectOfType<SfxController>();
        
        if (sfxController != null)
        {
            AudioSource audio = sfxController.GetComponent<AudioSource>();
            audio.Play();
        }else
        {
            print("SFX Controller Not Found!");
        } */
        
    }

    /* void OnCollisionEnter(Collision other)
    {
        print("Hit something");
        if (isThrown)
        {
            MakeNoise();
            UseDurability();
            isThrown = false;
        }
        //play audio clip of object hitting something
    } */
}
