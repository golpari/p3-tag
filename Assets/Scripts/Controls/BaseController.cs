using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseController : MonoBehaviour
{
    public float movementSpeed;
    //public float givenAngle = 30.0f; // 270 if want to direct at end of room

    protected Rigidbody rb;
    protected Vector2 currentMovementInput;
    protected InputActionAsset inputAsset;
    protected InputActionMap actionMap;
    protected InputAction movementAction;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();

        inputAsset = GetComponent<PlayerInput>().actions;

        InitializeActionMap();
    }

    protected abstract void InitializeActionMap();

    protected virtual void OnEnable()
    {
        actionMap.Enable();
        SubscribeActions();
    }

    protected abstract void SubscribeActions();

    protected virtual void OnDisable()
    {
        UnsubscribeActions();
        actionMap.Disable();
    }

    protected abstract void UnsubscribeActions();

    protected virtual void Update()
    {
        HandleMovement();
    }
    protected virtual void HandleMovement()
    {
        // Get the directions relative to the camera's orientation
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Remove any influence of the camera's y component
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Read the input from the user
        float moveX = currentMovementInput.x; // Left and right
        float moveZ = currentMovementInput.y; // Up and down

        // Calculate the movement vector in world space
        Vector3 movement = (forward * moveZ + right * moveX) * movementSpeed;

        // Apply the movement to the Rigidbody while keeping the y-velocity
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    protected void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }

}

