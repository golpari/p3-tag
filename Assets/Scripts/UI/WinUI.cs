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
            // TODO: rearrange this, the number of lives is decreased in the playerController based on the thief died event
            //TODO (for when we have multiple environments): add a check that checks if the num of lives hits 0 or below, if yes, restart from the first scene instead of just the one you are in
            //PlayerController.num_lives -= 1;
            GetComponent<Text>().text = "The Ghost caught the player!\n Press any key to restart the level";
        }
        else
        {
            GetComponent<Text>().text = "The Player successfully escaped! \n Press any key to continue";
        }
    }

    private void OnDestroy()
    {
        EventBus.Unsubscribe(gameEndSubscription);
    }
}
