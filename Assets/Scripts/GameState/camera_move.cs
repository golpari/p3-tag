using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.GraphicsBuffer;

public class camera_move : MonoBehaviour
{
    public Vector3[] camera_pos;
    public Vector3[] starting_pos;
    public Vector3[] ghost_pos;

    public GameObject player;
    public GameObject ghost;




    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<int>(_change_index);
        EventBus.Publish<int>(0);

        EventBus.Subscribe<ChangeDoorsEvent>(transition_camera);

    }


    void _change_index(int ind)
    {
        this.transform.position = camera_pos[ind];
        player.transform.position = starting_pos[ind];
        ghost.transform.position = ghost_pos[ind];


    }

    void transition_camera(ChangeDoorsEvent e)
    {

        StartCoroutine(camera_change(e.doorRoomNum));


    }


    public IEnumerator camera_change(int door_num)
    {
        PlayerController.player_lock = true;

        EventBus.Publish<PauseCountDownTimer>(new PauseCountDownTimer());
        StartCoroutine(changeColor(Color.blue));
        yield return StartCoroutine(MoveObjectOverTime(player.transform, player.transform.position, starting_pos[door_num], 1.0f));


        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        //yield return StartCoroutine(FadeTo(ghost.gameObject.GetComponent<Renderer>().material, 0.0f, 1.5f)); for now

        ghost.transform.position = ghost_pos[door_num];


        GameObject loc = GameObject.Find("L0"+(door_num - 1).ToString()+"-Grid");
        loc.SetActive(false);
        loc = GameObject.Find("L0" + (door_num - 1).ToString() + "-Environment");
        loc.SetActive(false);
        loc = GameObject.Find("L0" + (door_num - 1).ToString() + "-SpiritContainer");
        loc.SetActive(false);

        yield return StartCoroutine(MoveObjectOverTime(this.transform, this.transform.position, camera_pos[door_num], 1.5f));

        //yield return StartCoroutine(FadeTo(ghost.gameObject.GetComponent<Renderer>().material, 1.0f, 1.5f));

        EventBus.Publish<StartCountDownTimer>(new StartCountDownTimer());

        StartCoroutine(changeColor(Color.red));
        PlayerController.player_lock = false;
    }


    public IEnumerator changeColor(Color color)
    {
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime;
            this.GetComponent<Camera>().backgroundColor = Color.Lerp(color, Color.black, t);
            yield return null;
        }

    }

    public static IEnumerator MoveObjectOverTime(Transform target, Vector3 initial_pos, Vector3 dest_pos, float duration_sec)
    {

        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration_sec;

        while (progress < 1.0f)
        {
            // Recalculate the progress variable every frame. Use it to determine
            // new position on line from "initial_pos" to "dest_pos"
            progress = (Time.time - initial_time) / duration_sec;
            Vector3 new_position = Vector3.Lerp(initial_pos, dest_pos, progress);
            target.position = new_position;

            // yield until the end of the frame, allowing other code / coroutines to run
            // and allowing time to pass.
            yield return null;
        }

        target.position = dest_pos;

    }

    IEnumerator FadeTo(Material material, float targetOpacity, float duration)
    {
        // Cache the current color of the material, and its initiql opacity.
        Color color = material.color;
        float startOpacity = color.a;

        // Track how many seconds we've been fading.
        float t = 0;

        while (t < duration)
        {

            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / duration);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            material.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }
    }


}
