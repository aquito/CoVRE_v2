using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchActivatorMaterial : MonoBehaviour
{
    private Material originalMaterial; // for storing the initial material (eg if it needs to be changed back)
    
    [SerializeField]
    private Material newMaterial; // defining a material object for the new one

    private AudioSource audioSource;

    private PedestalActivatorSync _pedestalActivatorSync;

    private Color _originalColor;

    private Color _activeColor;

    public bool isSwitchActive;

    private void Awake()
    {
        // Get a reference to the color sync component
        _pedestalActivatorSync = GetComponent<PedestalActivatorSync>();
    }

    void Start()
    {
        originalMaterial = GetComponent<MeshRenderer>().material; // storing the existing material
        audioSource = GetComponent<AudioSource>();
        isSwitchActive = false;
        _activeColor = Color.green;
        _originalColor = GetComponent<MeshRenderer>().material.color;
    }
        
    public void Switch() // public method so that the puzzlemanager script can access this 
    {
        GetComponent<MeshRenderer>().material = newMaterial; // when called, switching to the new one
        audioSource.Play();

        if (!isSwitchActive)
        {
            isSwitchActive = true;
        }
        

    }

    public void SwitchBack()
    {
        GetComponent<MeshRenderer>().material = originalMaterial;
        audioSource.Stop();

        if (isSwitchActive)
        {
            isSwitchActive = false;
        }
    }

    public void SwitchColor()
    {
        if(GetComponent<MeshRenderer>().material.color != _activeColor)
        _pedestalActivatorSync.SetColor(_activeColor);
    }

    public void SwitchColorBack()
    {
        if (GetComponent<MeshRenderer>().material.color != _originalColor)
            _pedestalActivatorSync.SetColor(_originalColor);
    }

}
