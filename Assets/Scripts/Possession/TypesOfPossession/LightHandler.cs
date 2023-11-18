using UnityEngine;

public class LightHandler : MonoBehaviour, IPossessionAction
{
    private bool isDark = false;
    private bool isActive = false;

    public void EnableAction()
    {
        isActive = true;
        // Always darken when first posses
        isDark = true;
        EventBus.Publish<ChangeLightingEvent>(new ChangeLightingEvent(isDark));
    }

    public void DisableAction()
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

