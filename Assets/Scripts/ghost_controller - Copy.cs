using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ghost_controller : MonoBehaviour
{
    public Rigidbody rb;
    public float movement_speed; // 5.0f;
    float x_wall_dir = 0.0f;
    float z_wall_dir = 0.0f;
    public float up_limit;

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


        check_wall();
    }
    private void OnCollisionExit(Collision collision)
    {

        z_wall_dir = 0.0f;
        x_wall_dir = 0.0f;


    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        handle_float();
        handle_movement();
    }


    void handle_float() {
        if (Input.GetKey(KeyCode.Z) && this.transform.position.y <= up_limit)
        {
            rb.velocity = new Vector3(rb.velocity.x, 2.0f, rb.velocity.z);

        }
        else if (Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector3(rb.velocity.x, -2.0f, rb.velocity.z);
        }
        else {
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
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
}
