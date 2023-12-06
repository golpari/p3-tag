using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
//using System.Numerics;
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
    //public float downwardGravityFactor;
    public UnityEngine.Vector3 startingPosition;

    // Private variables to control runtime behavior
    private float jumpTime;
    private float scale;
    private bool falling;
    private bool isGrounded; 
    private float gravityScaleCopy;
    private bool jumpPressed = false;
    bool lift = false;

    public static int num_lives = 3;

    float x_wall_dir = 0.0f;
    float z_wall_dir = 0.0f;


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
        EventBus.Subscribe<Reset>(_reset);
    }

    void _reset(Reset e) {
        this.gameObject.transform.parent = null;
        this.transform.position = startingPosition;
        player_lock = false;
        nextFloor = 1;
        ResetJump();
    }

    private void _OnGravityChange(ChangeGravityEvent e)
    {
        // Update the gravity scale when the ChangeGravityEvent is published
        gravityScale = ChangeGravityEvent.gravityScale;
    }

    private void _OnEndCountdown(EndCountdownEvent e)
    {
        // Reset the player's position when the EndCountdownEvent is published
        //this.transform.position = startingPosition;
        string winner = "Ghost";
        EventBus.Publish<EndGameEvent>(new EndGameEvent(winner));

    }
    
    private void _OnThiefDied(ThiefDiedEvent e)
    {
        //whenever the player dies, its lives are decreased
        /*
        num_lives -= e.livesLost;

        if (num_lives <= 0)
        {
            num_lives = 3;
            SceneManager.LoadScene(0);
        }
        */
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
        if (player_lock)
        {
            lift = true;
            
        }

        jumpPressed = true;



    }

    private void OnJumpCancel(InputAction.CallbackContext context)
    {
        // jump is no longer pressed
        // could use for pressing A button
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

            if (Mathf.Abs(rb.velocity.z) == z_wall_dir)
            {

                rb.velocity = new UnityEngine.Vector3(rb.velocity.x, rb.velocity.y, 0.0f);
            }

            if (Mathf.Abs(rb.velocity.x) == x_wall_dir)
            {
                rb.velocity = new UnityEngine.Vector3(0.0f, rb.velocity.y, rb.velocity.z);
            }
        }
        else if (tutorial.tut) {
            
        }
        else {
            if (lift)
            {
                EventBus.Publish<button_mash>(new button_mash(-0.1f));
                lift = false;
                 EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "thief"));
            }
        }
        

      //  HandleFall();
    }


    private void check_wall()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 0.5f))
        {
            z_wall_dir = 1.0f;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), 0.5f))
        {
            z_wall_dir = -1.0f;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), 0.5f))
        {
            x_wall_dir = -1.0f;
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), 0.5f))
        {
            x_wall_dir = 1.0f;
        }
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
        if (jumpPressed && isGrounded)
        {
            // Perform the jump
            rb.velocity = Vector3.up * jumpForce;
            isGrounded = false;
            animator.SetBool("isJumping", true);
            //doubleJump -= 1;
        }
        
    }


    bool check_ground(Collision collision)
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity) &&
            (collision.gameObject.tag == "floor" || collision.gameObject.tag == "Possessable" || collision.gameObject.tag == "Lava"))
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

        if (collision.gameObject.CompareTag("Ghost") && spirit_slider.current_value > 75.0f)
        {
            spirit_slider.current_value -= 75.0f;
            StartCoroutine(FreezePlayer());
        }

    }

    private IEnumerator FreezePlayer()
    {
        player_lock = true;

        Camera.main.GetComponent<FrostEffect>().enabled = true;
        animator.enabled = false;

        yield return new WaitForSeconds(2.0f);

        animator.enabled = true;
        Camera.main.GetComponent<FrostEffect>().enabled = false;

        player_lock = false;
    }

    private void ResetJump()
    {
        // Reset jumping mechanics after landing or falling off the map
        falling = false;
        jumpTime = 1.0f;
        isGrounded = true;
        animator.SetBool("isJumping", false);
        scale = 1.5f;
        doubleJump = 1; // Reset double jump
        jumpPressed = false;
        //gravityScale = gravityScaleCopy;
    }

    private void HandleFall()
    {
        // Reset the player if they fall off the map
        if (transform.position.y < -13.0f)
        {
            ResetJump();
            //this.transform.position = startingPosition;
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
            if (nextFloor < 3) { // change this value later
                EventBus.Publish<ChangeDoorsEvent>(new ChangeDoorsEvent(nextFloor));
                nextFloor += 1;
            }
            
        }
    }

}
