using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    Subscription<EndGameEvent> gameEndSubscription;

    void Start()
    {
        gameEndSubscription = EventBus.Subscribe<EndGameEvent>(_OnPlayerWin);
    }

    void _OnPlayerWin(EndGameEvent e)
    {
        GetComponent<Text>().text = e.playerWinnerName + " Wins!\n Press any key or start to continue";
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }
}
