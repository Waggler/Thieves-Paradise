using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


//Current Bugs:
// - 


//Things to add:
//    - https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.CalculatePath.html
//    - Setting target to waypoints and having the AI cycle through them


//Done:
//    - Created Group of empty objects w/ parent for testing patrol functionality
// - 

public class EnemyController : MonoBehaviour
{
    #region AI State Machine

    private enum EnemyStates
    {
        PASSIVE,
        SUSPICIOUS,
        HOSTILE,
        RANGEDATTACK
    }
    #endregion

    #region Variables

    //Important Variables
    private bool alert;
    private Transform target;
    private NavMeshAgent agent;
    [SerializeField] private GameObject player;
    [SerializeField] private Text stateText;
    [SerializeField] private float lookRadius = 10f;
    [SerializeField] private float faceRadius = 10f;
    [SerializeField] private float pursuitSpeed = 25f;
    [SerializeField] private float patrolSpeed = 10f;

    //Temporary Variables
    [SerializeField] private float speedRead;
    #endregion

    #region Start & Update
    // Start is called before the first frame update
    void Start()
    {
        print("I live!");
        alert = false;
        agent = GetComponent<NavMeshAgent>();
        target = player.transform;
        stateText.text = "IDLE";

        ////Initial values for drawn Frustrum
        //fov = 90f;
        //maxRange = 16f;
        //maxRange = 5f;
        //aspect = 5;
    }


    //---------------------------------//
    void Update()
    {
        //Start of navigating enemy state machine
        //switch (EnemyStates)
        //{
        //    case EnemyStates.PASSIVE:
        //        print("");
        //        break;

        //    case EnemyStates.SUSPICIOUS:
        //        print("");

        //        break;

        //    case EnemyStates.HOSTILE:
        //        print("");
        //        break;

        //    default:
        //        print("");
        //        break;
        //}



        //public float pursuitSpeed;

        target = player.transform;

        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= lookRadius)
        {


            alert = true;

            print("I can see you");

            SetAIDestination(player.transform.position);

            SetAiSpeed(true);
            speedRead = pursuitSpeed;

            stateText.text = $"{target}";
            if (distanceToPlayer <= faceRadius)
            {
                FaceTarget();
            }

        }
        else
        {
            alert = false;

            print("Where are you");

            //transform.position in this case will eventually be replaced with the Vector3 data of the selected waypoint
            SetAIDestination(transform.position);

            SetAiSpeed(false);
            speedRead = patrolSpeed;

            stateText.text = $"{target}";

        }

        switch (alert)
        {
            case true:
                print("I have been given purpose");
                break;

            case false:
                print("Give me a task to do pls");
                break;
        }

        //agent.SetDestination(player.transform.position);

    }//End Update
    #endregion

    #region General Functions
    //---------------------------------//
    //Used to draw a sphere tied to the AI's current look radius. Really just serving as a diagnostic tool
    void OnDrawGizmosSelected()
    {
        //Drawing a magenta sphere at the AI's current position
        Gizmos.color = Color.magenta;
        //Gizmos.DrawFrustum(center , fov , maxRange, minRange, aspect);
        //Gizmos.DrawFrustum(transform.position, fov, maxRange, minRange, aspect);
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, faceRadius);

    }//End DrawGizmos


    //---------------------------------//
    //function for facing the player when the AI is withing stopping distance of the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100f);
    }//End FaceTarget

    //---------------------------------//
    // currently fleshing out this bit, might have another funtion that acts as a sort of gate for all these other funtions along the lines of "Mode controller" (or just have
    // that be handled by the initial switch case statement)
    void SetAiSpeed(bool detection)
    {
        if (detection == true)
        {

            switch (detection)
            {
                case true:
                    //agent.speed = pursuitSpeed;
                    agent.speed = Mathf.Lerp(patrolSpeed, pursuitSpeed, 1);
                    break;
                case false:
                    //agent.speed = patrolSpeed;
                    agent.speed = Mathf.Lerp(pursuitSpeed, patrolSpeed, 1);
                    break;
            }
        }

    }//End SetSpeed

    //---------------------------------//
    //Function for setting AI destination
    void SetAIDestination(Vector3 point)
    {
        agent.SetDestination(point);
    }//End Set AI Destination
    #endregion

    #region Waypoints Logic
    [SerializeField] private List<Transform> waypoints;
    //waypoints.Count will be used to get the number of points in the list (similar to array.Length)
    private int waypointsIndex = 0;

    #region Waypoints Functions
    private void SetNextWaypoint()
    {

    }




    #endregion

    #endregion


    //Generic Funcitons to be add WAY down the line
    private void RangedAttack()
    {
    }

    private void CQCAttack()
    {
    }


}