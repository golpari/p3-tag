using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class camera_move : MonoBehaviour
{
    public Vector3[] camera_pos;
    public Vector3[] starting_pos;
    public Vector3[] ghost_pos;
    public float [] orth_sizes;

    public GameObject player;
    public GameObject ghost;


    public GameObject[] Enviorments;
    public GameObject[] Grids;
    public GameObject Script;


    int current_floor = 0;
    // Start is called before the first frame update

    float original_speed;
    void Start()
    {
        EventBus.Subscribe<int>(_change_index);
        EventBus.Publish<int>(0);

        EventBus.Subscribe<ChangeDoorsEvent>(transition_camera);
        EventBus.Subscribe<respawn>(_respawn);
        EventBus.Subscribe<ThiefDiedEvent>(_thiefDied_flashRed);

        EventBus.Subscribe<Reset>(_reset);

    }


    void _reset(Reset e) {
        Enviorments[0].SetActive(true);
        Grids[0].SetActive(true);
        Enviorments[1].SetActive(false);
        Grids[1].SetActive(false);
        Enviorments[2].SetActive(false);
        Grids[2].SetActive(false);
        this.GetComponent<Camera>().orthographicSize = orth_sizes[0];
        this.GetComponent<Camera>().transform.position = camera_pos[0];
        current_floor = 0;
    }

    void _respawn(respawn e) {
        StartCoroutine(respawn());
    }


    public IEnumerator respawn() {
        yield return new WaitForSeconds(0.5f); // add a buffer of half a second for the player to realize they got hurt
        player.transform.position = starting_pos[current_floor];
        yield return null;
    }

    void _thiefDied_flashRed(ThiefDiedEvent e)
    {
        StartCoroutine(changeColor(UnityEngine.Color.red, 1.5f));
    }

    void _change_index(int ind)
    {
        this.transform.position = camera_pos[ind];
        player.transform.position = starting_pos[ind] + new Vector3(0.0f, 0.0f, 5.0f);
        ghost.transform.position = ghost_pos[ind] - new Vector3(0.0f, 0.0f, 5.0f);

    }

    void transition_camera(ChangeDoorsEvent e)
    {

        StartCoroutine(camera_change(e.doorRoomNum));


    }

    void changeRoom(int NextDoorNum) {
        Enviorments[NextDoorNum - 1].SetActive(false);
        Grids[NextDoorNum - 1].SetActive(false);
        Enviorments[NextDoorNum].SetActive(true);
        Grids[NextDoorNum].SetActive(true);
        this.GetComponent<Camera>().orthographicSize = orth_sizes[NextDoorNum];

    }
    public IEnumerator camera_change(int door_num)
    {
        current_floor = door_num;
        player.transform.position = starting_pos[door_num];




        player.SetActive(false);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        EventBus.Publish<PauseCountDownTimer>(new PauseCountDownTimer());


        // ghost aimation - fucking howwwwwwwwwww to fucking remove the skeleton shit

        StartCoroutine(FadeTo(ghost.GetComponentInChildren<SkinnedMeshRenderer>().material, 0.0f, 1.5f));
        Script.GetComponent<spirit_spawn>().turn_on(0.0f);
        yield return changeColor(new UnityEngine.Color(119 / 255, 248 / 255, 255 / 255), 1.5f);

        EventBus.Publish<fadeOut>(new fadeOut(true));
        yield return new WaitForSeconds(1.0f);

        yield return ghost_transition(door_num);

        EventBus.Publish<fadeOut>(new fadeOut(false));
        yield return new WaitForSeconds(1.0f);


        StartCoroutine(FadeTo(ghost.GetComponentInChildren<SkinnedMeshRenderer>().material, 1.0f, 1.5f));
        Script.GetComponent<spirit_spawn>().turn_on(1.0f);
        yield return changeColor(new UnityEngine.Color(119 / 255, 248 / 255, 255 / 255), 1.5f);

        
        player.SetActive(true);
        PlayerController.player_lock = true;
        // Zade put your shader here!!!


        EventBus.Publish<ghost_set>(new ghost_set(15.0f));

    }


    public IEnumerator ghost_transition(int door_num) {

        // ask barb and the group about the velocity and why it won't transition up
        changeRoom(door_num);
        this.transform.position = camera_pos[door_num];
        ghost.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ghost.transform.position = ghost_pos[door_num];
        yield return null;
    }

    public IEnumerator changeColor(UnityEngine.Color color, float duration)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime/duration;
            this.GetComponent<Camera>().backgroundColor = UnityEngine.Color.Lerp(color, UnityEngine.Color.black, t);
            yield return null; ;
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
        UnityEngine.Color color = material.color;
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
