using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    public float jumpForce;
    public float gravityScale;
    public int doubleJump;
    public float jumpTimeLimit;
    public float downwardGravityFactor;
    public Vector3 startingPosition;

    // Private variables to control runtime behavior
    private float jumpTime;
    private float scale;
    private bool falling;
    private bool isGrounded; 
    private float gravityScaleCopy;
    private bool jumpPressed = false;

    public static int num_lives = 3;

    // Input System related variables
    private InputAction jumpAction;

    private Subscription<ThiefDiedEvent> thiefDiedSubscription;
    private Subscription<ChangeGravityEvent> changeGravitySubscription;
    private Subscription<EndCountdownEvent> endCountdownSubscription;


    public static bool player_lock = false;
    int nextFloor = 1;
    protected override void InitializeActionMap()
    {
        actionMap = inputAsset.FindActionMap("Player");
        movementAction = actionMap.FindAction("Move");
        jumpAction = actionMap.FindAction("Jump");

        rb.useGravity = false; // We'll be controlling gravity manually
        gravityScaleCopy = gravityScale; // Store the original gravity scale
    }

    private void Start()
    {
        // Subscribe to the ChangeGravityEvent
        changeGravitySubscription = EventBus.Subscribe<ChangeGravityEvent>(_OnGravityChange);
        // Subscribe to the EndCountdownEvent
        endCountdownSubscription = EventBus.Subscribe<EndCountdownEvent>(_OnEndCountdown);
        //Subscript to the thiefDiedEvent
        thiefDiedSubscription = EventBus.Subscribe<ThiefDiedEvent>(_OnThiefDied);
        player_lock = false;
    }
    private void _OnGravityChange(ChangeGravityEvent e)
    {
        // Update the gravity scale when the ChangeGravityEvent is published
        gravityScale = ChangeGravityEvent.gravityScale;
    }

    private void _OnEndCountdown(EndCountdownEvent e)
    {
        // Reset the player's position when the EndCountdownEvent is published
        this.transform.position = startingPosition;
        string winner = "Ghost";
        EventBus.Publish<EndGameEvent>(new EndGameEvent(winner));

    }
    
    private void _OnThiefDied(ThiefDiedEvent e)
    {
        //whenever the player dies, its lives are decreased
        num_lives -= e.livesLost;

        if (num_lives <= 0)
        {
            num_lives = 3;
            SceneManager.LoadScene(0);
        }
    }
    protected override void SubscribeActions()
    {
        movementAction.started += OnMovementInput;
        movementAction.performed += OnMovementInput;
        movementAction.canceled += OnMovementInput;
        jumpAction.started += OnJumpStart;
        jumpAction.canceled += OnJumpCancel;

    }

    protected override void UnsubscribeActions()
    {
        movementAction.started -= OnMovementInput;
        movementAction.performed -= OnMovementInput;
        movementAction.canceled -= OnMovementInput;
        jumpAction.started -= OnJumpStart;
        jumpAction.canceled -= OnJumpCancel;
    }

    private void OnJumpStart(InputAction.CallbackContext context)
    {
        // jump button is pressed
        jumpPressed = true;
    }

    private void OnJumpCancel(InputAction.CallbackContext context)
    {
        // jump is no longer pressed
        jumpPressed = false;
    }

    protected override void Update()
    {
        // Update the player's jump and fall mechanics
        // order is important
        if (!player_lock)
        {
            HandleJump();
            HandleMovement();
        }
        else {
            if (Input.GetKey(KeyCode.Space)) {
                EventBus.Publish<button_mash>(new button_mash(-0.1f));
            }
        }
        

      //  HandleFall();
    }

    void handle_button_mash() { 
    
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
            animator.SetBool("isJumping", true);
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
                //gravityScale *= scale;
                rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            }
        }
        else if (!jumpPressed && !falling && !isGrounded)
        {
            // Begin falling if jump is released
            falling = true;
            //gravityScale *= scale;
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        }
        else if (falling)
        {
            // Apply increased gravity when falling
            if (gravityScale < gravityScaleCopy)
            {
                //gravityScale += Time.deltaTime * downwardGravityFactor;
            }
        }
    }


    bool check_ground(Collision collision)
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity) &&
            (collision.gameObject.tag == "floor" || collision.gameObject.tag == "jump_obj"))
        {
            return true;
        }
        return false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (check_ground(collision))
        {
            ResetJump();
        }

    }

    private void ResetJump()
    {
        // Reset jumping mechanics after landing or falling off the map
        falling = false;
        jumpTime = 1.0f;
        isGrounded = true;
        animator.SetBool("isJumping", false);
        //EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(gravityScaleCopy)); // set gravity to default
        scale = 1.0f;
        doubleJump = 1; // Reset double jump
    }

    private void HandleFall()
    {
        // Reset the player if they fall off the map
        if (transform.position.y < -13.0f)
        {
            ResetJump();
            this.transform.position = startingPosition;
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(changeGravitySubscription);
        EventBus.Unsubscribe(endCountdownSubscription);
        EventBus.Unsubscribe(thiefDiedSubscription);
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "room_change" && !player_lock)
        {
            EventBus.Publish<ChangeDoorsEvent>(new ChangeDoorsEvent(nextFloor));
            nextFloor += 1;
        }

    }

}
