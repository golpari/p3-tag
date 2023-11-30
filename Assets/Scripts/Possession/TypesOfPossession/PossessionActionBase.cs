using UnityEngine;

// Possessable action is movement related
public interface IMovable
{
    void Move(Vector2 currentMovementInput, float speed, Vector2 currentFloatInput);
    // can do checks e.g. if (currentPossessionAction is IMovable movable)
    //{
    //    movable.Move();
    //}
}

public abstract class PossessionActionBase : MonoBehaviour
{
    protected Outline outline;
    [SerializeField] protected float spiritPrice = 10f; // Default value for spirit price just in case

    protected virtual void Start()
    {
        outline = GetComponent<Outline>();
        // default selection color to blue
        // keep to add juice in future
        //outline.OutlineColor = Color.blue;
    }

    public abstract bool EnableAction();
    public abstract void DisableAction();


    // Check if the ghost can afford to possess
    public bool AffordCheck()
    {
        // A bit buggy so need this extra conditional for if the item's free
        if (spiritPrice == 0) return true;
        if (spirit_slider.current_value < spiritPrice) return false;
        return true;
    }
}

