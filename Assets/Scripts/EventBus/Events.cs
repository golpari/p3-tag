using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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


public class StartCountDownTimer
{
    public StartCountDownTimer() { }
}

public class PauseCountDownTimer
{
    public PauseCountDownTimer() { }
}

public class ChangeDoorsEvent
{
    public int doorRoomNum;
    public ChangeDoorsEvent(int _doorRoomNum) { doorRoomNum = _doorRoomNum; }
}



public class SpiritEvent
{
    public float spirit;
    public SpiritEvent(float _spirit) { this.spirit = _spirit; }
}

