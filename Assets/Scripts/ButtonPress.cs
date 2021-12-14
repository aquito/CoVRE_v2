using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonPress : MonoBehaviour
{
    public bool lockX;
    public bool lockY;
    public bool lockZ;

    public float speed;
    public float returnSpeed;
    private float distanceX = 0;
    private float distanceY = 0;
    private float distanceZ = 0;

    [SerializeField]
    private bool isButtonOut = true;
 

    // This is the second part of the button which remains in static position but changes color according to press
    public GameObject indicatorObject;
    private Collider indicatorCollider;

    private AudioSource audioSource;

   
    protected Vector3 startPosition;
    private Vector3 localPos;

    void Start()
    {
        // Remember start position of button
        startPosition = transform.localPosition;

        // Use local position instead of global, so button can be rotated in any direction
        localPos = transform.localPosition;
        if (lockX) localPos.x = startPosition.x;
        if (lockY) localPos.y = startPosition.y;
        if (lockZ) localPos.z = startPosition.z;
        transform.localPosition = localPos;

        audioSource = GetComponent<AudioSource>();
        indicatorCollider = indicatorObject.GetComponent<Collider>();

    }

    public void AllowPress()
    {
       if(isButtonOut)
        {
            isButtonOut = false;
            Debug.Log("press button registered");
            audioSource.Play();
        }
       
    }

    public void ResetButton()
    {
        if(!isButtonOut )
        {
            isButtonOut = true;
            transform.Translate(startPosition);
        }
        
        
    }


    void Update()
    {


        //released = false;
        if (!isButtonOut)
        {
            //Get distance of button press. Make sure to only have one moving axis.
            Vector3 allDistances = transform.localPosition + indicatorObject.transform.localPosition + indicatorCollider.bounds.size; // startPosition;
            if (!lockX)
            {
                distanceX = Math.Abs(allDistances.x);
            }

            if (!lockY)
            {
                distanceY = Math.Abs(allDistances.y);
            }

            if (!lockZ)
            {
                distanceZ = Math.Abs(allDistances.z);
            }

            //if (allDistances.x > transform.localPosition.x)
            //{
            transform.Translate(-distanceX * Time.deltaTime * speed, distanceY * Time.deltaTime * speed, distanceZ * Time.deltaTime * speed);
            //}
        }
        else
        {
           // isButtonOut = true;

             if (transform.localPosition.x > startPosition.x)
             {
            // Return button to startPosition
            transform.Translate(startPosition.x * Time.deltaTime * returnSpeed, startPosition.y, startPosition.z);
             }
        }
        
            
            
        
        
        

    }   


}
