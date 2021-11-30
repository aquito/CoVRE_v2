using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavingLift : MonoBehaviour
{
    public GameObject PlayerObject;

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "LiftFloor")
        {
            Debug.Log("Player has left the zone");
            PlayerObject.transform.parent = null;
        }
    }
}
