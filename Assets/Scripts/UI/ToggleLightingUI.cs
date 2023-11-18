using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 Shows the command to toggle the lighting when the lighting first changes
 (on first torch possession)
 Rest of the time, show nothing
 */
public class ToggleLightingUI : MonoBehaviour
{
    Subscription<ChangeLightingEvent> lightingSubscription;
    private bool isFirst = true;

    void Start()
    {
        lightingSubscription = EventBus.Subscribe<ChangeLightingEvent>(_OnLightingChange);
    }

    void _OnLightingChange(ChangeLightingEvent e)
    {
        // on first lighting possession, show command to toggle
        if (isFirst)
        {
            GetComponent<Text>().text = "A";
            isFirst = false;
        }
        // Reset to blank
        else
            GetComponent<Text>().text = "";
    }

    private void OnDestroy()
    { 
        EventBus.Unsubscribe(lightingSubscription);
    }
}
