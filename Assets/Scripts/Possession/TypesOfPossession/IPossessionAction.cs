using UnityEngine;

public interface IPossessionAction
{
    // Enables the action, allowing it to start responding to input or other triggers
    void EnableAction();

    // Disables the action, stopping it from responding to input or other triggers
    void DisableAction();
}

// Possessable action is movement related
public interface IMovable
{
    void Move(Vector2 currentMovementInput, float speed);
    // can do checks e.g. if (currentPossessionAction is IMovable movable)
    //{
    //    movable.Move();
    //}
}
