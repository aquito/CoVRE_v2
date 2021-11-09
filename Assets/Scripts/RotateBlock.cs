using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlock : MonoBehaviour
{
    private Transform obeliskTransform; // the transform of the object the script is attached to
    private Quaternion obeliskRotation; // quaternion is how unity handles rotations so need to get that from object
 
    private Quaternion ninetyDegreeRotation; // and define one for the 90-degree rotation
   
    private void Start()
    {
        obeliskTransform = GetComponent<Transform>(); // get access to the object's transform
        obeliskRotation = obeliskTransform.rotation;
        ninetyDegreeRotation = Quaternion.Euler(0, -90, 0); 
      

    }

    public void RotateObeliskPart() // function to rotate the block
    {

        StartCoroutine(RotateMe(ninetyDegreeRotation.eulerAngles, 0.8f)); // launching the coroutine; using coroutine to get a smooth rotation
       
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime) // this is the coroutine, copied from a post online :D
    {
        var fromAngle = transform.rotation; // existing rotation
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles); // the rotation we want to end with
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime) // for loop to cycle up to the specified inTime variable
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t); // this actually rotates the object
            yield return null; // this you need at the end of a coroutine to tell the function it's work is done
        }
    }

}
