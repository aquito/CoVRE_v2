using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class SpawnPoint : MonoBehaviour
{
    public Realtime _realtime; // Wire this up in the Unity Editor

    public List<Transform> spawnPoints;

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

        // TODO: Use the clientID to position the player
        playerXRRigposition.position = spawnPoints[localPlayerClientID].position;

    }
}
