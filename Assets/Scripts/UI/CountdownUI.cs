using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;
using static UnityEngine.Rendering.DebugUI;

public class CountdownUI : MonoBehaviour
{
    
    public float totalTime;
    public float ease_factor;
    public GameObject camera_;

    private float timeLeft;
    private Text timeComp;
    private bool keepCounting = true;
    private bool runGame = true; // originally true
    
    private bool shake = false;
    private int scale_factor = 1;

    Subscription<StartGameEvent> startEventSubscription;
    Subscription<EndGameEvent> endEventSubscription;
    private void Start()
    {
        timeComp = GetComponent<Text>();
        timeLeft = totalTime;
        startEventSubscription = EventBus.Subscribe<StartGameEvent>(_OnGameStart);
        endEventSubscription = EventBus.Subscribe<EndGameEvent>(_OnGameEnd);
        StartCoroutine(count_down());


    }

    // ignore for now
    void _OnGameStart(StartGameEvent e)
    {
        runGame = true;
        StartCoroutine(count_down());
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
            
            // Countdown the time
            timeLeft -= Time.deltaTime;
            if (keepCounting) {
                timeComp.text = (timeLeft).ToString("0"); //default
                timeComp.alignment = TextAnchor.MiddleCenter;
            }
                
                
            // If the time has run out
            if (timeLeft < 0)
            {
                // Ghost wins, player loses
                EventBus.Publish<EndCountdownEvent>(new EndCountdownEvent());
                runGame = false;
            }
        }
    }



    public IEnumerator ShakeEffect(float duration, float radius)
    {
        shake = true;
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            camera_.transform.localPosition = UnityEngine.Random.onUnitSphere * radius;
            yield return null;
        }
        camera_.transform.localPosition = Vector3.zero;
        shake = false;
    }

    IEnumerator count_down() {
        while (timeLeft >= 0) {
            if (timeComp.color.b > 0)
            {
                timeComp.color = new Color(timeComp.color.r, timeComp.color.g, (timeLeft / totalTime));
            }

            if (timeComp.color.g > 0)
            {
                timeComp.color = new Color(timeComp.color.r, (timeLeft / totalTime), timeComp.color.b);
            }



            if (timeLeft <= Mathf.Floor(totalTime / 2)) {
                timeComp.fontSize += Mathf.Clamp(scale_factor, 0, 3); 
            }

            if (timeLeft <= Mathf.Floor(totalTime / 4) && !shake)
            {
                scale_factor += 1;
                StartCoroutine(ShakeEffect(0.75f, 0.25f));
            }

            yield return null;
        }
    }

}