using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;



//using System.Numerics;

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

    bool super = false;

    private bool isLowGravity = false;

    public float defaultGravityScale;
    public float lowGravityScale;
    public GameObject gravityFX;

    bool tempToggle = true;

    Vector3 starting_position;

    public static bool  ghost_lock;

    private void Start()
    {
        ghost_lock = false;
        EventBus.Subscribe<Reset>(_reset);
        starting_position = this.transform.position;
    }

    void _reset(Reset e) {
        GhostController.ghost_lock = false;
        this.transform.position = starting_position;
    }

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
        floatUpAction.performed += ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent("button_b", "ghost"));
        floatUpAction.canceled += ctx => isFloatingUp = false;
        floatUpAction.performed += ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent(null, "ghost"));
        floatDownAction.performed += ctx => isFloatingDown = true;
        floatUpAction.performed += ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent("button_a", "ghost"));
        floatDownAction.canceled += ctx => isFloatingDown = false;
        floatUpAction.performed += ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent(null, "ghost"));

        possessAction.performed += _ => TogglePossession();

    }
 
    protected override void UnsubscribeActions()
    {
        // Unsubscribe from the input actions to avoid memory leaks
        movementAction.started -= OnMovementInput;
        movementAction.performed -= OnMovementInput;
        movementAction.canceled -= OnMovementInput;

        floatUpAction.performed -= ctx => isFloatingUp = true;
        floatUpAction.performed -= ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent("button_b", "ghost"));
        floatUpAction.canceled -= ctx => isFloatingUp = false;
        floatUpAction.performed -= ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent(null, "ghost"));
        floatDownAction.performed -= ctx => isFloatingDown = true;
        floatUpAction.performed -= ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent("button_a", "ghost"));
        floatDownAction.canceled -= ctx => isFloatingDown = false;
        floatUpAction.performed -= ctx => EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent(null, "ghost"));

        possessAction.performed -= _ => TogglePossession();

    }

    protected override void Update()
    {
        if (!ghost_lock) {
            if (actionMap.enabled)
            {
                base.Update();
                HandleFloating();


            }
        }

    }

    protected override void OnMovementInput(InputAction.CallbackContext context)
    {
        base.OnMovementInput(context);
        EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent("joystick2_left", "ghost"));
    }


    private void HandleFloating()
    {
        
        if (isFloatingUp && this.transform.position.y <= upLimit)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5.0f, rb.velocity.z);
        }
        else if (isFloatingDown)
        {
            rb.velocity = new Vector3(rb.velocity.x, -5.0f, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        }
    }

    private void ToggleGravity()
    {
        // Default to low gravity.
        if (!isLowGravity)
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(lowGravityScale, gravityFX));
            isLowGravity = true;
        }
        // Low gravity to default gravity.
        else
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(defaultGravityScale, gravityFX));
            isLowGravity = false;
        }
    }

    private void TogglePossession()
    {
        // temporary for testing
        /*if (tempToggle)
            EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "ghost"));
        else
            EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        tempToggle = !tempToggle;*/
        // end temporary

        // if we add a spirit limit here then it applies to all types of possession, do it within handler scripts
        EventBus.Publish<PossessionEvent>(new PossessionEvent(inputAsset));
        EventBus.Publish<ButtonPressEvent>(new ButtonPressEvent("button_y", "ghost"));
    }

    public (bool, bool) GetFloatStatus()
    {
        return (isFloatingUp, isFloatingDown);
    }


}

// couldn't we have one function that does this?