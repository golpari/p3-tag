using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



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
<<<<<<< HEAD
    bool super = false;
=======

>>>>>>> 42947fcc8f92e2a8e053db779f02fbc55fe99b4c
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


            if (Input.GetKeyDown(KeyCode.Space) && spirit_slider.current_value >= 100 && !super)
            {
                EventBus.Publish<SpiritEvent>(new SpiritEvent(-100));
                StartCoroutine(super_power());


            }

        }




        // delete after
    }

    public IEnumerator super_power() {
        super = true;
        ToggleGravity(); // something wrong with gravity scale
        yield return new WaitForSeconds(5.0f); // 5 seconds
        ToggleGravity();
        super = false;
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

<<<<<<< HEAD
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

    private void ToggleLighting()
    {
        isDark = !isDark;
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
        
        
    }

=======
>>>>>>> 42947fcc8f92e2a8e053db779f02fbc55fe99b4c
    private void TogglePossession()
    {
        if (spirit_slider.current_value >= 75f) {
            EventBus.Publish<PossessionEvent>(new PossessionEvent(inputAsset));
        }
        
    }

}

// couldn't we have one function that does this?