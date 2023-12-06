using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLighting : MonoBehaviour
{
    private Subscription<ChangeLightingEvent> changeLightingSubscription;
    private Light lightComponent; 
    private Material material;
    private bool isDark = false;

    private void Start()
    {
        EventBus.Subscribe<ChangeLightingEvent>(_OnLightingChange);
        lightComponent = GetComponent<Light>();
        MeshRenderer temp = GetComponent<MeshRenderer>();
        if (temp != null) {
            material = GetComponent<MeshRenderer>().material;
        }
        
    }
    private void _OnLightingChange(ChangeLightingEvent e)
    {
        if (e.isDark && lightComponent != null)
        {
            if (lightComponent.CompareTag("Flashlight"))
            {
                lightComponent.intensity = 7.5f;
            }
            else
            {
                lightComponent.intensity = 0.0f;
            }
        }
        else if (lightComponent != null)
        {
            // If not dark, dim the torches.
            if (lightComponent.type == LightType.Point && !lightComponent.CompareTag("Flashlight"))
            {
                lightComponent.intensity = 1.0f;
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
        else if (material != null && !isDark && !CompareTag("Spirit"))
        {
            Color color = material.GetColor("_Color");
            color *= 0.1f;
            material.SetColor("_Color", color);
            isDark = true;
        }
        else if (material != null && isDark && !CompareTag("Spirit"))
        {
            Color color = material.GetColor("_Color");
            color *= 10.0f;
            material.SetColor("_Color", color);
            isDark = false;
        }

        if (CompareTag("Spirit") && !isDark)
        {
            Color color = material.GetColor("_EmissionColor");
            color *= 0.001f;
            material.SetColor("_EmissionColor", color);
            isDark = true;
        }
        else if (CompareTag("Spirit") && isDark)
        {
            Color color = material.GetColor("_EmissionColor");
            color *= 1000.0f;
            material.SetColor("_Color", color);
            isDark = false;
        }
    }
}
