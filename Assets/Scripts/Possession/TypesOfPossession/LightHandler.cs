using UnityEngine;

public class LightHandler : PossessionActionBase
{
    private bool isDark = false;
    private bool isActive = false;

    private void Awake()
    {
        spiritPrice = 1f;
    }

    public override bool EnableAction()
    {
        // Don't enable if don't have the price to pay for possession
        if (spirit_slider.current_value <= 0) return false;

        isActive = true;
        // Always darken when first posses 
        isDark = true;
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
        EventBus.Publish<SpiritPossesion>(new SpiritPossesion(true, -4.0f)); // -2
        return true;
    }

    public override void DisableAction()
    {
        isActive = false;
        // To change back to light when unposses
        isDark = false; 
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));

        if (spirit_slider.current_value  > 0.0f) {
            EventBus.Publish<SpiritPossesion>(new SpiritPossesion(false));
        }
        // have all events be children of spiritPossesion
    }
}

