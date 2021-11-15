using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimatorScript : MonoBehaviour
{
    public Animator anim;
    public EnemyManager enemyManager;
    private void Update()
    {
        if (enemyManager.stateMachine == EnemyManager.EnemyStates.PASSIVE)
        {
            anim.SetBool("isPassive", true);
            anim.SetBool("isSuspicious", false);
            if (enemyManager.waitTime < 5 && enemyManager.waitTime > 0)
            {
                anim.SetBool("isSearching", true);
            }
            else
            {
                anim.SetBool("isSearching", false);
            }
        }
        else if (enemyManager.stateMachine == EnemyManager.EnemyStates.SUSPICIOUS)
        {
            anim.SetBool("isPassive", false);
            anim.SetBool("isSuspicious", true);
        }

    }


}

