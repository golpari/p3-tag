using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 Shows the command to toggle the grav when the grav first changes
 Rest of the time, show nothing
 */
public class ToggleGravUI : MonoBehaviour
{
    Subscription<ChangeGravityEvent> gravSubscription;
    private bool isFirst = true;

    void Start()
    {
        gravSubscription = EventBus.Subscribe<ChangeGravityEvent>(_OnGravChange);
    }

    void _OnGravChange(ChangeGravityEvent e)
    {
        // on first grav possession, show command to toggle
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
        EventBus.Unsubscribe(gravSubscription);
    }
}
