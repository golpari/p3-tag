using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : BaseController
{
    public float upLimit; // The maximum height the ghost can float upwards.

    // Flags for gravity state and lighting state.
    private bool isStrongGravity;
    private bool isLowGravity;
    // needs to start flipped for some reason
    private bool isDark = true;

    // Gravity scale presets for different gravity states.
    [SerializeField] private float defaultGravityScale = 1.0f;
    [SerializeField] private float strongGravityScale = 10.0f;
    [SerializeField] private float lowGravityScale = 0.25f;

    // Input System related variables
    private InputAction lightAction;
    private InputAction gravityAction;
    private InputAction floatUpAction;
    private InputAction floatDownAction;

    // Convert input to var
    private bool isFloatingUp;
    private bool isFloatingDown;
    protected override void InitializeActionMap()
    {
        // Initialize the action map specific to the Ghost
        actionMap = inputAsset.FindActionMap("Ghost");
        movementAction = actionMap.FindAction("Move");
        lightAction = actionMap.FindAction("ToggleLight");
        gravityAction = actionMap.FindAction("ToggleGravity");
        floatUpAction = actionMap.FindAction("FloatUp");
        floatDownAction = actionMap.FindAction("FloatDown");
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

        lightAction.performed += _ => ToggleLighting();
        gravityAction.performed += _ => ToggleGravity();
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

        lightAction.performed -= _ => ToggleLighting();
        gravityAction.performed -= _ => ToggleGravity();

    }

    protected override void Update()
    {
        base.Update(); 
        HandleFloating();
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
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        }
    }

    private void ToggleGravity()
    {
        // Default to strong gravity.
        if (!isStrongGravity && !isLowGravity)
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(strongGravityScale));
            isStrongGravity = true;
            isLowGravity = false;
        }
        // Strong gravity to low gravity.
        else if (isStrongGravity)
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(lowGravityScale));
            isStrongGravity = false;
            isLowGravity = true;
        }
        // Low gravity to default gravity.
        else if (isLowGravity)
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(defaultGravityScale));
            isStrongGravity = false;
            isLowGravity = false;
        }
    }

    private void ToggleLighting()
    {
        isDark = !isDark;
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
    }

}
