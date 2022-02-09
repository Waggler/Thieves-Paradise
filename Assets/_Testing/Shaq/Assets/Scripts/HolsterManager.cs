using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolsterManager : MonoBehaviour
{
    #region Variables

    [Header("Object Pooling Management")]

    [Tooltip("List of objects that are currently spawend / pooled")]
    private List<GameObject> pooledObjects;

    [Tooltip("The object that is being pooled")]
    private GameObject objectToPool;

    [Tooltip("Maximum number of taser projectiles per guard instance")]
    private int objectPoolCap;


    #endregion Variables

    #region Awake / Start / Update
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion Awake / Start / Update
}
