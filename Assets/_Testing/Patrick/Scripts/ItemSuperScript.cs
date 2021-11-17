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
    void Awake()
    {
        if (gameObject.GetComponent<Outline>() == null)
        {
            var outline = gameObject.AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineColor = Color.white;
            outline.OutlineWidth = 5f;
            outline.enabled = false;
        }
        
        susManager = (SuspicionManager)FindObjectOfType(typeof(SuspicionManager));
    }

    private bool isThrown;
    private float timeCounter;
    private SuspicionManager susManager;
    public SfxController sfxController;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ThrowItem()
    {
        isThrown = true;
        print("throwing item " + isThrown);
    }

    public void UseDurability()
    {
        if (durability > 1)
        {
            print("Used Item");
            durability--; //decriment durability
        } else
        {
            print("Item Broke");
            Destroy(this.gameObject);
        }
    }
    private void MakeNoise()
    {
        sfxController = FindObjectOfType<SfxController>();
        //Alert Guards here
        print("making noise");
        susManager.AlertGuards(transform.position, transform.position, thrownNoiseRadius);
        AudioSource audio = sfxController.GetComponent<AudioSource>();
        audio.Play();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isThrown)
        {
            MakeNoise();
            UseDurability();
            isThrown = false;
        }
        //play audio clip of object hitting something
    }
}
