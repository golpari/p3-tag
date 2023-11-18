//using UnityEngine;

//public class HorizontalMoveHandler : PossessionController
//{
//    public void Run(GameObject currObject)
//    {
//        HorizontalMovePossessed isHorizontalMove = currObject.GetComponent<HorizontalMovePossessed>();
//        //if the nearest object is of type HorizontalMove...
//        if (currObject.GetComponent<HorizontalMovePossessed>() != null)
//        {
//            // Read the input from the user for X-axis movement
//            float moveX = currentMovementInput.x;

//            // Calculate the movement in X direction
//            Vector3 movement = new Vector3(moveX * movementSpeed * Time.deltaTime, 0, 0);

//            // Apply the movement to the object's transform
//            currObject.transform.Translate(movement, Space.World);
//        }
//    }
//}
