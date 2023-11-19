using UnityEngine;

public class LightHandler : PossessionActionBase
{
    private bool isDark = false;
    private bool isActive = false;

    private void Awake()
    {
        spiritPrice = 25f;
    }

    public override bool EnableAction()
    {
        // Don't enable if don't have the price to pay for possession
        if (spirit_slider.current_value < spiritPrice) return false;

        isActive = true;
        // Always darken when first posses 
        isDark = true;
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
        // Charge price
        EventBus.Publish<SpiritEvent>(new SpiritEvent(-spiritPrice));
        return true;
    }

    public override void DisableAction()
    {
        isActive = false;
        // To change back to light when unposses
        isDark = false; 
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
    }

    public void ToggleLighting()
    {
        if (!isActive) return;
        isDark = !isDark;
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
    }
}

