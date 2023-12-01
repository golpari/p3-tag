using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessUI : MonoBehaviour
{
    Subscription<OutlineEvent> outlineSubscription;
    Subscription<PossessionEvent> possessionSubscription;

    void Start()
    {
        outlineSubscription = EventBus.Subscribe<OutlineEvent>(_OnOutline);
        possessionSubscription = EventBus.Subscribe<PossessionEvent>(_OnPossession);
    }

    void _OnOutline(OutlineEvent e)
    {
        // if player towards or away from an object

        if (e.shouldOutline)
        {
            GetComponent<Text>().text = "Y";
        }
            
        else
            GetComponent<Text>().text = "";
    }

    void _OnPossession(PossessionEvent e)
    {
        // if a player presses the button
        GetComponent<Text>().text = "";
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(outlineSubscription);
        EventBus.Unsubscribe(possessionSubscription);
    }
}
