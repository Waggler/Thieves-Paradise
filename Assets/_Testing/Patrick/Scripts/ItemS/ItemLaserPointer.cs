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
    private LayerMask layerMask;

    private bool isObjectiveItem = false;
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

        layerMask = ~(LayerMask.GetMask("Player") + LayerMask.GetMask("Ghost"));

        InitLine();
    }

    

    private void InitLine()
    {
        if (heldItemTransform == null || cameraTransform == null) //line already set up so don't do anything
        {
            heldItemTransform = GameObject.FindGameObjectWithTag("HeldItemPosition");
            cameraTransform = Camera.main.gameObject;
        }
        /*
        if (heldItemTransform.GetComponent<LineRenderer>() == null)
        {
            heldItemTransform.AddComponent<LineRenderer>();
        }*/

        var line = heldItemTransform.GetComponent<LineRenderer>();
        //establish line details
        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = 0.01f;
        line.endWidth = 0.1f;
        line.positionCount = 2;

        Material randoMaterial = new Material(Shader.Find("Mobile/Particles/Additive"));
        line.material = randoMaterial;
        //line.material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    public void UseItem()
    {
        //print("Used Laser Pointer Item");
        //InitLine();

        var line = GetComponent<LineRenderer>();
        line.enabled = true;
        
        isLaserOn = true; //activate Real-Time Laser calculations
        StartCoroutine("FireLaser");
    }

    public void UseItemEnd()
    {
        //print("Stopped using Laser Pointer");
        GetComponent<LineRenderer>().enabled = false;
        isLaserOn = false; //deactivate math
    }
    IEnumerator FireLaser()
    {
        var line = GetComponent<LineRenderer>();

        line.startColor = Color.red;
        line.endColor = Color.red;
        line.startWidth = 0.01f;
        line.endWidth = 0.03f;
        line.positionCount = 2;

        Material randoMaterial = new Material(Shader.Find("Mobile/Particles/Additive"));
        line.material = randoMaterial;

        //print("Firing my lasers");

        RaycastHit hit;

        Vector3 startPoint = Vector3.zero;
        Vector3 endPoint = Vector3.zero;

        while (isLaserOn)
        {
            startPoint = transform.position + transform.forward*0.1f;
            line.SetPosition(0, startPoint);

            endPoint = startPoint + transform.forward*100f;//backup endpoint in case raycast fails for some reason

            /*if (Physics.Raycast(cameraTransform.transform.position, cameraTransform.transform.forward, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
                //first do a cast from the camera to figre out where the player is looking
                endPoint = hit.point;
            }else
            {
                endPoint = cameraTransform.transform.forward * 1000;
            }*/
            endPoint = cameraTransform.transform.forward * 1000;

            if (Physics.Raycast(startPoint, (endPoint-startPoint).normalized, out hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
            {
                //now do a cast from the laser pointer to where the player is looking to figure out if anything is in the way.
                endPoint = hit.point;

                if (hit.transform.gameObject.GetComponent<CameraManager>())
                {
                    print("Hit a camera");
                    durability--;
                    UseItemEnd();
                    //hit.transform.gameObject.GetComponent<CameraManager>().Disable(disableTime);
                }
            }

            line.SetPosition(1, endPoint);

            //print(line.GetPosition(0) + " " + line.GetPosition(1));
            yield return new WaitForSeconds(Time.deltaTime/2);
        }
    }
}
