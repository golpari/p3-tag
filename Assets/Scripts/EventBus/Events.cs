using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class ChangeGravityEvent
{
    public static float gravityScale;

    // set the gravity scale to the given value
    public ChangeGravityEvent(float _gravityScale, GameObject _gravityFX)
    {
        //Debug.Log("change to: " + _gravityScale + " from:" + gravityScale);
        if (gravityScale != _gravityScale)
            // Toggle low gravity visual effects on and off when gravity has changed
            _gravityFX.GetComponent<PostProcessVolume>().enabled = !_gravityFX.GetComponent<PostProcessVolume>().enabled;

        gravityScale = _gravityScale;
    }
}

public class ChangeLightingEvent
{
    public bool isDark;
    public ChangeLightingEvent(bool becomeDark) { isDark = becomeDark; }
}

public class OutlineEvent
{
    public bool shouldOutline;
    public OutlineEvent(bool _shouldOutline) { shouldOutline = _shouldOutline; }
}

public class PossessionEvent
{
    public InputActionAsset inputAsset;
    public PossessionEvent(InputActionAsset _inputAsset) { inputAsset = _inputAsset; }
}

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
    public ArtifactPickupEvent(int _artRoomNum){artRoomNum = _artRoomNum; }
}

public class ThiefDiedEvent
{
    public int livesLost;
    public ThiefDiedEvent(int _livesLost) { livesLost = _livesLost; }

}

