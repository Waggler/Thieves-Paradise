using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreadMill : MonoBehaviour
{
    [Header("Direction & Speed.")]
    [Tooltip("Use only the X and Z fields as Y will send you to the moon. Use either -1 or 1 for the fields. Never have more than on feild active; example (1, 0, 1) will cause the player to move diagonal.")]
    [SerializeField] private Vector3 Direction;
    [SerializeField] private float TreadMillSpeed;

    [Header("")]
    [SerializeField] private CharacterController Player;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player.Move(Direction * TreadMillSpeed * Time.deltaTime);
            TreadMillSpeed += Time.deltaTime;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.Move(Direction * 15 * Time.deltaTime * 2);
            TreadMillSpeed += Time.deltaTime;

            TreadMillSpeed = 0;
        }
    }
}
