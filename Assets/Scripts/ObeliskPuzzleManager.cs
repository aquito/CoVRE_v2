using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class ObeliskPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private Transform slot_ObeliskSlot_1stFromTop; // serialising so the gameobjects can be added from the editor
    [SerializeField]
    private float startingRotation01;  // these are exposed to the editor so that the initial rotations can be shuffled

    [SerializeField]
    private Transform slot_ObeliskSlot_2ndFromTop;
    [SerializeField]
    private float startingRotation02;

    [SerializeField]
    private Transform slot_ObeliskSlot_3rdFromTop;
    [SerializeField]
    private float startingRotation03;

    [SerializeField]
    private Transform slot_ObeliskSlot_Bottom;
    [SerializeField]
    private float startingRotation04;

    [SerializeField]
    private GameObject obeliskBlockPrefab;

    [SerializeField]
    public GameObject normalRealTimeObject; // this needs to be the Realtime + VR Player game object in the scene

    private Realtime realTime; // we need access to the realtime script in the above object

    private bool isTop01BlockCorrect; // these will be used to set to true when correct rotation is in place
    private bool isTop02BlockCorrect;
    private bool isTop03BlockCorrect;
    private bool isBottomBlockCorrect;

    private bool isPuzzleSolved; // this helps in tracking the state of the puzzle

    private Quaternion top01BlockRotation; // these enable to specify the starting rotations when instantiating the objects
    private Quaternion top02BlockRotation;
    private Quaternion top03BlockRotation;
    private Quaternion bottomBlockRotation;

    private List<GameObject> blocks = new List<GameObject>(); // setting up an array so that changes to all blocks (e.g. switching material) can be done easily

    private AudioSource audioSource; // need to define this object to get access to it later

    [SerializeField]
    public GameObject doorToOpen;

    private GameObject top01Block;
    private GameObject top02Block;
    private GameObject top03Block;
    private GameObject bottomBlock;

    /*

    private void Awake()
    {
        realTime = normalRealTimeObject.GetComponent<Realtime>(); // to check the connection status we need access to the realtime component
        realTime.didConnectToRoom += Realtime_didConnectToRoom; // once the didConnectToRoom is true, run the Realtime_didConnectToRoom function

    }

    private void Realtime_didConnectToRoom(Realtime realTime) // so now once the client is connected to the room, we instantiate the blocks
    {
        // defining the editor-exposed starting rotations as quaternions, used in instantiating below
        top01BlockRotation = Quaternion.Euler(0, startingRotation01, 0);
        top02BlockRotation = Quaternion.Euler(0, startingRotation02, 0);
        top03BlockRotation = Quaternion.Euler(0, startingRotation03, 0);
        bottomBlockRotation = Quaternion.Euler(0, startingRotation04, 0);

        top01Block = Realtime.Instantiate(obeliskBlockPrefab.name, rotation: top01BlockRotation, position: slot_ObeliskSlot_1stFromTop.position);
        top01Block.transform.parent = slot_ObeliskSlot_1stFromTop;
        blocks.Add(top01Block); // adding the objects just instantiated and putting them into the list

        top02Block = Realtime.Instantiate(obeliskBlockPrefab.name, rotation: top02BlockRotation, position: slot_ObeliskSlot_2ndFromTop.position, ownedByClient: false);
        top02Block.transform.parent = slot_ObeliskSlot_2ndFromTop;
        blocks.Add(top02Block);

        top03Block = Realtime.Instantiate(obeliskBlockPrefab.name, rotation: top03BlockRotation, position: slot_ObeliskSlot_3rdFromTop.position, ownedByClient: false);
        top03Block.transform.parent = slot_ObeliskSlot_3rdFromTop;
        blocks.Add(top03Block);

        bottomBlock = Realtime.Instantiate(obeliskBlockPrefab.name, slot_ObeliskSlot_Bottom.position, bottomBlockRotation);
        bottomBlock.transform.parent = slot_ObeliskSlot_Bottom;
        blocks.Add(bottomBlock);

    }

    */

    private void Start()
    {

        // defining the editor-exposed starting rotations as quaternions, used in instantiating below
        top01BlockRotation = Quaternion.Euler(0, startingRotation01, 0);
        top02BlockRotation = Quaternion.Euler(0, startingRotation02, 0);
        top03BlockRotation = Quaternion.Euler(0, startingRotation03, 0);
        bottomBlockRotation = Quaternion.Euler(0, startingRotation04, 0);

        top01Block = Realtime.Instantiate(obeliskBlockPrefab.name, rotation: top01BlockRotation, position: slot_ObeliskSlot_1stFromTop.position);
        top01Block.transform.parent = slot_ObeliskSlot_1stFromTop;
        blocks.Add(top01Block); // adding the objects just instantiated and putting them into the list

        top02Block = Realtime.Instantiate(obeliskBlockPrefab.name, rotation: top02BlockRotation, position: slot_ObeliskSlot_2ndFromTop.position, ownedByClient: false);
        top02Block.transform.parent = slot_ObeliskSlot_2ndFromTop;
        blocks.Add(top02Block);

        top03Block = Realtime.Instantiate(obeliskBlockPrefab.name, rotation: top03BlockRotation, position: slot_ObeliskSlot_3rdFromTop.position, ownedByClient: false);
        top03Block.transform.parent = slot_ObeliskSlot_3rdFromTop;
        blocks.Add(top03Block);

        bottomBlock = Realtime.Instantiate(obeliskBlockPrefab.name, slot_ObeliskSlot_Bottom.position, bottomBlockRotation);
        bottomBlock.transform.parent = slot_ObeliskSlot_Bottom;
        blocks.Add(bottomBlock);

        audioSource = GetComponent<AudioSource>(); // getting access to the audiosource component in the object to play audio clips
    }


    private void OnTriggerEnter(Collider other)
    {
       if(other != null) // this some protective programming to make sure the collider has not moved away before the checks
        {
            // checking that the object colliding is the face of the block (using tags) and which block slot is it colliding with
            if (other.gameObject.tag == "ObeliskFace" && other.gameObject.transform.parent.parent.name == "ObeliskSlot_1stFromTop")
            {
                isTop01BlockCorrect = true; // setting this true so that all three can be checked together (see update function below)

                Debug.Log("top block CORRECT!");
            }


            if (other.gameObject.tag == "ObeliskFace" && other.gameObject.transform.parent.parent.name == "ObeliskSlot_2ndFromTop")
            {
                isTop02BlockCorrect = true;

                Debug.Log("top 2nd block CORRECT!");
            }

            if (other.gameObject.tag == "ObeliskFace" && other.gameObject.transform.parent.parent.name == "ObeliskSlot_3rdFromTop")
            {
                isTop03BlockCorrect = true;

                Debug.Log("top 3rd block CORRECT!");
            }


            if (other.gameObject.tag == "ObeliskFace" && other.gameObject.transform.parent.parent.name == "ObeliskSlot_Bottom")
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

                foreach (GameObject obj in blocks) // cycle through each block in the array
                { 
                    obj.GetComponent<SwitchMaterial>().Switch(); // run the public function in the script attached to the block
                }

                audioSource.Play(); // play audio clip in the audio source component 
                 
                Debug.Log("Puzzle SOLVED!"); // just a debugging message to the console

                doorToOpen.GetComponent<OpenDoor>().MoveDoorUp(); // run the function on the door object via its script
                
                isPuzzleSolved = true; // set the puzzle solved so this does not run more than once

               
            }

           
        }
    }


    

}
