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
            if (lightComponent.CompareTag("Flashlight"))
            {
                lightComponent.intensity = 10.0f;
            }
            else
            {
                lightComponent.intensity = 0.0f;
            }
        }
        else
        {
            // If not dark, dim the torches.
            if (lightComponent.type == LightType.Point && !lightComponent.CompareTag("Flashlight"))
            {
                lightComponent.intensity = 3.0f;
            }
            // if not a torch, dim the main light.
            else if (!lightComponent.CompareTag("Flashlight"))
            {
                lightComponent.intensity = 1.0f;
            }

            if (lightComponent.CompareTag("Flashlight"))
            {
                lightComponent.intensity = 0.0f;
            }
        }
    }
}
