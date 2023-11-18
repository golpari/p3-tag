using UnityEngine;

public class LowGravityHandler : MonoBehaviour, IPossessionAction
{
    // Gravity scale presets for different gravity states.
    [SerializeField] private float defaultGravityScale = 2.0f;
    [SerializeField] private float lowGravityScale = 0.0005f;
    [SerializeField] private GameObject gravityFX;

    private bool isLowGravity = false;
    private bool isActive = false;

    public void EnableAction()
    {
        isActive = true;
        // Always low grav when first posses
        EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(lowGravityScale, gravityFX));
        isLowGravity = true;
    }

    public void DisableAction()
    {
        isActive = false;
        // change back to normal grav when unpossess
        EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(defaultGravityScale, gravityFX));
        isLowGravity = false;
    }

    public void ToggleGravity()
    {
        if (!isActive) return;
        //Default to low gravity.
        if (!isLowGravity)
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(lowGravityScale, gravityFX));
            isLowGravity = true;
        }
        // Low gravity to default gravity.
        else
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(defaultGravityScale, gravityFX));
            isLowGravity = false;
        }
    }
}