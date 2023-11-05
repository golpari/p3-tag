using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLighting : MonoBehaviour
{
    private Subscription<ChangeLightingEvent> changeLightingSubscription;
    private Light lightComponent; 
    private void Start()
    {
        EventBus.Subscribe<ChangeLightingEvent>(_OnLightingChange);
        lightComponent = GetComponent<Light>();
    }
    private void _OnLightingChange(ChangeLightingEvent e)
    {
        if (e.isDark)
        {
            // If dark, brighten the torches.
            if (lightComponent.type == LightType.Point)
            {
                lightComponent.intensity = 3.0f;
            }
            // if not a torch, brighten the main light.
            else
            {
                lightComponent.intensity = 1.0f;
            }

            e.isDark = false;
        }
        else
        {
            // If not dark, dim the torches.
            if (lightComponent.type == LightType.Point)
            {
                lightComponent.intensity = 1.0f;
            }
            // if not a torch, dim the main light.
            else
            {
                lightComponent.intensity = 0.1f;
            }
            e.isDark = true;
        }
    }
}
