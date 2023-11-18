using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : BaseController
{
    public float upLimit; // The maximum height the ghost can float upwards.

    // Input System related variables
    private InputAction floatUpAction;
    private InputAction floatDownAction;
    private InputAction possessAction;

    // Convert input to var
    private bool isFloatingUp;
    private bool isFloatingDown;

    protected override void InitializeActionMap()
    {
        // Initialize the action map specific to the Ghost
        actionMap = inputAsset.FindActionMap("Ghost");
        movementAction = actionMap.FindAction("Move");
        floatUpAction = actionMap.FindAction("FloatUp");
        floatDownAction = actionMap.FindAction("FloatDown");
        possessAction = actionMap.FindAction("Possess");
    }

    protected override void SubscribeActions()
    {
        // Subscribe to input actions specific to the Ghost
        movementAction.started += OnMovementInput;
        movementAction.performed += OnMovementInput;
        movementAction.canceled += OnMovementInput;
        
        floatUpAction.performed += ctx => isFloatingUp = true;
        floatUpAction.canceled += ctx => isFloatingUp = false;
        floatDownAction.performed += ctx => isFloatingDown = true;
        floatDownAction.canceled += ctx => isFloatingDown = false;

        possessAction.performed += _ => TogglePossession();
    }
 
    protected override void UnsubscribeActions()
    {
        // Unsubscribe from the input actions to avoid memory leaks
        movementAction.started -= OnMovementInput;
        movementAction.performed -= OnMovementInput;
        movementAction.canceled -= OnMovementInput;
        
        floatUpAction.performed -= ctx => isFloatingUp = true;
        floatUpAction.canceled -= ctx => isFloatingUp = false;
        floatDownAction.performed -= ctx => isFloatingDown = true;
        floatDownAction.canceled -= ctx => isFloatingDown = false;

        possessAction.performed -= _ => TogglePossession();

    }

    protected override void Update()
    {
        if (actionMap.enabled)
        {
            base.Update(); 
            HandleFloating();
        }
    }

    private void HandleFloating()
    {
        if (isFloatingUp && this.transform.position.y <= upLimit)
        {
            rb.velocity = new Vector3(rb.velocity.x, 2.0f, rb.velocity.z);
        }
        else if (isFloatingDown)
        {
            rb.velocity = new Vector3(rb.velocity.x, -2.0f, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        }
    }

    private void TogglePossession()
    {
        EventBus.Publish<PossessionEvent>(new PossessionEvent(inputAsset));
    }

}
