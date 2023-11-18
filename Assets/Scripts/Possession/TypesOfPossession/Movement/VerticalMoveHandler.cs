using UnityEngine;

public class VerticalMoveHandler: PossessionController
{
    public void Run(GameObject currObject)
    { 
        VerticalMovePossessed isVertMove = currObject.GetComponent<VerticalMovePossessed>();
        //if the nearest object is of type VertMove...
        if (currObject.GetComponent<VerticalMovePossessed>() != null)
        {
            // Read the input from the user for Y-axis movement
            float moveY = currentMovementInput.y;

            // Calculate the movement in Y direction
            Vector3 movement = new Vector3(0, moveY * movementSpeed * Time.deltaTime, 0);

            // Apply the movement to the object's transform
            currObject.transform.Translate(movement, Space.World);
        }
    }
}
