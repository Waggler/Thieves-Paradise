using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimatorScript : MonoBehaviour
{
    public Animator anim;
    public EnemyManager enemyManager;
    public PlayerMovement Player;

    public enum AnimStates
    {

    }

    private void Start()
    {
        if (Player == null)
        {
            Player = FindObjectOfType<PlayerMovement>();
        }
    }

    private void Update()
    {
        #region Old Logic
        //if (enemyManager.stateMachine == EnemyManager.EnemyStates.PASSIVE)
        //{
        //    anim.SetBool("isPassive", true);
        //    anim.SetBool("isSuspicious", false);
        //    anim.SetBool("isHostile", false);
        //    if (enemyManager.patrolWaitTime < 5 && enemyManager.patrolWaitTime > 0)
        //    {
        //        anim.SetBool("isSearching", true);
        //    }
        //    else
        //    {
        //        anim.SetBool("isSearching", false);
        //    }
        //}
        //else if (enemyManager.stateMachine == EnemyManager.EnemyStates.SUSPICIOUS)
        //{
        //    anim.SetBool("isPassive", false);
        //    anim.SetBool("isSuspicious", true);
        //    anim.SetBool("isHostile", false);
        //}
        //else if (enemyManager.stateMachine == EnemyManager.EnemyStates.HOSTILE)
        //{
        //    anim.SetBool("isSuspicious", false);
        //    anim.SetBool("isHostile", true);
        //}
        //else if (enemyManager.stateMachine == EnemyManager.EnemyStates.ATTACK)
        //{
        //    if (Player.IsStunned == false)
        //    {
        //        anim.SetBool("isPlayerFree", true);
        //    }
        //    else
        //    {
        //        anim.SetBool("isPlayerFree", false);
        //    }
        //}
        #endregion Old Logic


    }

    //---------------------------------//
    //Sets the guard animation to "isPassive"
    //Regular standing pose
    public void EnterPassiveAnim()
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
    //---------------------------------//


    //---------------------------------//
    //Sets the guard animation to "isSuspicious"
    //Does the fucking idiot thing where he's wide stanced and looking around all confused n' shit
    public void EnterSusAnim()
    {
        anim.SetBool("isPassive", false);
        anim.SetBool("isSuspicious", true);
        anim.SetBool("isHostile", false);
        anim.SetBool("isPlayerFree", true);
    }
    //---------------------------------//
    

    //---------------------------------//
    //Sets the guard animation to "isHostile"
    //"Animation but hostile"
    //He schmovin'
    public void EnterHostileAnim()
    {
        anim.SetBool("isSuspicious", false);
        anim.SetBool("isHostile", true);
        anim.SetBool("isPlayerFree", true);
    }


    //---------------------------------//
    //Sets the guard animation to attack
    //Rear naked choke
    public void EnterAttackAnim()
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
    //---------------------------------//

    //---------------------------------//
    //Sets the guard animation to walking
    public void EnterWalking()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isPassive", false);
        anim.SetBool("isPlayerFree", true);
    }
    //---------------------------------//

}

