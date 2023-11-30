using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDirectionMovementHandler : PossessionActionBase, IMovable
{
    private bool isActive = false;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxZ = 10f;
    [SerializeField] private float minZ = -10f;
    [SerializeField] private float maxY = 10f;
    [SerializeField] private float minY = -10f;

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
        float moveX = currentMovementInput.x; // Left and right
        float moveZ = currentMovementInput.y; // Up and down
        float moveY = currentFloatInput.y;

        // constrain the new Y position
        //moveY = Mathf.Clamp(moveY, minY, maxY);

        // Calculate the movement vector in world space
        Vector3 movement = (forward * moveZ + right * moveX) * speed * Time.deltaTime;
        movement.y = moveY * speed * Time.deltaTime;

        /*// Constrain the new X and Z positions
        movement.x = Mathf.Clamp(movement.x, minX, maxX);
        movement.z = Mathf.Clamp(movement.z, minZ, maxZ);*/

        transform.Translate(movement, Space.World);

    }
}