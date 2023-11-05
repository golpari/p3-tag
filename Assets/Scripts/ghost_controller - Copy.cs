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
    private bool isStrongGravity;
    private bool isLowGravity;
    bool isDark;

    [SerializeField] private GameObject player;
    [SerializeField] private float defaultGravityScale = 1.0f;
    [SerializeField] private float strongGravityScale = 10.0f;
    [SerializeField] private float lowGravityScale = 0.25f;

    [SerializeField] private GameObject mainLight;
    [SerializeField] private GameObject[] torches;

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
        HandleGravity();
        HandleLighting();
    }

    void handle_float()
    {
        if (Input.GetKey(KeyCode.Z) && this.transform.position.y <= up_limit)
        {
            rb.velocity = new Vector3(rb.velocity.x, 2.0f, rb.velocity.z);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            rb.velocity = new Vector3(rb.velocity.x, -2.0f, rb.velocity.z);
        }
        else
        {
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

    private void HandleGravity()
    {
        // Default to strong gravity.
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isStrongGravity && !isLowGravity)
        {
            player.GetComponent<player_controller>().SetGravityScale(strongGravityScale);
            isStrongGravity = true;
            isLowGravity = false;
        }
        // Strong gravity to low gravity.
        else if (Input.GetKeyDown(KeyCode.LeftShift) && isStrongGravity)
        {
            player.GetComponent<player_controller>().SetGravityScale(lowGravityScale);
            isStrongGravity = false;
            isLowGravity = true;
        }
        // Low gravity to default gravity.
        else if (Input.GetKeyDown(KeyCode.LeftShift) && isLowGravity)
        {
            player.GetComponent<player_controller>().SetGravityScale(defaultGravityScale);
            isStrongGravity = false;
            isLowGravity = false;
        }
    }

    private void HandleLighting()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isDark)
        {
            mainLight.GetComponent<Light>().intensity = 0.1f;

            for (int i = 0; i < torches.Length; i++)
            {
                torches[i].GetComponent<Light>().intensity = 1.0f;
            }

            isDark = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isDark)
        {
            mainLight.GetComponent<Light>().intensity = 1.0f;

            for (int i = 0; i < torches.Length; i++)
            {
                torches[i].GetComponent<Light>().intensity = 3.0f;
            }

            isDark = false;
        }
    }
}
