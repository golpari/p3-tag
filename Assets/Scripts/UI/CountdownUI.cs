using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;
using System.Runtime.CompilerServices;

public class CountdownUI : MonoBehaviour
{

    public float totalTime;
    public float ease_factor;
    public GameObject camera_;

    private float timeLeft;
    private Text timeComp;
    private bool runGame = true; // originally true

    private bool shake = false;
    private int scale_factor = 1;
    public bool TimerBegin = false;

    public float wait_time = 5.0f;
    float MaxFont = 260;
    int font_increase = 0;

    Subscription<StartGameEvent> startEventSubscription;
    Subscription<EndGameEvent> endEventSubscription;
    Subscription<StartCountDownTimer> startCountdownSubscription;
    private void Start()
    {
        timeComp = GetComponent<Text>();
        timeLeft = totalTime;
        startEventSubscription = EventBus.Subscribe<StartGameEvent>(_OnGameStart);
        endEventSubscription = EventBus.Subscribe<EndGameEvent>(_OnGameEnd);
        startCountdownSubscription = EventBus.Subscribe<StartCountDownTimer>(_startTimer);
        EventBus.Subscribe<PauseCountDownTimer>(_pauseTimer);

        font_increase = (int)(MaxFont / totalTime);
    }

    void _startTimer(StartCountDownTimer e)
    {
        Vector3 position = this.transform.position;
        int temp = timeComp.fontSize;
        timeComp.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f);
        timeComp.fontSize = 260;
        TimerBegin = true;
        StartCoroutine(text_start(position,temp));

    }




    public IEnumerator text_start(Vector3 position, int temp_size) {
        Color temp = timeComp.color;
        timeComp.color = Color.red;
        timeComp.color = temp;
        PlayerController.player_lock = true;
        yield return StartCoroutine(camera_move.MoveObjectOverTime(timeComp.transform, timeComp.transform.position, position, 1.5f));
        PlayerController.player_lock = false;
        timeComp.fontSize = temp_size;
    }

   

    void _pauseTimer(PauseCountDownTimer e)
    {
        TimerBegin = false;
    }

    // ignore for now
    void _OnGameStart(StartGameEvent e)
    {
        runGame = true;
    }

    void _OnGameEnd(EndGameEvent e)
    {
        runGame = false;
    }

    // change from 255 255 255 to 255 0 0
    void Update()
    {

        // If the game has started
        if (runGame)
        {

            if (TimerBegin)
            {

                timeLeft -= Time.deltaTime;
                timeComp.text = (timeLeft).ToString("0"); //default
                timeComp.alignment = TextAnchor.MiddleCenter;
                timer_juicing();

                // If the time has run out
                if (timeLeft < 0)
                {
                    runGame = false;
                    EventBus.Publish<EndCountdownEvent>(new EndCountdownEvent());

                }
            }

        }
    }



    void timer_juicing()
    {
        if (timeComp.color.b > 0)
        {
            timeComp.color = new Color(timeComp.color.r, timeComp.color.g, (timeLeft / totalTime));
        }

        if (timeComp.color.g > 0)
        {
            timeComp.color = new Color(timeComp.color.r, (timeLeft / totalTime), timeComp.color.b);
        }

        // max fint size is 160 // totalTime


        if (!shake)
        {
            timeComp.fontSize += 10;
            StartCoroutine(ShakeEffect(1 - (timeLeft / totalTime), (timeLeft / totalTime), wait_time * (timeLeft / totalTime)));
        }
    }


    public IEnumerator ShakeEffect(float duration, float radius, float wait_time)
    {
        Vector3 temp = camera_.transform.position;
        shake = true;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            camera_.transform.position = temp + UnityEngine.Random.onUnitSphere * radius;
            yield return null;
        }
        camera_.transform.position = temp;

        yield return new WaitForSeconds(wait_time);

        shake = false;
    }




}