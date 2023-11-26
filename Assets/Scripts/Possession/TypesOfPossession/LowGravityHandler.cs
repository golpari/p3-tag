using UnityEngine;

public class LowGravityHandler : PossessionActionBase
{
    // Gravity scale presets for different gravity states.
    [SerializeField] private float defaultGravityScale = 2.0f;
    [SerializeField] private float lowGravityScale = 0.0005f;
    [SerializeField] private GameObject gravityFX;
    // To have a base spiritprice but still be able to modify individually
    //public override float spiritPrice
    //{
    //    get { return _spiritPrice; }
    //    set { _spiritPrice = value; }
    //}
    private void Awake()
    {
        spiritPrice = 75f;
    }

    private bool isLowGravity = false;
    private bool isActive = false;

    public override bool EnableAction()
    {
        // Don't enable if don't have the price to pay for possession
        // Same for all of them but at least this way we can change the price
        // individually if we want to 
        if (spirit_slider.current_value <= 0.0f) return false;

        isActive = true;
        // Always low grav when first posses
        EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(lowGravityScale, gravityFX));
        EventBus.Publish<SpiritPossesion>(new SpiritPossesion(true, -3.0f));
        isLowGravity = true;
        return true;
    }

    public override void DisableAction()
    {
        isActive = false;
        // change back to normal grav when unpossess
        EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(defaultGravityScale, gravityFX));
        if (spirit_slider.current_value > 0.0f)
        {
            EventBus.Publish<SpiritPossesion>(new SpiritPossesion(false));
        }
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