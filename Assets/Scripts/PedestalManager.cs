using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PedestalManager : MonoBehaviour
{

    // this script oversees the completion of the puzzle whereas Puzzle2Manager is managing the activator spheres
    // and the individual color-coded puzzles
    // it might be though that some of the things this currently has needs to be moved into the Puzzle2Manager 
    // and this just oversees the completion

    [SerializeField]
    private GameObject greenPedestal;

    
    //[SerializeField]
    //private Material greenPedestalActiveMaterial;

    //private Material greenPedestalDefaultMaterial;


    [SerializeField]
    private GameObject redPedestal;

    //[SerializeField]
    //private Material redPedestalActiveMaterial;

    //private Material redPedestalDefaultMaterial;

    [SerializeField]
    private GameObject yellowPedestal;

   // [SerializeField]
    //private Material yellowPedestalActiveMaterial;

    //private Material yellowPedestalDefaultMaterial;

    /*
    [SerializeField]
    private GameObject pedestalActivatorGreen01;

    [SerializeField]
    private GameObject pedestalActivatorGreen02;

    [SerializeField]
    private GameObject pedestalActivatorRed01;

    [SerializeField]
    private GameObject pedestalActivatorRed02;
    [SerializeField]
    private GameObject pedestalActivatorYellow01;

    [SerializeField]
    private GameObject pedestalActivatorYellow02;

    */

    private XRSocketInteractor greenSocketInteractor;
    private XRSocketInteractor redSocketInteractor;
    private XRSocketInteractor yellowSocketInteractor;

    public bool isGreen01Active;
    public bool isGreen02Active;

    public bool isRed01Active;
    public bool isRed02Active;

    public bool isYellow01Active;
    public bool isYellow02Active;

   

    private void Start()
    {
       // defining these objects so that they can be changed/activated based on player interactions
        //greenPedestalDefaultMaterial = greenPedestal.GetComponent<MeshRenderer>().material;
        greenSocketInteractor = greenPedestal.GetComponentInChildren<XRSocketInteractor>();

       // redPedestalDefaultMaterial = redPedestal.GetComponent<MeshRenderer>().material;
        redSocketInteractor = redPedestal.GetComponentInChildren<XRSocketInteractor>();

       // yellowPedestalDefaultMaterial = yellowPedestal.GetComponent<MeshRenderer>().material;
        yellowSocketInteractor = yellowPedestal.GetComponentInChildren<XRSocketInteractor>();

     

       
    }

    private void Update()
    {
       // as is, these booleans monitor the states of the spheres however this has since moved or is to be moved to 
       // Puzzle2Manager which maybe should be renamed to 'Puzzle2ActivatorManager' to be more clear
       
        /*
        isGreen01Active = pedestalActivatorGreen01.GetComponent<SwitchActivatorMaterial>().isSwitchActive;
        isGreen02Active = pedestalActivatorGreen02.GetComponent<SwitchActivatorMaterial>().isSwitchActive;
        isRed01Active = pedestalActivatorRed01.GetComponent<SwitchActivatorMaterial>().isSwitchActive;
        isRed02Active = pedestalActivatorRed02.GetComponent<SwitchActivatorMaterial>().isSwitchActive;
        isYellow01Active = pedestalActivatorYellow01.GetComponent<SwitchActivatorMaterial>().isSwitchActive;
        isYellow02Active = pedestalActivatorYellow02.GetComponent<SwitchActivatorMaterial>().isSwitchActive;
        */
       // the below is monitoring the simultaneous activation of the two same-colure activators
       // ie if they are both true they give the hint of which pedestal relates to which statue color
       // (which statue should be brought onto which pedestal)

        if (isGreen01Active && isGreen02Active)
        {
            greenPedestal.GetComponent<RaisePedestal>().MoveUp();
           // greenPedestal.GetComponent<MeshRenderer>().material = greenPedestalActiveMaterial;
            greenSocketInteractor.socketActive = true;
        }
        else if(isRed01Active && isRed02Active)
        {
            redPedestal.GetComponent<RaisePedestal>().MoveUp();
            //redPedestal.GetComponent<MeshRenderer>().material = redPedestalActiveMaterial;
            redSocketInteractor.socketActive = true;
        }
        else if(isYellow01Active && isYellow02Active)
        {

            //yellowPedestal.GetComponent<MeshRenderer>().material = yellowPedestalActiveMaterial;
            yellowPedestal.GetComponent<RaisePedestal>().MoveUp();
            yellowSocketInteractor.socketActive = true;
        }

        /*
        else
        {
           greenPedestal.GetComponent<MeshRenderer>().material = greenPedestalDefaultMaterial;
            greenSocketInteractor.socketActive = false;
            redPedestal.GetComponent<MeshRenderer>().material = redPedestalDefaultMaterial;
            redSocketInteractor.socketActive = false;
            yellowPedestal.GetComponent<MeshRenderer>().material = yellowPedestalDefaultMaterial;
            yellowSocketInteractor.socketActive = false;
        }
        */


        //For Debug purposes
        if (Input.GetKeyDown(KeyCode.G))
        {

            greenPedestal.GetComponent<RaisePedestal>().MoveUp();
            
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            redPedestal.GetComponent<RaisePedestal>().MoveUp();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            yellowPedestal.GetComponent<RaisePedestal>().MoveUp();
        }

    }


}
