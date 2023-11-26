using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PossessionController : BaseController
{
    private InputAction possessAction;
    private InputAction toggleAction;
    protected GhostSelection ghostSelection;
    Subscription<PossessionEvent> possessionSubscription;
    private PossessionActionBase currPossessionAction;
    GameObject currObject;


    void Start()
    {
        possessionSubscription = EventBus.Subscribe<PossessionEvent>(_OnPossession);
        ghostSelection = GetComponent<GhostSelection>();
        inputAsset.FindActionMap("Possession").Disable();
        EventBus.Subscribe<ChangeDoorsEvent>(_turn_off);
    }

    void _turn_off(ChangeDoorsEvent e) {
    if (currPossessionAction != null) {
            inputAsset.FindActionMap("Possession").Disable();
            inputAsset.FindActionMap("Ghost").Enable();
            currPossessionAction?.DisableAction();
            currObject = null;
            currPossessionAction = null;
            spirit_slider.zero_spirit = false;
        }
    }

    private void _OnPossession(PossessionEvent e)
    {
        // Check to see which map is active
        if (e.inputAsset.FindActionMap("Ghost").enabled)
        {
            // make sure there is a possessable object to possess
            currObject = ghostSelection.GetClosestObject();
            if (!currObject)
                return;

            // enable possession depending on what type it is
            currPossessionAction = currObject.GetComponent<PossessionActionBase>();
            bool canPay = currPossessionAction.EnableAction();
            if (!canPay) return;

            // Change Map to Possession Controls if possession succeeded (could pay)
            e.inputAsset.FindActionMap("Ghost").Disable();
            e.inputAsset.FindActionMap("Possession").Enable();
        }
        else if (e.inputAsset.FindActionMap("Possession").enabled)
        {
            // Change Map to Ghost Controls
            e.inputAsset.FindActionMap("Possession").Disable();
            e.inputAsset.FindActionMap("Ghost").Enable();

            // Reset the action too and the vars
            currPossessionAction?.DisableAction();
            currObject = null;
            currPossessionAction = null;
        }
        else
        {
            Debug.Log("Error: Neither action map is enabled");
        }
    }

    protected override void InitializeActionMap()
    {
        // Initialize the action map specific to Possession
        actionMap = inputAsset.FindActionMap("Possession");
        movementAction = actionMap.FindAction("Move");
        possessAction = actionMap.FindAction("Possess");
        toggleAction = actionMap.FindAction("Toggle");
    }

    protected override void SubscribeActions()
    {
        // Subscribe to input actions specific to Possession
        movementAction.started += OnMovementInput;
        movementAction.performed += OnMovementInput;
        movementAction.canceled += OnMovementInput;
        possessAction.performed += _ => TogglePossession();
        toggleAction.performed += _ => AttemptToggle();
    }

    protected override void UnsubscribeActions()
    {
        // Unsubscribe from the input actions to avoid memory leaks
        movementAction.started -= OnMovementInput;
        movementAction.performed -= OnMovementInput;
        movementAction.canceled -= OnMovementInput;
        possessAction.performed -= _ => TogglePossession();
        toggleAction.performed -= _ => AttemptToggle();
    }

    // Only run update if the action map is enabled
    protected override void Update()
    {
        if (actionMap.enabled)
            base.Update();

        // Rohun Changes
        if (spirit_slider.zero_spirit) {
            inputAsset.FindActionMap("Possession").Disable();
            inputAsset.FindActionMap("Ghost").Enable();
            currPossessionAction?.DisableAction();
            currObject = null;
            currPossessionAction = null;
            spirit_slider.zero_spirit = false;
        }
            // Rohun Changes

    }




    //call this functiondisable


    protected override void HandleMovement()
    {
        // Implement movement logic for the possessed object
        if (currPossessionAction is IMovable movable)
            movable.Move(currentMovementInput, movementSpeed);
    }

    private void AttemptToggle()
    {

        // Check if the current possession action is a LightingHandler
        if (currPossessionAction is LightHandler lightHandler)
        { 
            lightHandler.ToggleLighting();
        }

        // check if curr possession is a low grav type
        if (currPossessionAction is LowGravityHandler lowGravityHandler)
        {
            lowGravityHandler.ToggleGravity();
        }
    }

    private void TogglePossession()
    {
        // This is to return to the normal, non possession controls, this shouldn't cost anything
        EventBus.Publish<PossessionEvent>(new PossessionEvent(inputAsset));
            
    }
}

//// default movement is can move in any direction
//private void defaultMovement(GameObject currObject)
//{
//    // Get the directions relative to the camera's orientation
//    Vector3 forward = Camera.main.transform.forward;
//    Vector3 right = Camera.main.transform.right;

//    // Remove any influence of the camera's y component
//    forward.y = 0;
//    right.y = 0;
//    forward.Normalize();
//    right.Normalize();

//    // Read the input from the user
//    float moveX = currentMovementInput.x; // Left and right
//    float moveZ = currentMovementInput.y; // Up and down

//    // Calculate the movement vector in world space
//    Vector3 movement = (forward * moveZ + right * moveX) * movementSpeed * Time.deltaTime;

//    // Apply the movement to the new object's Rigidbody while keeping the y-velocity
//    //curr_rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
//    if (currObject != null)
//        currObject.transform.Translate(movement, Space.World);
//}
