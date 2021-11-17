using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    public Transform obeliskLocation;

    [SerializeField]
    private GameObject puzzleManagerObject;

    [SerializeField]
    private GameObject doorToOpen;

    [SerializeField]
    public GameObject normalRealTimeObject; // this needs to be the Realtime + VR Player game object in the scene

    private Realtime realTime; // we need access to the realtime script in the above object

    private GameObject puzzleManager;

    private ObeliskPuzzleManager obeliskPuzzleManager;

    //public List<GameObject> blocks = new List<GameObject>(); // setting up an array so that changes to all blocks (e.g. switching material) can be done easily

    private void Awake()
    {
       obeliskPuzzleManager = puzzleManagerObject.GetComponent<ObeliskPuzzleManager>();

       // obeliskPuzzleManager.puzzleStartingPosition = obeliskLocation;

       // obeliskPuzzleManager.normalRealTimeObject = normalRealTimeObject;

       // obeliskPuzzleManager.doorToOpen = doorToOpen;

       // obeliskPuzzleManager.blocks = blocks;

        realTime = normalRealTimeObject.GetComponent<Realtime>(); // to check the connection status we need access to the realtime component
        realTime.didConnectToRoom += Realtime_didConnectToRoom; // once the didConnectToRoom is true, run the Realtime_didConnectToRoom function

    }

    private void Realtime_didConnectToRoom(Realtime realTime)
    {

       
        puzzleManagerObject.SetActive(true);
       


    }


}
