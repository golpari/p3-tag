using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseController : MonoBehaviour
{
    public float movementSpeed;
    //public float givenAngle = 30.0f; // 270 if want to direct at end of room

    protected Rigidbody rb;
    protected Vector2 currentMovementInput;
    protected Vector2 currentFloatInput;
    public InputActionAsset inputAsset;
    protected InputActionMap actionMap;
    protected InputAction movementAction;
    protected InputAction floatAction;
    public Animator animator;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

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
        //if (actionMap.enabled)
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

        if (currentMovementInput.x != 0.0f ||
            currentMovementInput.y != 0.0f)
        {
            SetAnimatorBool("isWalking", true);
        }
        else
        {
            SetAnimatorBool("isWalking", false);
        }

        // Calculate the movement vector in world space
        Vector3 movement = (forward * moveZ + right * moveX) * movementSpeed;

        //float x = Input.GetAxisRaw("Horizontal");
        //float z = Input.GetAxisRaw("Vertical");


        // delete for noncontroller use
        //rb.velocity = new Vector3(x * movementSpeed, rb.velocity.y, z * movementSpeed);

        // Apply the movement to the Rigidbody while keeping the y-velocity
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        if (movement != Vector3.zero)
        {
            //transform.forward = movement;

            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720.0f * Time.deltaTime);
        }
    }

    protected virtual void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>();
    }

    protected void OnFloatInput(InputAction.CallbackContext context)
    {
        currentFloatInput = context.ReadValue<Vector2>();
    }

    // Necessary bc ghost animation doesn't have an isWalking val
    private void SetAnimatorBool(string paramName, bool value)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName && param.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(paramName, value);
                return;
            }
        }
    }

}

