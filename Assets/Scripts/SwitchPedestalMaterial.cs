using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPedestalMaterial : MonoBehaviour
{
    private Material originalMaterial; // for storing the initial material (eg if it needs to be changed back)
    
    [SerializeField]
    private Material newMaterial; // defining a material object for the new one

    [SerializeField]
    private GameObject statuePedestal;

    //private AudioSource audioSource;

    void Start()
    {
        originalMaterial = statuePedestal.GetComponent<MeshRenderer>().sharedMaterial; // storing the existing material
        //audioSource = GetComponent<AudioSource>();
    }
        
    public void Switch() // public method so that the puzzlemanager script can access this 
    {
        statuePedestal.GetComponent<MeshRenderer>().material = newMaterial; // when called, switching to the new one
        //audioSource.Play();

    }

    public void SwitchBack()
    {
        statuePedestal.GetComponent<MeshRenderer>().material = originalMaterial;
        //audioSource.Stop();
    }
}
