using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    private Transform doorTransform; // transform of the object
    private Vector3 doorYlimit; // the highest position to lift the door to on the y axis

    [SerializeField]
    private Transform doorOpenPosition;

    private AudioSource audioSource;

    private bool isDoorGoodToMove;

    private void Start()
    {
        doorTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        doorYlimit = doorOpenPosition.transform.position;
        isDoorGoodToMove = false;
    }

    private void Update()
    {
        if (isDoorGoodToMove)
        {
            if (doorTransform.position.y < doorYlimit.y)
            {
                doorTransform.Translate(0, 1f * Time.deltaTime, 0);
            }
        }
    }

    public void MoveDoorUp()
    {
        isDoorGoodToMove = true;
        audioSource.Play();

        Debug.Log("Starting to move" + doorTransform.gameObject.name + " door");
    }

}
