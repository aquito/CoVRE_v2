using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ObeliskPuzzleManager : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;


    [SerializeField]
    private float startingRotation01;  // these are exposed to the editor so that the initial rotations can be shuffled

    [SerializeField]
    private float startingRotation02;

    
    [SerializeField]
    private float startingRotation03;

    
    [SerializeField]
    private float startingRotation04;

    [SerializeField]
    private GameObject topPuzzle;

    [SerializeField]
    private GameObject bottomPuzzle;

    private BottomPuzzleCheck bottomPuzzleCheck;
    private TopPuzzleCheck topPuzzleCheck;

    private bool isTop01BlockCorrect; // these will be used to set to true when correct rotation is in place
    private bool isTop02BlockCorrect;
    private bool isTop03BlockCorrect;
    private bool isBottomBlockCorrect;

    private bool isPuzzleSolved; // this helps in tracking the state of the puzzle

    private AudioSource audioSource; // need to define this object to get access to it later

    public GameObject doorRightToOpen;
    public GameObject doorLeftToOpen;

    private GameObject top01Block; // need to define these to be able to instantiate & check alignments
    private GameObject top01Face;
    private GameObject top02Block;
    private GameObject top02Face;  
    private GameObject top03Block;
    private GameObject top03Face;
    private GameObject bottomBlock;
    private GameObject bottomFace;


    private void Start()
    {

        topPuzzleCheck = topPuzzle.GetComponent<TopPuzzleCheck>();
        bottomPuzzleCheck = bottomPuzzle.GetComponent<BottomPuzzleCheck>();

        audioSource = GetComponent<AudioSource>(); // getting access to the audiosource component in the object to play audio clips

    }

    /*
    private void InstantiateBlocks()
    {
        

        // defining the editor-exposed starting rotations as quaternions, used in instantiating below
        top01BlockRotation = Quaternion.Euler(0, startingRotation01, 0);
        top02BlockRotation = Quaternion.Euler(0, startingRotation02, 0);
        top03BlockRotation = Quaternion.Euler(0, startingRotation03, 0);
        bottomBlockRotation = Quaternion.Euler(0, startingRotation04, 0);


        // defining the increments for the block positions
       // top01BlockPosition.position = puzzleStartingPosition.position;

       // top02BlockPosition.position = top01BlockPosition.position;
       // top03BlockPosition.position = top01BlockPosition.position + (increment * 2);
      //  bottomBlockPosition.position = top01BlockPosition.position + (increment * 3);

        // the blocks are instantiated into the top position and then incremented with the increment defined in the editor
        top01Block = Realtime.Instantiate("ObeliskBlock", rotation: top01BlockRotation, position: top01BlockPosition);
        top01Face = top01Block.GetComponentInChildren<Transform>().Find("Face").gameObject; // getting the child (face) and tagging it
        top01Face.tag = "Face01";   

        blocks.Add(top01Block); // adding the objects just instantiated and putting them into the list

        top02Block = Realtime.Instantiate("ObeliskBlock", rotation: top02BlockRotation, position: top02BlockPosition);
        top02Face = top02Block.GetComponentInChildren<Transform>().Find("Face").gameObject;
        top02Face.tag = "Face02";
        blocks.Add(top02Block);

        top03Block = Realtime.Instantiate("ObeliskBlock", rotation: top03BlockRotation, position: top03BlockPosition);
        top03Face = top03Block.GetComponentInChildren<Transform>().Find("Face").gameObject;
        top03Face.tag = "Face03";   
        blocks.Add(top03Block);

        bottomBlock = Realtime.Instantiate("ObeliskBlock", rotation: bottomBlockRotation, position: bottomBlockPosition);
        bottomFace = bottomBlock.GetComponentInChildren<Transform>().Find("Face").gameObject;
        bottomFace.tag = "Face04";
        blocks.Add(bottomBlock);

        
    }
    

    */

    private void Update()
    {
        if(topPuzzleCheck.isBottomBlockCorrect && topPuzzleCheck.isTopBlockCorrect && bottomPuzzleCheck.isTopBlockCorrect && bottomPuzzleCheck.isBottomBlockCorrect) // are all blocks in correct rotation?
        {
            if (!isPuzzleSolved) // if puzzle remains unsolved, do the below
            {

                gameManager.SwitchBlockMaterials();


                audioSource.Play(); // play audio clip in the audio source component 

                doorRightToOpen.GetComponent<OpenDoor>().MoveDoorUp(); // run the function on the door object via its script
                doorLeftToOpen.GetComponent<OpenDoor>().MoveDoorUp(); // run the function on the door object via its script

                isPuzzleSolved = true; // set the puzzle solved so this does not run more than once

               
            }

           
        }
    }


    

}
