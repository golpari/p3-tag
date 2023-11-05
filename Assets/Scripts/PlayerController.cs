using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Public variables for customization in the Unity Inspector
    public float movementSpeed;
    public float jumpForce;
    public float gravityScale;
    public int doubleJump;
    public float jumpTimeLimit;
    public float downwardGravityFactor;
    public Vector3 startingPosition;

    // Private variables to control runtime behavior
    private Rigidbody rb;
    private float jumpTime;
    private float scale;
    private bool falling;
    private bool isGrounded;
    private float gravityScaleCopy;
    private Vector2 currentMovementInput;
    private bool jumpPressed = false;

    // Input System related variables
    private InputActionAsset inputAsset;
    private InputActionMap playerActionMap;
    private InputAction movementAction;
    private InputAction jumpAction;

    private Subscription<ChangeGravityEvent> changeGravitySubscription;

    private void Awake()
    {
        // Get the Rigidbody component for physics operations
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // We'll be controlling gravity manually
        gravityScaleCopy = gravityScale; // Store the original gravity scale

        // Set up the new Input System
        inputAsset = GetComponent<PlayerInput>().actions;
        playerActionMap = inputAsset.FindActionMap("Player");
        movementAction = playerActionMap.FindAction("Move");
        jumpAction = playerActionMap.FindAction("Jump");
    }
    private void Start()
    {
        // Subscribe to the ChangeGravityEvent
        changeGravitySubscription = EventBus.Subscribe<ChangeGravityEvent>(_OnGravityChange);
    }

    private void _OnGravityChange(ChangeGravityEvent e)
    {
        // Update the gravity scale when the ChangeGravityEvent is published
        gravityScale = e.gravityScale;
    }
    private void OnEnable()
    {
        // Subscribe to the input system events
        movementAction.performed += OnMovementInput;
        movementAction.canceled += OnMovementInput;
        jumpAction.started += OnJumpStart;
        jumpAction.canceled += OnJumpCancel;
        playerActionMap.Enable();
    }

    private void OnDisable()
    {
        // Unsubscribe from the input system events
        movementAction.performed -= OnMovementInput;
        movementAction.canceled -= OnMovementInput;
        jumpAction.started -= OnJumpStart;
        jumpAction.canceled -= OnJumpCancel;
        playerActionMap.Disable();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        // Read the movement vector from the input action
        currentMovementInput = context.ReadValue<Vector2>();
    }

    private void OnJumpStart(InputAction.CallbackContext context)
    {
        // When the jump action starts, set jumpPressed to true
        jumpPressed = true;
    }

    private void OnJumpCancel(InputAction.CallbackContext context)
    {
        // When the jump action ends, set jumpPressed to false
        jumpPressed = false;
    }

    void Update()
    {
        // Update the player's jump and fall mechanics
        HandleJump();
        HandleMovement();
        HandleFall();
    }

    private void FixedUpdate()
    {
        // Apply manual gravity force in FixedUpdate for consistent physics simulation
        Vector3 gravity = -9.18f * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void HandleJump()
    {
        // Handle the jumping logic including double jumps
        if (jumpPressed && (isGrounded || doubleJump > 0))
        {
            // Perform the jump
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
            doubleJump -= 1;
        }
        else if (jumpPressed && !falling && !isGrounded)
        {
            // If holding the jump key, apply a sustained jump force
            if (jumpTime > jumpTimeLimit)
            {
                rb.velocity = Vector3.up * jumpForce * scale;
                jumpTime -= Time.deltaTime;
                scale -= Time.deltaTime;
            }
            else
            {
                // Start falling when jump is no longer being held or time limit exceeded
                falling = true;
                gravityScale *= scale;
                rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            }
        }
        else if (!jumpPressed && !falling && !isGrounded)
        {
            // Begin falling if jump is released
            falling = true;
            gravityScale *= scale;
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        }
        else if (falling)
        {
            // Apply increased gravity when falling
            if (gravityScale < gravityScaleCopy)
            {
                gravityScale += Time.deltaTime * downwardGravityFactor;
            }
        }
    }

    void HandleMovement()
    {
        // Handle the player's ground movement
        float factor = 1.0f; // A factor to reduce movement speed diagonally
        float x = currentMovementInput.x;
        float z = currentMovementInput.y;

        // Diagonal movement should be slower than straight movement
        if (Mathf.Abs(x) > 0.0f && Mathf.Abs(z) > 0.0f)
        {
            factor = 0.8f;
        }

        // Apply the movement velocity to the rb
        rb.velocity = new Vector3(x * movementSpeed * factor, rb.velocity.y, z * movementSpeed * factor);
    }

    private void ResetJump()
    {
        // Reset jumping mechanics after landing or falling off the map
        falling = false;
        jumpTime = 1.0f;
        isGrounded = true;
        EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(gravityScaleCopy)); // set gravity to default
        scale = 1.0f;
        doubleJump = 1; // Reset double jump
        transform.position = startingPosition; // Reset position
    }

    void HandleFall()
    {
        // Reset the player if they fall off the map
        if (transform.position.y < -13.0f)
        {
            ResetJump();
        }
    }
}
