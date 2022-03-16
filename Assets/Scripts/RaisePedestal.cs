using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePedestal : MonoBehaviour
{

    private Transform pedestalTransform; // transform of the object
    private Vector3 pedestalYlimit; // the highest position to lift the door to on the y axis

    [SerializeField]
    public Transform pedestalRaisedPosition;

    private AudioSource audioSource;

    private bool isPedestalGoodToMove;

    private void Start()
    {
        pedestalTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        pedestalYlimit = pedestalRaisedPosition.transform.position;
        isPedestalGoodToMove = false;
    }

    private void Update()
    {
        if (isPedestalGoodToMove)
        {
            if (pedestalTransform.position.y < pedestalYlimit.y)
            {
                pedestalTransform.Translate(0, 1f * Time.deltaTime, 0);
            }
        }
    }

    public void MoveUp()
    {
        isPedestalGoodToMove = true;
        audioSource.Play();

        Debug.Log("Starting to move" + pedestalTransform.gameObject.name + " door");
    }

}
