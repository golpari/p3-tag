using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    // Object position on game start.
    private Vector3 startPosition;

    // Amount to move the object in each direction.
    [SerializeField] private float xMovement;
    [SerializeField] private float yMovement;
    [SerializeField] private float zMovement;

    // Speed at which the object is moved;
    [SerializeField] private float speed;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        float xPosition = xMovement != 0 ? Mathf.PingPong(Time.time * speed, xMovement) + startPosition.x : startPosition.x;
        float yPosition = yMovement != 0 ? Mathf.PingPong(Time.time * speed, yMovement) + startPosition.y : startPosition.y;
        float zPosition = zMovement != 0 ? Mathf.PingPong(Time.time * speed, zMovement) + startPosition.z : startPosition.z;

        transform.position = new Vector3(xPosition, yPosition, zPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
