using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVFXManager : MonoBehaviour
{
    [SerializeField] private Material hostileVfxMat;

    [SerializeField] private Material susVfxMat;

    [SerializeField] private GameObject guardRef;

    private EnemyManager guardScriptRef;

    [SerializeField] private Material selfMaterial;

    [SerializeField] private GameObject self;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (guardScriptRef.stateMachine == EnemyManager.EnemyStates.HOSTILE || guardScriptRef.stateMachine == EnemyManager.EnemyStates.RANGEDATTACK)
        {
            self.SetActive(true);

            selfMaterial = hostileVfxMat;
        }
        
        if (guardScriptRef.stateMachine == EnemyManager.EnemyStates.SUSPICIOUS)
        {
            self.SetActive(true);

            selfMaterial = susVfxMat;
        }
        
        if (guardScriptRef.stateMachine == EnemyManager.EnemyStates.PASSIVE || guardScriptRef.stateMachine == EnemyManager.EnemyStates.STUNNED)
        {
            self.SetActive(false);
        }
    }


    private void Init()
    {
        self.SetActive(true);

        if (guardScriptRef == null)
        {
            guardScriptRef = guardRef.GetComponent<EnemyManager>();
        }

        if (selfMaterial == null)
        {
            selfMaterial = GetComponent<Material>();
        }
    }
}
