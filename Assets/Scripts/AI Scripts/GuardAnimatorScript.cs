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

    //The shooting bool may need to be removed from some methods, got lazy and added a bunch of them just in case ~ Shaq

    //---------------------------------//
    //Sets the guard animation to "isPassive"
    //Regular standing pose
    public void EnterPassiveAnim()
    {
        anim.SetBool("isPassive", true);
        anim.SetBool("isSuspicious", false);
        anim.SetBool("isHostile", false);
        anim.SetBool("isShooting", false);
        if (enemyManager.patrolWaitTime < 5 && enemyManager.patrolWaitTime > 0)
        {
            anim.SetBool("isShooting", false);
            anim.SetBool("isSearching", true);
        }
        else
        {
            anim.SetBool("isShooting", false);
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
        anim.SetBool("isShooting", false);
        anim.SetBool("isHostile", false);
    }//End EnterSusAnim


    //---------------------------------//
    public void EnterSearchingAnim()
    {
        anim.SetBool("isShooting", false);
        anim.SetBool("isSearching", true);
    }//End EnterSearchingAnim

    //---------------------------------//
    public void ExitSearchingAnim()
    {
        anim.SetBool("isShooting", false);
        anim.SetBool("isSearching", false);
    }//End ExitSearchingAnim


    //---------------------------------//
    //Sets the guard animation to "isHostile"
    //"Animation but hostile"
    //He schmovin'
    public void EnterHostileAnim()
    {
        anim.SetBool("isSuspicious", false);
        anim.SetBool("isSearching", false);
        anim.SetBool("isHostile", true);
        anim.SetBool("isShooting", false);
        //anim.SetBool("isPlayerFree", true);
    }

    public void EnterStunAnim()
    {
        anim.SetBool("isHostile", false);
        anim.SetBool("isSuspicious", false);
        anim.SetBool("isSearching", false);
        anim.SetBool("isStunned", true);
        anim.SetBool("isShooting", false);

    }


    public void ExitStunAnim()
    {
        anim.SetBool("isUnholster", false);
        anim.SetBool("isStunned", false);
    }

    //---------------------------------//
    //Rear naked choke
    public void EnterAttackAnim()
    {
        anim.SetBool("isAttacking", true);
    }
    //---------------------------------//


    public void ExitAttackAnim()
    {
        anim.SetBool("isAttacking", false);
    }


    //---------------------------------//
    //Sets the guard animation to walking
    public void EnterWalking()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isPassive", false);
        anim.SetBool("isShooting", false);
    }
    //---------------------------------//


    public void SetAgentSpeed(float speed)
    {
        anim.SetFloat("guardSpeed", speed);
    }

    public void EnterShoot()
    {
        anim.SetBool("isShooting", true);
    }

    public void ExitShoot()
    {
        anim.SetBool("isShooting", false);
    }

    public void EnterSmack()
    {
        anim.SetBool("isHitting", true);
        ExitShoot();
        ExitReload();
    }

    public void ExitSmack()
    {
        anim.SetBool("isHitting", false);
    }

    public void EnterUnholster()
    {
        anim.SetBool("isUnholster", true);
    }

    public void ExitUnholster()
    {
        anim.SetBool("isUnholster", false);
    }

    public void EnterReload()
    {
        anim.SetBool("isShooting", false);
        //anim.SetBool("isHitting", false);

        ExitUnholster();

        //Leave reloading here ~Shaq
        anim.SetBool("isReloading", true);
    }

    public void ExitReload()
    {
        anim.SetBool("isReloading", false);
    }
}

