using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitItemScript : ItemSuperScript, ItemInterface
{
    [SerializeField] private int itemDurability;
    [SerializeField] public string objectName;
    [SerializeField] public GameObject myPrefab;//reference to this object's prefab
    [SerializeField] private float noiseRadius = 5;
    [SerializeField] private float baitRadius = 10;
    private float baitInterval = 0.5f; //how often the item tries to lure enemies
    private float timer;

    public bool DebugMode;

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

    private SuspicionManager alertManager;
    // Start is called before the first frame update
    void Start()
    {
        durability = itemDurability;
        itemName = objectName;
        
        if (noiseRadius != 0)
            thrownNoiseRadius = noiseRadius;
        
        alertManager = (SuspicionManager)FindObjectOfType(typeof(SuspicionManager));
    }

    // Update is called once per frame
    void Update()
    {
        if (isThrown && timer < 0)
        {
            //only lure once every 5 seconds
            LurePigs();
            timer = baitInterval;
        }else if (isThrown)
        {
            timer -= Time.deltaTime;
        }
    }

    private void LurePigs()
    {
        alertManager.AlertGuards(transform.position, transform.position, baitRadius);
    }

    private void LurePigsAlt()
    {
        //alertManager.AlertGuards(transform.position, transform.position, baitRadius);


        if (DebugMode) print("Looking for enemies");

        //grab everything within a radius
        List<Collider> hitColliders = new List<Collider>(Physics.OverlapSphere(transform.position, baitRadius));

        int layer = 1 << LayerMask.NameToLayer("Default");
        foreach (Collider i in hitColliders.ToArray())
        {
            if (i.tag != "Guard")
            {
                //trim everything that isn't a guard
                hitColliders.Remove(i);
                hitColliders.TrimExcess();
            }else if (Physics.Linecast(transform.position, i.transform.position, layer, QueryTriggerInteraction.Ignore))
            {
                //trim guards that can't see the donut
                hitColliders.Remove(i);
                hitColliders.TrimExcess();

                Debug.DrawLine(transform.position, i.transform.position, Color.red, 1f);
            }else
            {
                Debug.DrawLine(transform.position, i.transform.position, Color.green, 2f);
            }
            
        }

        //calculate distance
        float minDistance = baitRadius;
        EnemyManager enemy = hitColliders[0].gameObject.GetComponent<EnemyManager>();
        foreach (Collider guard in hitColliders)
        {
            float distance = Vector3.Distance(guard.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                enemy = guard.GetComponent<EnemyManager>();
            }
        }
        
        //finally lure in the guard
        enemy.Alert(transform.position);
        //set the timer really high so that more guards aren't alerted unless something happens
        timer = 10;
    }

    public void UseItem()
    {
        //no use;
    }
    public void UseItemEnd()
    {
        //no use;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, baitRadius);
    }
}
