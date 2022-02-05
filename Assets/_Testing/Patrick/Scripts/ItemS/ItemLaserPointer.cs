using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLaserPointer : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    private GameObject myPrefab;//reference to this object's prefab
    [SerializeField] private float noiseRadius = 15;

    [SerializeField] private GameObject deployableObject;

    private GameObject heldItemTransform;
    private GameObject cameraTransform;
    private bool isLaserOn = false;

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
    // Start is called before the first frame update
    void Start()
    {
        durability = itemDurability;
        itemName = objectName;
        
        if (noiseRadius != 0)
            thrownNoiseRadius = noiseRadius;

        myself = this.gameObject;
        myself.name = itemName;

        InitLine();
    }

    void Update()
    {
        if (isLaserOn)
        {
            print("Firing my lasers");
            var line = heldItemTransform.GetComponent<LineRenderer>();
            line.SetPosition(0, heldItemTransform.transform.position);
            line.SetPosition(1, cameraTransform.transform.position + (cameraTransform.transform.forward*100) );
        }
    }

    private void InitLine()
    {
        if (heldItemTransform == null || cameraTransform == null) //line already set up so don't do anything
        {
            heldItemTransform = GameObject.FindGameObjectWithTag("HeldItemPosition");
            cameraTransform = Camera.main.gameObject;
        }
        if (heldItemTransform.GetComponent<LineRenderer>() == null)
        {
            heldItemTransform.AddComponent<LineRenderer>();
        } else
        {
            return;
        }
        
        

        var line = heldItemTransform.GetComponent<LineRenderer>();
        //establish line details
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = 0.01f;
        line.endWidth = 0.1f;
        line.positionCount = 2;

        Material randoMaterial = new Material(Shader.Find("Mobile/Particles/Additive"));
        line.material = randoMaterial;
        line.material.SetColor("_Color", Color.red);
        

        print(heldItemTransform.transform.position);

    }

    // Update is called once per frame
    public void UseItem()
    {
        print("Used Laser Pointer Item");
        InitLine();

        var line = heldItemTransform.GetComponent<LineRenderer>();
        line.enabled = true;

        isLaserOn = true; //activate Real-Time Laser calculations

        print(line.GetPosition(0) + " " + line.GetPosition(1));
    }

    public void UseItemEnd()
    {
        print("Stopped using Laser Pointer");
        heldItemTransform.GetComponent<LineRenderer>().enabled = false;
        isLaserOn = false; //deactivate math
    }
}
