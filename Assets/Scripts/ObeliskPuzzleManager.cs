using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObeliskPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private Transform topBlockSlot; // serialising so the gameobjects can be added from the editor
    [SerializeField]
    private float topBlockStartingRotation;  // these are exposed to the editor so that the initial rotations can be shuffled

    [SerializeField]
    private Transform middleBlockSlot;
    [SerializeField]
    private float middleBlockStartingRotation;

    [SerializeField]
    private Transform bottomBlockSlot;
    [SerializeField]
    private float bottomBlockStartingRotation;

    [SerializeField]
    private GameObject obeliskBlockPrefab;

    private bool isTopBlockCorrect; // these will be used to set to true when correct rotation is in place
    private bool isMiddleBlockCorrect;
    private bool isBottomBlockCorrect;

    private bool isPuzzleSolved; // this helps in tracking the state of the puzzle

    private Quaternion topBlockRotation; // these enable to specify the starting rotations when instantiating the objects
    private Quaternion middleBlockRotation;
    private Quaternion bottomBlockRotation;

    private GameObject[] blocks; // setting up an array so that changes to all blocks (e.g. switching material) can be done easily

    private AudioSource audioSource; // need to define this object to get access to it later

    private void Start()
    {
        // defining the editor-exposed starting rotations as quaternions, used in instantiating below
        topBlockRotation = Quaternion.Euler(0, topBlockStartingRotation, 0);
        middleBlockRotation = Quaternion.Euler(0, middleBlockStartingRotation, 0);
        bottomBlockRotation = Quaternion.Euler(0, bottomBlockStartingRotation, 0);
        // instantiating the prefab into the three slots defined above and parenting them to the slots
        GameObject.Instantiate(obeliskBlockPrefab, topBlockSlot.position, topBlockRotation, topBlockSlot.transform);
        GameObject.Instantiate(obeliskBlockPrefab, middleBlockSlot.position, middleBlockRotation, middleBlockSlot.transform);
        GameObject.Instantiate(obeliskBlockPrefab, bottomBlockSlot.position, bottomBlockRotation, bottomBlockSlot.transform);

        blocks = GameObject.FindGameObjectsWithTag("ObeliskBlock"); // finding the objects just instantiated and putting them into the array
        audioSource = GetComponent<AudioSource>(); // getting access to the audiosource component in the object to play audio clips
    }


    private void OnTriggerEnter(Collider other)
    {
       
        // checking that the object colliding is the face of the block (using tags) and which block slot is it colliding with
        if (other.gameObject.tag == "ObeliskFace" && other.gameObject.transform.parent.parent.name == "ObeliskSlotTop") 
        {
            isTopBlockCorrect = true; // setting this true so that all three can be checked together (see update function below)

            Debug.Log("top block CORRECT!");
        }
        

        if (other.gameObject.tag == "ObeliskFace" && other.gameObject.transform.parent.parent.name == "ObeliskSlotMiddle")
        {
            isMiddleBlockCorrect = true;

            Debug.Log("middle block CORRECT!");
        }
        

        if (other.gameObject.tag == "ObeliskFace" && other.gameObject.transform.parent.parent.name == "ObeliskSlotBottom")
        {
            isBottomBlockCorrect = true;

            Debug.Log("bottom block CORRECT!");
        }
        

    }

    private void Update()
    {
        if(isTopBlockCorrect && isMiddleBlockCorrect && isBottomBlockCorrect) // are all blocks in correct rotation?
        {
            if (!isPuzzleSolved) // if puzzle remains unsolved, do the below
            {

                foreach (GameObject obj in blocks) // cycle through each block in the array
                { 
                    obj.GetComponent<SwitchMaterial>().Switch(); // run the public function in the script attached to the block
                }

                audioSource.Play(); // play audio clip in the audio source component 
                 
                Debug.Log("Puzzle SOLVED!"); // just a debugging message to the console

                isPuzzleSolved = true; // set the puzzle solved so this does not run more than once
            }
            
            
        }
    }


    

}
