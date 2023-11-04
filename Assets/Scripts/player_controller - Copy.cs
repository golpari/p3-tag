using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class player_controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public float movement_speed; // 5.0f
    public float jump_force; // 5.0f
    public float gravity_scale; // 1.0f
    public int doubleJump; // 1
    public float jump_time_limit; // 0.25f
    public float downward_gravity_factor; // 1.0f
    public Vector3 starting_position; // Vector3(16, -10.5, -1.5)

    private float jump_time = 1.0f;
    private float scale = 1.0f;
    private bool falling = false;
    private bool isGrounded = true;
    private float gravity_scale_copy;

    float x_wall_dir = 0.0f;
    float z_wall_dir = 0.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        gravity_scale_copy = gravity_scale;
    }

    public void SetGravityScale(float scale)
    {
        gravity_scale = scale;
        gravity_scale_copy = scale;
    }

    // Update is called once per frame
    void Update()
    {
        // order is important
        handle_jump();
        handle_movement(); 
        handle_fall();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            Reset_jump();
        }

        check_wall();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("floor"))
        {
            z_wall_dir = 0.0f;
            x_wall_dir = 0.0f;
        }
    }


    private void FixedUpdate()
    {
        Vector3 gravity = -9.81f * gravity_scale * Vector3.up;
        Debug.Log(gravity_scale);

        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void handle_jump()
    {
        if (Input.GetKeyDown(KeyCode.Z) && (isGrounded || doubleJump >= 0))
        {
            rb.velocity = Vector3.up * jump_force;
            isGrounded = false;
            doubleJump -= 1;
        }
        else if (Input.GetKey(KeyCode.Z) && !falling && !isGrounded)
        {
            if (jump_time > jump_time_limit)
            {
                rb.velocity = Vector3.up * jump_force * scale;
                jump_time -= Time.deltaTime;
                scale -= Time.deltaTime; 
            }
            else
            {
                falling = true;
                gravity_scale *= scale;
                rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.y);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z) && !falling && !isGrounded)
        {
            falling = true;
            gravity_scale *= scale;
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.y);
        }
        else if (falling)
        {
            if (gravity_scale < gravity_scale_copy)
            {
                gravity_scale += Time.deltaTime * downward_gravity_factor;
            }
        }
    }

    void handle_movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        float factor = 1.0f;

        if (Mathf.Abs(x) > 0.0f && Mathf.Abs(z) > 0.0f)
        {
            factor = 0.8f;
        }
        
        if (z == z_wall_dir)
        {
           
            z = 0.0f;
        }

        if (x == x_wall_dir)
        {
            x = 0.0f;
        }

        rb.velocity = new Vector3(x * movement_speed * factor, rb.velocity.y, z * movement_speed * factor);
    }

    private void Reset_jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
        falling = false;
        jump_time = 1.0f;
        isGrounded = true;
        gravity_scale = gravity_scale_copy;
        scale = 1.0f;
        doubleJump = 1;
    }

    void handle_fall()
    {
        if (this.transform.position.y < -13.0f)
        {
            Reset_jump();
            this.transform.position = starting_position;
        }
    }
}
