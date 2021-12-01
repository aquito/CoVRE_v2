using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotateBlock : MonoBehaviour
{
    private Transform obeliskTransform; // the transform of the object the script is attached to
    private Quaternion obeliskRotation; // quaternion is how unity handles rotations so need to get that from object

    private Quaternion ninetyDegreeRotation; // and define one for the 90-degree rotation

    private GameManager gameManager;

    private AudioSource audioSource;

    private AudioClip audioClip;

    private void Start()
    {
        obeliskTransform = GetComponent<Transform>(); // get access to the object's transform
        obeliskRotation = obeliskTransform.rotation;
        ninetyDegreeRotation = Quaternion.Euler(0, -90, 0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.GetComponent<AudioClip>();

    }

    public void RotateObeliskPart() // function to rotate the block
    {

        gameManager.DisableXRInteractables();

        var currentAngle = transform.rotation.y; // existing rotation

        if (currentAngle != 0 || currentAngle != 90 || currentAngle !=180 || currentAngle != 270)
        {
           // Debug.Log("Rotation is out of sync");

            // check if fromAngle.rotation.y > 0 && fromAngle.rotation.y < 90 then set start from 0
            //  if fromAngle.rotation.y > 90 && fromAngle.rotation.y < 180 then set start from 90
            //  if fromAngle.rotation.y > 180 && fromAngle.rotation.y < 270 then set start from 180
            //  if fromAngle.rotation.y > 270 && fromAngle.rotation.y < 360 then set start from 270
        }

        
        StartCoroutine(RotateMe(ninetyDegreeRotation.eulerAngles, 1.5f)); // launching the coroutine; using coroutine to get a smooth rotation
        
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime) // this is the coroutine, copied from a post online :D
    {
        audioSource.Play();

        var fromAngle = transform.rotation; // existing rotation

        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles); // the rotation we want to end with
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime) // for loop to cycle up to the specified inTime variable
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t); // this actually rotates the object
            yield return null; // this you need at the end of a coroutine to tell the function it's work is done
        }
        gameManager.EnableXRInteractables(); // reactivating the grab now that the rotation is done
    }

}
