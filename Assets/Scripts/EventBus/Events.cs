using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChangeGravityEvent
{
    public float gravityScale;

    // set the gravity scale to the given value
    public ChangeGravityEvent(float _gravityScale, GameObject _gravityFX)
    {
        gravityScale = _gravityScale;

        // Toggle low gravity visual effects on and off.
        _gravityFX.GetComponent<PostProcessVolume>().enabled = !_gravityFX.GetComponent<PostProcessVolume>().enabled;
    }

    
}

public class ChangeLightingEvent
{
    public bool isDark;
    public ChangeLightingEvent(bool becomeDark) { isDark = becomeDark; }
}

/*public class ScoreEvent
{
    // Static members to keep track of scores across all instances of the event
    private static int player1Points = 0;
    private static int player2Points = 0;

    public ScoreEvent(int playerNumber)
    {
        if (playerNumber == 1)
            player1Points++;
        else if (playerNumber == 2)
            player2Points++;
        else
            throw new ArgumentException("Invalid player number. Must be 1 or 2.");

        if (player1Points >= 5)
        {
            // Player 1 wins
            EventBus.Publish<WinEvent>(new WinEvent(1));
        }
        else if (player2Points >= 5)
        {
            // Player 2 wins
            EventBus.Publish<WinEvent>(new WinEvent(2));
        }
    }

    public static int GetPlayer1Points()
    {
        return player1Points;
    }

    public static int GetPlayer2Points()
    {
        return player2Points;
    }
    public static void ClearPoints()
    {
        player1Points = 0;
        player2Points = 0;
    }
}*/

public class EndGameEvent
{
    public string playerWinnerName;

    public EndGameEvent(string _playerWinnerName){playerWinnerName = _playerWinnerName;}
}

public class EndCountdownEvent
{
    public EndCountdownEvent() { }
}

public class StartGameEvent
{
    public StartGameEvent() { }
}

public class ArtifactPickupEvent
{
    public int artRoomNum;
    public ArtifactPickupEvent(int _artRoomNum){artRoomNum = _artRoomNum;}
}
