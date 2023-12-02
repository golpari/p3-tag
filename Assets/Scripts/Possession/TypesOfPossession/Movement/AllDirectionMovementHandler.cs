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

    public override bool EnableAction()
    {
        isActive = true;
        return true;
    }
    public override void DisableAction()
    {
        isActive = false;
    }
    public void Move(Vector2 currentMovementInput, float speed, Vector2 currentFloatInput)
    {
        if (!isActive) return;

        // Get the directions relative to the camera's orientation
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Remove any influence of the camera's y component
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

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

    }
}