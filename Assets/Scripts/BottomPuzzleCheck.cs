using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPuzzleCheck : MonoBehaviour
{
    public bool isTopBlockCorrect;
    public bool isBottomBlockCorrect;




    private void OnTriggerEnter(Collider other)
    {

        if (other != null) // this some protective programming to make sure the collider has not moved away before the checks
        {
            // checking that the object colliding is the face of the block (using tags) and which block slot is it colliding with
            if (other.gameObject.tag == "Face03")
            {
                isTopBlockCorrect = true; // setting this true so that all three can be checked together (see update function below)

                Debug.Log("bottom puzzle top block 1 CORRECT!");
            }

            if (other.gameObject.tag == "Face04")
            {
                isBottomBlockCorrect = true; // setting this true so that all three can be checked together (see update function below)

                Debug.Log("bottom puzzle bottom block CORRECT!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null) // this some protective programming to make sure the collider has not moved away before the checks
        {
            // checking that the object colliding is the face of the block (using tags) and which block slot is it colliding with
            if (other.gameObject.tag == "Face03")
            {
                isTopBlockCorrect = false; // setting this true so that all three can be checked together (see update function below)

                // Debug.Log("bottom puzzle top block 1 CORRECT!");
            }

            if (other.gameObject.tag == "Face04")
            {
                isBottomBlockCorrect = false; // setting this true so that all three can be checked together (see update function below)

                // Debug.Log("bottom puzzle bottom block CORRECT!");
            }
        }
    }
}
