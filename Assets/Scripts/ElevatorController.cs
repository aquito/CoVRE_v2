using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Normal.Realtime;
using UnityEngine.UI;


public class ElevatorController : MonoBehaviour
{

    //public Text inBounds;
    public Animator animator;

    public GameObject PlayerObject; 
    public GameObject PositionTransformer;
    public GameObject Pedastal;

    private GameObject[] PlayersInGame;
    
    
    private bool RaiseLiftCalled = false;
    private bool PlayersInBounds = false;

    private int PlayersOnLift, MaxPlayers;


    private void Start()
    {
        PlayersInGame = GameObject.FindGameObjectsWithTag("Player");
        CountPlayers();
    }


    private void Update()
    {
        
        if(isPlaying(animator, "RaiseFloor") == true) // Checks if RaiseFloor animation state is running
        {
            PlayerObject.transform.position = PositionTransformer.transform.position;
            Pedastal.transform.position = gameObject.transform.position;
        }
        else if(isPlaying(animator, "RaiseFloorCompleted") == true)
        {
            RaiseLiftCalled = false;
            PositionTransformer.transform.parent = PlayerObject.transform;        
        }

        CountPlayers();

        //inBounds.text = "Players on Lift " + PlayersOnLift + "/" + MaxPlayers;

       
    }

    public void RaiseLift() // Is called by the Button Script to start the animation for raising the lift
    {

        if (PlayersOnLift < MaxPlayers) // Currently set to less than to allow for testing of elevator while I work on Trigger recognising player
            {
                RaiseLiftCalled = true;
                PositionTransformer.transform.parent = gameObject.transform;
                animator.SetBool("PlayerPressedButton", true);
            }
        
    }

    private void CountPlayers()
    {
        MaxPlayers = PlayersInGame.Length / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            for(int i = 0; i < MaxPlayers; i++)
            {
                PlayersOnLift = i;
            }
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
