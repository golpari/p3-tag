using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Converts the string icon and player into the actual objects to display the icon */
public class PopUpManager : MonoBehaviour
{
    [SerializeField] List<Sprite> controls;

    // Both are children of their respective players
    [SerializeField] GameObject ghostPopUp;
    [SerializeField] GameObject thiefPopUp;
    GameObject currentPopUp;

    Subscription<PopUpEvent> popUpSubscription;
    Subscription<OutlineEvent> outlineSubscription;
    Subscription<PossessionEvent> possessionSubscription;
    private void Start()
    {
        popUpSubscription = EventBus.Subscribe<PopUpEvent>(_OnPopUpChange);
        outlineSubscription = EventBus.Subscribe<OutlineEvent>(_OnOutlineChange);
        possessionSubscription = EventBus.Subscribe<PossessionEvent>(_OnPossession);
    }

    void _OnPopUpChange(PopUpEvent e)
    {
        // Set who the parent it
        if (e.currPlayer == "ghost")
            currentPopUp = ghostPopUp;
        else if (e.currPlayer == "thief")
            currentPopUp = thiefPopUp;
        else
            Debug.Log("Error: currPlayer isn't named correctly (thief, ghost)");

        SpriteRenderer spriteRenderer = currentPopUp.GetComponent<SpriteRenderer>();

        // correct the sprite to be the one in the event
        if (e.currIcon == "button_a")
            spriteRenderer.sprite = controls[0];
        else if (e.currIcon == "joystick2_left")
            spriteRenderer.sprite = controls[1];
        else if (e.currIcon == "joystick2_right")
            spriteRenderer.sprite = controls[2];
        else if (e.currIcon == "controller_xbox")
            spriteRenderer.sprite = controls[3];
        else if (e.currIcon == "lt")
            spriteRenderer.sprite = controls[4];
        else if (e.currIcon == "rt")
            spriteRenderer.sprite = controls[5];
        else if (e.currIcon == "button_y")
            spriteRenderer.sprite = controls[6];
        else if (e.currIcon == null)
            spriteRenderer.sprite = null;
        else
            Debug.Log("Error: currIcon isn't named correctly (in controls in GameManager or null)");
    }

    void _OnOutlineChange(OutlineEvent e)
    {
        if (e.shouldOutline)
        {
            EventBus.Publish<PopUpEvent>(new PopUpEvent("button_y", "ghost"));
        }

        else
            EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
    }

    void _OnPossession(PossessionEvent e)
    {
        // if a player presses the button
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(outlineSubscription);
        EventBus.Unsubscribe(possessionSubscription);
    }
}
