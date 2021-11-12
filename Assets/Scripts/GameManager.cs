using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    Transform obeliskLocation;

    [SerializeField]
    private GameObject puzzleManagerPrefab;

    [SerializeField]
    private GameObject doorToOpen;

    [SerializeField]
    public GameObject normalRealTimeObject; // this needs to be the Realtime + VR Player game object in the scene

    private Realtime realTime; // we need access to the realtime script in the above object

    private GameObject puzzleManager;

    private ObeliskPuzzleManager obeliskPuzzleManager;

    private void Awake()
    {
        obeliskPuzzleManager = puzzleManagerPrefab.GetComponent<ObeliskPuzzleManager>();

        obeliskPuzzleManager.normalRealTimeObject = normalRealTimeObject;

        obeliskPuzzleManager.doorToOpen = doorToOpen;

        realTime = normalRealTimeObject.GetComponent<Realtime>(); // to check the connection status we need access to the realtime component
        realTime.didConnectToRoom += Realtime_didConnectToRoom; // once the didConnectToRoom is true, run the Realtime_didConnectToRoom function

    }

    private void Realtime_didConnectToRoom(Realtime realTime)
    {
        
        
        puzzleManager = Realtime.Instantiate("ObeliskPuzzleManager");
        puzzleManager.transform.parent = obeliskLocation;


    }


}
