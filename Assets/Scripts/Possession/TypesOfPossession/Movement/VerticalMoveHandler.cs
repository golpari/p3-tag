using UnityEngine;

public class VerticalMoveHandler : MonoBehaviour, IPossessionAction, IMovable
{
    private bool isActive = false;
    [SerializeField]
    private float maxY = 10f;

    [SerializeField]
    private float minY = -10f;

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
        float moveY = currentMovementInput.y * speed * Time.deltaTime;

        // Get the current position and apply the intended movement
        Vector3 newPosition = transform.position + new Vector3(0, moveY, 0);

        // Constrain the new Y position
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Apply the constrained position to the object's transform
        transform.position = newPosition;
    }

}