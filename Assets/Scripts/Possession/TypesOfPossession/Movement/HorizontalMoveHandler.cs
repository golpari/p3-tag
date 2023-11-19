using UnityEngine;

public class HorizontalMoveHandler : PossessionActionBase, IMovable
{
    private bool isActive = false;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minX = -10f; 

    public override bool EnableAction()
    {
        isActive = true;
        return true;
    }
    public override void DisableAction()
    {
        isActive = false;
    }
    public void Move(Vector2 currentMovementInput, float speed)
    {
        if (!isActive) return;
        //// Read vertical input and apply movement
        float moveX = currentMovementInput.x * speed * Time.deltaTime;

        // Get the current position and apply the intended movement
        Vector3 newPosition = transform.position + new Vector3(moveX, 0, 0);

        // Constrain the new X position
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Apply the constrained position to the object's transform
        transform.position = newPosition;
    }

}