using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    Subscription<EndGameEvent> gameEndSubscription;
    public Sprite player;
    public Sprite ghost;

    bool GameEnd = false;

    void Start()
    {
        gameEndSubscription = EventBus.Subscribe<EndGameEvent>(_OnPlayerWin);
        GameEnd = false;
    }

    void _OnPlayerWin(EndGameEvent e)
    {
        if (!GameEnd) {
            if (e.playerWinnerName == "Ghost")
            {
                GameEnd = true;
                GetComponent<Image>().sprite = player;
                GetComponent<Image>().color = Color.red;
            }
            else
            {
                GameEnd = true;
                GetComponent<Image>().sprite = ghost;
                GetComponent<Image>().color = Color.blue;
            }
        }
        
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }

}
