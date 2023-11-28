using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    Subscription<EndGameEvent> gameEndSubscription;
    public Sprite player;
    public Sprite ghost;

    void Start()
    {
        gameEndSubscription = EventBus.Subscribe<EndGameEvent>(_OnPlayerWin);
    }

    void _OnPlayerWin(EndGameEvent e)
    {
        if (e.playerWinnerName == "Ghost")
        {
            GetComponent<Image>().sprite = player;
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            GetComponent<Image>().sprite = ghost;
            GetComponent<Image>().color = Color.blue;
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
