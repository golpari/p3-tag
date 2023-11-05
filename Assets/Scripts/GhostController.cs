using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostController : MonoBehaviour
{
    public Rigidbody rb;
    public float movement_speed;
    public float up_limit; // The maximum height the ghost can float upwards.

    // Flags for gravity state and lighting state.
    private bool isStrongGravity;
    private bool isLowGravity;

    // Gravity scale presets for different gravity states.
    [SerializeField] private float defaultGravityScale = 1.0f;
    [SerializeField] private float strongGravityScale = 10.0f;
    [SerializeField] private float lowGravityScale = 0.25f;

    // Input System related variables
    private InputActionAsset inputAsset;
    private InputActionMap ghostActionMap;
    private InputAction movementAction;
    private InputAction lightAction;
    private InputAction gravityAction;
    private InputAction floatUpAction;
    private InputAction floatDownAction;

    // Convert input to var
    private Vector2 currentMovementInput;
    private bool isFloatingUp;
    private bool isFloatingDown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Set up input system
        inputAsset = this.GetComponent<PlayerInput>().actions;

        ghostActionMap = inputAsset.FindActionMap("Player");
  
        movementAction = ghostActionMap.FindAction("Move");
        lightAction = ghostActionMap.FindAction("ToggleLight");
        gravityAction = ghostActionMap.FindAction("ToggleGravity");
        floatUpAction = ghostActionMap.FindAction("FloatUp");
        floatDownAction = ghostActionMap.FindAction("FloatDown");

    }

    private void OnEnable()
    {
        ghostActionMap.Enable();
        // Subscribe to action events
        movementAction.performed += ctx => currentMovementInput = ctx.ReadValue<Vector2>();
        movementAction.canceled += ctx => currentMovementInput = Vector2.zero;

        floatUpAction.performed += ctx => isFloatingUp = true;
        floatUpAction.canceled += ctx => isFloatingUp = false;

        floatDownAction.performed += ctx => isFloatingDown = true;
        floatDownAction.canceled += ctx => isFloatingDown = false;

        lightAction.performed += _ => ToggleLighting();
        gravityAction.performed += _ => ToggleGravity();
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


    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleFloating();
    }

    private void HandleFloating()
    {
        if (isFloatingUp && this.transform.position.y <= up_limit)
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

    private void HandleMovement()
    {
        Vector3 move = new Vector3(currentMovementInput.x, 0, currentMovementInput.y) * movement_speed;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);
    }

    private void ToggleLighting()
    {
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent());
    }

    // Unsubscribe from events to avoid memory leaks
    private void OnDisable()
    {
        // Unsubscribe from all the action events
        movementAction.performed -= ctx => currentMovementInput = ctx.ReadValue<Vector2>();
        movementAction.canceled -= ctx => currentMovementInput = Vector2.zero;

        floatUpAction.performed -= ctx => isFloatingUp = true;
        floatUpAction.canceled -= ctx => isFloatingUp = false;

        floatDownAction.performed -= ctx => isFloatingDown = true;
        floatDownAction.canceled -= ctx => isFloatingDown = false;

        lightAction.performed -= _ => ToggleLighting();
        gravityAction.performed -= _ => ToggleGravity();
    }
}
