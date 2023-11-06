using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CountdownUI : MonoBehaviour
{
    public float timeLeft = 10.0f;
    private Text timeComp;
    private bool keepCounting = true;
    private bool runGame = true;
    Subscription<StartGameEvent> startEventSubscription;
    private void Start()
    {
        timeComp = GetComponent<Text>();
        // ignore for now
        startEventSubscription = EventBus.Subscribe<StartGameEvent>(_OnGameStart);
    }

    // ignore for now
    void _OnGameStart(StartGameEvent e)
    {
        runGame = true;
    }

    void Update()
    {
        // If the game has started
        if (runGame)
        {
            // Countdown the time
            timeLeft -= Time.deltaTime;
            if (keepCounting)
                timeComp.text = (timeLeft).ToString("0"); //default 0
            // If the time has run out
            if (timeLeft < 0)
            {
                // Ghost wins, player loses
                EventBus.Publish<EndCountdownEvent>(new EndCountdownEvent());
                Debug.Log("End of countdown#");
                runGame = false;
            }
        }
    }
}