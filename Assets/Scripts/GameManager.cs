using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    public Transform obeliskLocation;

    [SerializeField]
    private GameObject puzzleManagerObject;

 
    [SerializeField]
    public GameObject normalRealTimeObject; // this needs to be the Realtime + VR Player game object in the scene

    private Realtime realTime; // we need access to the realtime script in the above object

    private GameObject puzzleManager;

    private ObeliskPuzzleManager obeliskPuzzleManager;

    //private List<GameObject> blocks; //= new List<GameObject>(); // setting up an array so that changes to all blocks (e.g. switching material) can be done easily

    private GameObject[] blocks;

    private void Awake()
    {
       obeliskPuzzleManager = puzzleManagerObject.GetComponent<ObeliskPuzzleManager>();



        realTime = normalRealTimeObject.GetComponent<Realtime>(); // to check the connection status we need access to the realtime component
        realTime.didConnectToRoom += Realtime_didConnectToRoom; // once the didConnectToRoom is true, run the Realtime_didConnectToRoom function

    }

    private void Realtime_didConnectToRoom(Realtime realTime)
    {

       
        puzzleManagerObject.SetActive(true);
        blocks = GameObject.FindGameObjectsWithTag("ObeliskBlock");
        Debug.Log(blocks.Length);




    }

    public void SwitchBlockMaterials()
    {
        foreach (GameObject obj in blocks) // cycle through each block in the array
        {
            obj.GetComponent<SwitchMaterial>().Switch(); // run the public function in the script attached to the block
        }

       

        Debug.Log("Puzzle SOLVED!"); // just a debugging message to the console

    }

    public void DisableXRInteractables()
    {
        foreach (GameObject obj in blocks) // cycle through each block in the array
        {
            obj.GetComponent<XRGrabInteractable>().enabled = false;// run the public function in the script attached to the block
        }
    }

    public void EnableXRInteractables()
    {
        foreach (GameObject obj in blocks) // cycle through each block in the array
        {
            obj.GetComponent<XRGrabInteractable>().enabled = true;// run the public function in the script attached to the block
        }
    }

}
