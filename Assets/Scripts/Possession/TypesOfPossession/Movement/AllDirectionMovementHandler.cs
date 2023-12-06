using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDirectionMovementHandler : PossessionActionBase, IMovable
{
    private bool isActive = false;
    [SerializeField] private float maxX = 100f;
    [SerializeField] private float minX = -100f;
    [SerializeField] private float maxZ = 100f;
    [SerializeField] private float minZ = -100f;
    [SerializeField] private float maxY = 100f;
    [SerializeField] private float minY = -100f;


    Vector3 starting_position;
    Transform parent;

    private void Awake()
    {
        spiritPrice = 1f;
    }

    protected override void Start()
    {
        base.Start();
        parent = this.gameObject.transform.parent;
        starting_position = transform.position;
        EventBus.Subscribe<Reset>(_reset);
    }
    void _reset(Reset e)
    {
        this.gameObject.transform.parent = parent;
        this.transform.position = starting_position;
        this.GetComponent<Rigidbody>().useGravity = true;
    }


    public override bool EnableAction()
    {
        if (spirit_slider.current_value <= 0) return false;

        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().useGravity = false;
        EventBus.Publish<SpiritPossesion>(new SpiritPossesion(true, -5.0f));
        isActive = true;
        return true;
    }
    public override void DisableAction()
    {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().useGravity = true;
        isActive = false;
        if (spirit_slider.current_value > 0.0f)
        {
            EventBus.Publish<SpiritPossesion>(new SpiritPossesion(false));
        }

    }
    public void Move(Vector2 currentMovementInput, float speed, (bool isFloatingUp, bool isFloatingDown) floatStatus)
    {
        if (!isActive) {
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Remove any influence of the camera's y component
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();



        float moveX = currentMovementInput.x; // Left and right
        float moveZ = currentMovementInput.y; // Up and down

        // Define vertical speed
        float verticalSpeed = 5.0f; // Adjust this value as needed
        float moveY = 0f;

        // no y movement when both buttons are pressed
        if (floatStatus.isFloatingUp && !floatStatus.isFloatingDown)
        {
            moveY = verticalSpeed;
        }

        else if (floatStatus.isFloatingDown && !floatStatus.isFloatingUp)
        {
            moveY = -verticalSpeed;
        }



        Vector3 movement = (forward * moveZ + right * moveX) * speed;
        movement.y = moveY;

        this.GetComponent<Rigidbody>().velocity = movement;
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x, minX, maxX),
            Mathf.Clamp(this.transform.position.y, minY, maxY),
            Mathf.Clamp(this.transform.position.z, minZ, maxZ));
      
    }


    /*
     Barb changes

     // Read the input from the user
        float moveX = currentMovementInput.x * speed * Time.deltaTime; // Left and right
        float moveZ = currentMovementInput.y * speed * Time.deltaTime; // Up and down
        float moveY = currentFloatInput.y * speed * Time.deltaTime;

        Vector3 newPosition = transform.position + new Vector3(moveX, moveY, moveZ);

        //// Calculate the movement vector in world space
        //Vector3 movement = (forward * moveZ + right * moveX) * speed * Time.deltaTime;
        //movement.y = moveY * speed * Time.deltaTime;

        // Constrain the new X and Z positions
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        //transform.Translate(movement, Space.World);
        transform.position = newPosition;
     
     */
}