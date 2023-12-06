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

    void Start()
    {
        tutorial_scene();
    }

    void tutorial_scene() {
        PlayerController.player_lock = true;
        GhostController.ghost_lock = false;
        StartCoroutine(test());
    }
    // Update is called once per frame


    IEnumerator test()
    {
        EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "ghost"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "ghost"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_b", "ghost"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        Material mat = target_object.GetComponent<MeshRenderer>().material;
        for (int i = 0; i < 20; i++) {
            target_object.GetComponent<MeshRenderer>().material = glow;
            yield return new WaitForSeconds(0.15f);
            target_object.GetComponent<MeshRenderer>().material = mat;
            yield return new WaitForSeconds(0.15f);
        }

        yield return new WaitForSeconds(1.5f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_y", "ghost"));
        yield return new WaitForSeconds(1.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        mat = second_target.GetComponent<MeshRenderer>().material;
        for (int i = 0; i < 20; i++)
        {
            second_target.GetComponent<MeshRenderer>().material = glow;
            yield return new WaitForSeconds(0.15f);
            second_target.GetComponent<MeshRenderer>().material = mat;
            yield return new WaitForSeconds(0.15f);
        }

        EventBus.Publish<SpiritEvent>(new SpiritEvent(100.0f));

        EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "ghost"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_b", "ghost"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "ghost"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_y", "ghost"));
        yield return new WaitForSeconds(1.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        PlayerController.player_lock = false;

        EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "thief"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent(null, "ghost"));

        EventBus.Publish<PopUpEvent>(new PopUpEvent("joystick2_left", "thief"));
        yield return new WaitForSeconds(5.0f);
        EventBus.Publish<PopUpEvent>(new PopUpEvent("button_a", "thief"));
        yield return new WaitForSeconds(5.0f);
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
 public PopUpEvent(string _currIcon, string _currPlayer)
    {
        currIcon = _currIcon;
        currPlayer = _currPlayer;
    }
 */

