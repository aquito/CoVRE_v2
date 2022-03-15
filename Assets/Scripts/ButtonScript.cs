using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonScript : XRBaseInteractable

{
    public UnityEvent OnPress = null;

    private bool previousPress = false;

    private float yMin = 0.0f;
    private float yMax = 0.0f;
    private float previousHandHeight = 0.0f;

    private XRBaseInteractor hoverInteractor = null;

    /*protected override*/
    protected override void Awake()
    {
        base.Awake();   // Sets Colliders from XRBaseInteractor     
        onHoverEntered.AddListener(StartPress); //Unsure how to use new commands
        onHoverExited.AddListener(EndPress);
    }
         

    private void OnDestroy() //Destroy created listeners
    {
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    private void Start()
    {
        SetMinMax();
    }

    private void StartPress(XRBaseInteractor interactor) //Checks the height of the hand while the button is "active"
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
    }

    private void EndPress(XRBaseInteractor interactor) //Resets the buttons Y Position
    {
        hoverInteractor = null;
        previousHandHeight = 0.0f;

        previousPress = false;
        SetYPosition(yMax);
    }    

    private void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            CheckPress();
        }
    }

    private float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);

        return localPosition.y;
    }

    private void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    private void CheckPress() //Checks if button is pressed down and stops functionality from repeating
    {
        bool inPosition = InPosition();

        if(inPosition && inPosition != previousPress)
        {
            OnPress.Invoke();
        }

        previousPress = inPosition;
    }

    private bool InPosition()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        return transform.localPosition.y == inRange;
    }
}