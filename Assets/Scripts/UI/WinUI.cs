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
        //Debug.Log(EntrywayTrigger.level);
        if (e.playerWinnerName == "Ghost")
        {
            PlayerController.num_lives -= 1;
            GetComponent<Text>().text = "The Ghost caught the player!\n Press any key to restart the level";
        }
        else if (EndGame.index == 3)
        {
            GetComponent<Text>().text = "The Player successfully escaped! \n Press any key to continue";
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }
}
