using UnityEngine;

public class VerticalMoveHandler : MonoBehaviour, IPossessionAction, IMovable
{
    private bool isActive = false;

    public void EnableAction()
    {
        isActive = true;
    }
    public void DisableAction()
    {
        isActive = false;
    }
    public void Move(Vector2 currentMovementInput, float speed)
    {
        if (!isActive) return;
        // Read vertical input and apply movement
        float moveY = currentMovementInput.y;
        // Calculate the movement in Y direction
        Vector3 movement = new Vector3(0, moveY * speed * Time.deltaTime, 0);
        // Apply the movement to the object's transform
        transform.Translate(movement, Space.World);
    }

}