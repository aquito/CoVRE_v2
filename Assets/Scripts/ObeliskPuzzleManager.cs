using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ObeliskPuzzleManager : MonoBehaviour
{

    [SerializeField]
    private float yAxisIncrement; // this is exposed so the distance of the blocks from each other on the y axis acan be defined
    //private Vector3 increment;

    [SerializeField]
    private GameManager gameManager;

   // [SerializeField]
   // private Transform puzzleStartingPosition; // this is the game object called ObeliskLocation on the scene that determines the top-most location of the blocks

    [SerializeField]
    private float startingRotation01;  // these are exposed to the editor so that the initial rotations can be shuffled

    [SerializeField]
    private float startingRotation02;

    
    [SerializeField]
    private float startingRotation03;

    
    [SerializeField]
    private float startingRotation04;



    private bool isTop01BlockCorrect; // these will be used to set to true when correct rotation is in place
    private bool isTop02BlockCorrect;
    private bool isTop03BlockCorrect;
    private bool isBottomBlockCorrect;

    private bool isPuzzleSolved; // this helps in tracking the state of the puzzle

    private Vector3 top01BlockPosition; // these are stored to be used in the puzzle alignment checks
    private Vector3 top02BlockPosition;
    private Vector3 top03BlockPosition;
    private Vector3 bottomBlockPosition;

    private Quaternion top01BlockRotation; // these enable to specify the starting rotations when instantiating the objects
    private Quaternion top02BlockRotation;
    private Quaternion top03BlockRotation;
    private Quaternion bottomBlockRotation;

    // private List<GameObject> blocks = new List<GameObject>(); // setting up an array so that changes to all blocks (e.g. switching material) can be done easily

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

       // top01BlockPosition = puzzleStartingPosition.position;
       // top02BlockPosition = new Vector3(puzzleStartingPosition.position.x, puzzleStartingPosition.position.y - yAxisIncrement, puzzleStartingPosition.position.z);
       // top03BlockPosition = new Vector3(puzzleStartingPosition.position.x, puzzleStartingPosition.position.y - yAxisIncrement * 2, puzzleStartingPosition.position.z);
       // bottomBlockPosition = new Vector3(puzzleStartingPosition.position.x, puzzleStartingPosition.position.y - yAxisIncrement * 3, puzzleStartingPosition.position.z);

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

    private void OnTriggerEnter(Collider other)
    {

       if(other != null) // this some protective programming to make sure the collider has not moved away before the checks
        {
            // checking that the object colliding is the face of the block (using tags) and which block slot is it colliding with
            if (other.gameObject.tag == "Face01") 
            {
                isTop01BlockCorrect = true; // setting this true so that all three can be checked together (see update function below)

                Debug.Log("top block CORRECT!");
            }


            if (other.gameObject.tag == "Face02")
            {
                isTop02BlockCorrect = true;

                Debug.Log("top 2nd block CORRECT!");
            }

            if (other.gameObject.tag == "Face03")
            {
                isTop03BlockCorrect = true;

                Debug.Log("top 3rd block CORRECT!");
            }


            if (other.gameObject.tag == "Face04")
            {
                isBottomBlockCorrect = true;

                Debug.Log("bottom block CORRECT!");
            }
        }
        
        

    }

    private void Update()
    {
        if(isTop01BlockCorrect && isTop02BlockCorrect && isTop03BlockCorrect && isBottomBlockCorrect) // are all blocks in correct rotation?
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
