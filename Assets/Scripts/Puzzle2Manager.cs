using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class Puzzle2Manager : MonoBehaviour
{
    public Realtime _realtime; // Wire this up in the Unity Editor

    [SerializeField]
    GameObject greenActivator;

    [SerializeField]
    Transform greenActivator01Slot;
    [SerializeField]
    Transform greenActivator02Slot;

    [SerializeField]
    PedestalManager pedestalManager;

    private GameObject green01;
    private GameObject green02;

    private GameObject red01;
    private GameObject red02;

    private GameObject yellow01;
    private GameObject yellow02;

    private void Awake()
    {

        // Subscribe to the didConnectToRoom event
        _realtime.didConnectToRoom += DidConnectToRoom;
    }
    // Start is called before the first frame update
    private void DidConnectToRoom(Realtime realtime)
    {
        // instatiating the 'activators' ie the spheres with which to interact
        // instatiating so that they can me made into realtime components by normal (if understood correctly)
        green01 = Realtime.Instantiate(greenActivator.name);
        green01.transform.Translate(greenActivator01Slot.position);
        green01.transform.SetParent(greenActivator01Slot);

        green02 = Realtime.Instantiate(greenActivator.name);
        green02.transform.Translate(greenActivator02Slot.position);
        green02.transform.SetParent(greenActivator02Slot);

    }

    // Update is called once per frame
    void Update()
    {
        // see 'PedestalPuzzleManager' - likely the monitoring of the individual two activators re pedestal should be here rather
        // in that script

        if(green01 != null && green02 != null)
        {
            if(green01.GetComponent<SwitchActivatorMaterial>().isSwitchActive)
            {
                pedestalManager.GetComponent<PedestalManager>().isGreen01Active = true;

            }

            if (green02.GetComponent<SwitchActivatorMaterial>().isSwitchActive)
            {
                pedestalManager.GetComponent<PedestalManager>().isGreen02Active = true;

            }

        }

        /*
        //For Debug purposes
        if (Input.GetKeyDown(KeyCode.G))
        {

            //green01.GetComponent<SwitchActivatorMaterial>().Switch();
            // green02.GetComponent<SwitchActivatorMaterial>().Switch();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            red01.GetComponent<SwitchActivatorMaterial>().Switch();
            red02.GetComponent<SwitchActivatorMaterial>().Switch();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            yellow01.GetComponent<SwitchActivatorMaterial>().Switch();
            yellow02.GetComponent<SwitchActivatorMaterial>().Switch();
        }
        */

    }

    
}
