using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(MeshRenderer))]

public class SocketPuzzleEvents : MonoBehaviour
{
    private void Awake()
    {
        XRSocketInteractor socket = gameObject.GetComponent<XRSocketInteractor>();
       //socket.selectEntered. (CheckObjectInSocket);
       // socket.selectExited.AddListener(ResetSocket);
    }

    public void CheckObjectInSocket(XRBaseInteractable obj)
    {
        Debug.Log(obj.name + " was placed IN socket");
    }

    public void ResetSocket(XRBaseInteractable obj)
    {
        Debug.Log(obj.name + " was taken OUT OF socket");
    }
}

