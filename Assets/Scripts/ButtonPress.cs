using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonPress : MonoBehaviour
{
    private Animator anim;
    private bool isButtonDown;
    private bool isButtonIdle;
    //private bool isButtonActive;
    

    private void Start()
    {
        anim = GetComponent<Animator>();
      
        isButtonDown = anim.GetBool("isButtonDown");
        isButtonIdle = anim.GetBool("isButtonIdle");
        anim.Play("ButtonReset");


    }

    public void PressButton()
    {
        if(isButtonIdle)
        {
            anim.Play("ButtonDown");
            anim.SetBool("isButtonDown", true);
            anim.SetBool("isButtonIdle", false);
            //anim.SetTrigger("buttonPressed");

        }    
        
    }

    public void ResetButton()
    {
        if (isButtonDown)
        {
            anim.Play("ButtonReset");
            
            

            anim.SetBool("isButtonDown", false);
            anim.SetBool("isButtonIdle", true);
        }
    }

    public void ToggleButton()
    {
       

        if (isButtonDown)
        {
            ResetButton();
            Debug.Log("Resetting button");
            
        }
        else
        {

            PressButton();
            Debug.Log("Pressing button");
        }
       
        
    }

    private void Update()
    {
        isButtonDown = anim.GetBool("isButtonDown");
        isButtonIdle = anim.GetBool("isButtonIdle");
    }

}
