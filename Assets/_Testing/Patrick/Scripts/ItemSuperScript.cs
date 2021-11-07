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
    public string itemName
    {
        get;
        set;
    }
    void Awake()
    {
        var outline = gameObject.AddComponent<Outline>();

        outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 5f;
        outline.enabled = false;
    }

    private bool isThrown;
    private float timeCounter;

    void Start()
    {

    }
    void Update()
    {
        if (isThrown  && timeCounter > 0)
        {
            timeCounter -= Time.deltaTime;
        }else
        {
            isThrown = false;
            timeCounter = 5;
        }
    }
    public void Pickup()
    {
        print("Pickup Item");
    }
    public void Throw()
    {
        print("Throw Item");
        isThrown = true;
    }

    public void UseDurability()
    {
        if (durability > 0)
        {
            print("Used Item");
            durability--; //decriment durability
        } else
        {
            print("Item Broke");
            Destroy(this.gameObject);
        }
    }

    public void HighlightItem()
    {

    }
    private void MakeNoise()
    {
        //Alert Guards here
    }

    void OnCollisionEnter(Collision other)
    {
        if (isThrown)
        {
            MakeNoise();
            UseDurability();
        }
    }
}
