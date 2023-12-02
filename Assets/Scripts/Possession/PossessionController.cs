using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PossessionController : BaseController
{
    private InputAction possessAction;
    private InputAction toggleAction;
    private InputAction floatUpAction;
    private InputAction floatDownAction;

    private bool isFloatingUp;
    private bool isFloatingDown;


    protected GhostSelection ghostSelection;
    Subscription<PossessionEvent> possessionSubscription;
    
    private PossessionActionBase currPossessionAction;
    GameObject currObject;

    bool lock_ghost = false;

    void Start()
    {
        possessionSubscription = EventBus.Subscribe<PossessionEvent>(_OnPossession);
        ghostSelection = GetComponent<GhostSelection>();
        inputAsset.FindActionMap("Possession").Disable();
        EventBus.Subscribe<ChangeDoorsEvent>(_turn_off);
        lock_ghost = false;
    }

    void _turn_off(ChangeDoorsEvent e)
    {
        if (currPossessionAction != null)
        {
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
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
            lock_ghost = false;
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
        floatAction = actionMap.FindAction("Float");
        possessAction = actionMap.FindAction("Possess");
        toggleAction = actionMap.FindAction("Toggle");
        floatUpAction = actionMap.FindAction("FloatUp");
        floatDownAction = actionMap.FindAction("FloatDown");


    }

    protected override void SubscribeActions()
    {
        // Subscribe to input actions specific to Possession
        movementAction.started += OnMovementInput;
        movementAction.performed += OnMovementInput;
        movementAction.canceled += OnMovementInput;
        
        floatAction.started += OnFloatInput;
        floatAction.performed += OnFloatInput;
        floatAction.canceled += OnFloatInput;
        
        possessAction.performed += _ => TogglePossession();
        toggleAction.performed += _ => AttemptToggle();

        floatUpAction.performed += ctx => isFloatingUp = true;
        floatUpAction.canceled += ctx => isFloatingUp = false;
        floatDownAction.performed += ctx => isFloatingDown = true;
        floatDownAction.canceled += ctx => isFloatingDown = false;
    }

    protected override void UnsubscribeActions()
    {
        // Unsubscribe from the input actions to avoid memory leaks
        movementAction.started -= OnMovementInput;
        movementAction.performed -= OnMovementInput;
        movementAction.canceled -= OnMovementInput;

        floatAction.started -= OnFloatInput;
        floatAction.performed -= OnFloatInput;
        floatAction.canceled -= OnFloatInput;

        possessAction.performed -= _ => TogglePossession();
        toggleAction.performed -= _ => AttemptToggle();

        floatUpAction.performed -= ctx => isFloatingUp = true;
        floatUpAction.canceled -= ctx => isFloatingUp = false;
        floatDownAction.performed -= ctx => isFloatingDown = true;
        floatDownAction.canceled -= ctx => isFloatingDown = false;
    }

    // Only run update if the action map is enabled
    protected override void Update()
    {
        if (actionMap.enabled)
        {
            base.Update();
        }
            

        // Rohun Changes
        if (spirit_slider.zero_spirit)
        {
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
        //(bool, bool) floatStatus = (isFloatingUp, isFloatingDown);
        if (currPossessionAction is IMovable movable)
            movable.Move(currentMovementInput, movementSpeed, currentFloatInput);
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
