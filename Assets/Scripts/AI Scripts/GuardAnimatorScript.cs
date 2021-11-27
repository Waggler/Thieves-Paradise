using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimatorScript : MonoBehaviour
{
    public Animator anim;
    public EnemyManager enemyManager;
    public PlayerMovement Player;
    private void Start()
    {
        if (Player == null)
        {
            Player = FindObjectOfType<PlayerMovement>();
        }
    }

    private void Update()
    {
        if (enemyManager.stateMachine == EnemyManager.EnemyStates.PASSIVE)
        {
            anim.SetBool("isPassive", true);
            anim.SetBool("isSuspicious", false);
            anim.SetBool("isHostile", false);
            if (enemyManager.patrolWaitTime < 5 && enemyManager.patrolWaitTime > 0)
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
            anim.SetBool("isHostile", false);
        }
        else if (enemyManager.stateMachine == EnemyManager.EnemyStates.HOSTILE)
        {
            anim.SetBool("isSuspicious", false);
            anim.SetBool("isHostile", true);
        }
        else if (enemyManager.stateMachine == EnemyManager.EnemyStates.ATTACK)
        {
            if (Player.IsStunned == false)
            {
                anim.SetBool("isPlayerFree", true);
            }
            else
            {
                anim.SetBool("isPlayerFree", false);
            }
        }

        

    }


}

