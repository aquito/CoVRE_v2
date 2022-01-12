using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ElevatorController : MonoBehaviour
{

    public Animator animator;
    public GameObject PlayerObject;
    public GameObject PositionTransformer;
    public bool RaiseLiftCalled = false;
    


    private void Update()
    {
        if(isPlaying(animator, "RaiseFloor") == true) // Checks if RaiseFloor animation state is running
        {
            PlayerObject.transform.position = PositionTransformer.transform.position; 
        }
        else if(isPlaying(animator, "RaiseFloorCompleted") == true)
        {
            RaiseLiftCalled = false;
            PositionTransformer.transform.parent = PlayerObject.transform;
        }
    }

    public void RaiseLift() // Is called by the Button Script to start the animation for raising the lift
    {       

        RaiseLiftCalled = true;

        if (RaiseLiftCalled)
        {
            PositionTransformer.transform.parent = gameObject.transform;           
            animator.SetBool("PlayerPressedButton", true);            
        }      

        
    }

    bool isPlaying(Animator anim, string stateName) // Function for checking animation states
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }












}
