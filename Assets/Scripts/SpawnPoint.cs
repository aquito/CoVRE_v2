using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnPoint : MonoBehaviour
{
    public Realtime _realtime; // Wire this up in the Unity Editor

    public List<Transform> spawnPoints;

    [SerializeField]
    private GameObject loadingOverlay;
    private LoadingOverlay overlay;

    private Transform playerXRRigposition;
    private void Awake()
    {
        
        // Subscribe to the didConnectToRoom event
        _realtime.didConnectToRoom += DidConnectToRoom;
    }

    private void Start()
    {
        
        playerXRRigposition = gameObject.GetComponent<Transform>();   
        

    }

    private void DidConnectToRoom(Realtime realtime)
    {
        // This method will be called by Realtime when it connects to the room.

        // Fetch this client's clientID
        int localPlayerClientID = _realtime.clientID;

        //check that the clientID is not greater than the number of predefined spawn points
        if (localPlayerClientID <= spawnPoints.Count)
        {
            // Use the clientID to position the player
            playerXRRigposition.position = spawnPoints[localPlayerClientID].position;
            playerXRRigposition.rotation = spawnPoints[localPlayerClientID].rotation;
            
        } else
        {
            // Use the clientID to position the player
            playerXRRigposition.position = spawnPoints[0].position;
            playerXRRigposition.rotation = spawnPoints[0].rotation;
        }
        //Boolean, xr ray, button
        

        //overlay = loadingOverlay.GetComponent<LoadingOverlay>();
        //overlay.FadeIn();
        //loadingOverlay.SetActive(false); // disabling the black cube
    }
}
