using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorial : MonoBehaviour
{
    public GameObject player;
    public GameObject ghost;
    // Start is called before the first frame update

    public GameObject target_object; // The object to zoom in on
    public GameObject second_target;
    public Material glow;
    public static bool tut = true;
    Subscription<ButtonPressEvent> butttonSubscription;
    private bool buttonBPressed = false;
    private bool buttonAPressed = false;
    private bool buttonYPressed = false;
    private bool joystickPressed = false;

    void Start()
    {
        // UNCOMMENT FOR FINAL BUILD
        tutorial_scene();
        butttonSubscription = EventBus.Subscribe<ButtonPressEvent>(_OnButtonPress);
        EventBus.Subscribe<Reset>(_reset);
    }

    void _reset(Reset e) {
        tutorial_scene();
        tut = true;
    }

    void tutorial_scene() {
        PlayerController.player_lock = true;
        GhostController.ghost_lock = false;
        StartCoroutine(test());
    }
    // Update is called once per frame

    void _OnButtonPress(ButtonPressEvent e)
    {
        if (e.currButton == "button_b")
        {
            buttonBPressed = true;
        }
        else if (e.currButton == "button_a")
        {
            buttonAPressed = true;
        }
        else if (e.currButton == "button_y")
        {
            buttonYPressed = true;
        }
        else if (e.currButton == "joystick2_left")
        {
            joystickPressed = true;
        }
        /*else if (e.currButton == null)
        {
            buttonBPressed = false;
            buttonAPressed = false;
            buttonYPressed = false;
            joystickPressed = false;
        }   */
    }

    IEnumerator test()
    {
        // ghost move
        EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "ghost"));
        yield return new WaitUntil(() => joystickPressed);
        joystickPressed = false;
        yield return new WaitForSeconds(1.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        yield return new WaitForSeconds(1.0f);

        // ghost float up
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_b", "ghost"));
        yield return new WaitUntil(() => buttonBPressed);
        buttonBPressed = false;                                                                                         
        yield return new WaitForSeconds(1.0f);                                                                  
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        yield return new WaitForSeconds(1.0f);

        // ghost float down
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "ghost"));  
        yield return new WaitUntil(() => buttonAPressed);
        buttonAPressed = false;
        yield return new WaitForSeconds(1.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        yield return new WaitForSeconds(1.0f);

        Material mat = target_object.GetComponent<MeshRenderer>().material;
        for (int i = 0; i < 20; i++) {
            target_object.GetComponent<MeshRenderer>().material = glow;
            yield return new WaitForSeconds(0.15f);
            target_object.GetComponent<MeshRenderer>().material = mat;
            yield return new WaitForSeconds(0.15f);
        }

        // ghost possess
        //EventBus.Publish<PopUpEvent>(new PopUpEvent("button_y", "ghost"));
        yield return new WaitUntil(() => buttonYPressed);
        buttonYPressed = false;
        //yield return new WaitForSeconds(1.0f);
        //EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        //yield return new WaitForSeconds(1.0f);

        mat = second_target.GetComponent<MeshRenderer>().material;
        for (int i = 0; i < 10; i++)
        {
            second_target.GetComponent<MeshRenderer>().material = glow;
            yield return new WaitForSeconds(0.15f);
            second_target.GetComponent<MeshRenderer>().material = mat;
            yield return new WaitForSeconds(0.15f);
        }

        EventBus.Publish<SpiritEvent>(new SpiritEvent(100.0f));

        //EventBus.Publish<PopUpEvent>(new PopUpEvent("button_y", "ghost"));
        yield return new WaitUntil(() => buttonYPressed);
        buttonYPressed = false;
       // yield return new WaitForSeconds(1.0f);
        //EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));
        yield return new WaitForSeconds(0.5f);

        joystickPressed = false;
        EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "ghost"));
        yield return new WaitUntil(() => joystickPressed);
        joystickPressed = false;
        yield return new WaitForSeconds(0.5f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        buttonBPressed = false;
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_b", "ghost"));
        yield return new WaitUntil(() => buttonBPressed);
        buttonBPressed = false;
        yield return new WaitForSeconds(0.5f);
        buttonAPressed = false;
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "ghost"));
        yield return new WaitUntil(() => buttonAPressed);
        buttonAPressed = false;
        yield return new WaitForSeconds(0.5f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        /*EventBus.Publish<PopUpEvent>(new PopUpEvent("button_y", "ghost"));
        yield return new WaitUntil(() => buttonYPressed);
        buttonYPressed = false;
        yield return new WaitForSeconds(1.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));*/

        EventBus.Publish<SpiritEvent>(new SpiritEvent(100.0f));

        PlayerController.player_lock = false;

        EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "thief"));
        joystickPressed = false;
        yield return new WaitUntil(() => joystickPressed);
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "thief"));

        buttonAPressed = false;
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "thief"));
        buttonAPressed = false;
        yield return new WaitUntil(() => buttonAPressed);
        yield return new WaitForSeconds(1.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "thief"));


       


        PlayerController.player_lock = false;
        GhostController.ghost_lock = false;
        tutorial.tut = false;
        Debug.Log("move");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Main Menu");
        }
    }
}


/*
 if (Input.GetButtonDown("Fire1"))
{
    Debug.Log("Fire1 button was pressed");
}

 */

