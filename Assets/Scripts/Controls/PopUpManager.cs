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
    [SerializeField] float lagTime = 0.5f;
    GameObject currentPopUp;
    private bool hasPossessed = false;


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
        spriteRenderer.color = Color.black;
        
        // correct the sprite to be the one in the event
        if (e.currIcon == "button_a")
            spriteRenderer.sprite = controls[0];
        
        else if (e.currIcon == "joystick2_left")
        {
            spriteRenderer.sprite = controls[1];
            /*if (hasPossessed)
                StartCoroutine(PossessionPopUpFloatUp());*/
        }   
        
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
        
        else if (e.currIcon == "button_b")
        {
            spriteRenderer.sprite = controls[7];
            /*if (hasPossessed)
                StartCoroutine(PossessionPopUpFloatDown());*/
        }

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
            hasPossessed = true;
        }

        else
        {
            EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
            // no longer possessing
            hasPossessed = false;
        }
        
    }

    void _OnPossession(PossessionEvent e)
    {
        // when a player presses the button
        /*if (hasPossessed)
            StartCoroutine(PossessionPopUpInitial());*/
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
    }

  /*  IEnumerator PossessionPopUpInitial()
    {
        // show Y for 0.5 after usage
        yield return new WaitForSeconds(lagTime);
        // show nothing
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        yield return new WaitForSeconds(lagTime);
        if (hasPossessed)
            EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "ghost")); // show leftstick
    }

    IEnumerator PossessionPopUpFloatUp()
    {
        yield return new WaitForSeconds(lagTime);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        yield return new WaitForSeconds(lagTime);
        if (hasPossessed)
            EventBus.Publish<PopUpEvent>(new PopUpEvent("button_b", "ghost")); // show b button
    }

    IEnumerator PossessionPopUpFloatDown()
    {
        yield return new WaitForSeconds(lagTime);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        yield return new WaitForSeconds(lagTime);
        if (hasPossessed)
            EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "ghost")); // show b button
    }*/


    private void OnDestroy()
    {
        EventBus.Unsubscribe(outlineSubscription);
        EventBus.Unsubscribe(possessionSubscription);
    }
}
