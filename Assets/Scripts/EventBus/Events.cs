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

public class SpiritPickup {

    public int index;
    public string name;

    public SpiritPickup(int index, string name)
    {
        this.index = index;
        this.name = name;
    }
}

public class SpiritPossesion {
    public bool active_inactive;
    public float scale_factor = 1.0f;

    public SpiritPossesion(bool active_inactive, float scale_factor)
    {
        this.active_inactive = active_inactive;
        this.scale_factor = scale_factor;
    }

    public SpiritPossesion(bool active_inactive) {
        this.active_inactive = active_inactive;
    }
}

public class fadeOut {
    public bool fade;

    public fadeOut(bool fade)
    {
        this.fade = fade;
    }
}
