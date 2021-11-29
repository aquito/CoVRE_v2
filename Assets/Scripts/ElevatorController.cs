using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{

    public Animator animator;
    public GameObject PlayerObject;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player is in the Zone");
            PlayerObject.transform.parent = gameObject.transform;
            animator.SetTrigger("PlayerEnterTrigger");            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player has left the zone");
            PlayerObject.transform.parent = null;
        }
    }












}
