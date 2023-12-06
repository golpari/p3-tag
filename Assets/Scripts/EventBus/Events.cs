using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class ChangeGravityEvent
{
    public static float gravityScale;

    // set the gravity scale to the given value
    public ChangeGravityEvent(float _gravityScale, GameObject _gravityFX)
    {
        Debug.Log(gravityScale);
        Debug.Log(_gravityScale);
        if (gravityScale != _gravityScale) {
            
        }
        _gravityFX.GetComponent<PostProcessVolume>().enabled = !_gravityFX.GetComponent<PostProcessVolume>().enabled;
        // Toggle low gravity visual effects on and off when gravity has changed


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
    public PossessionEvent(InputActionAsset _inputAsset) { inputAsset = _inputAsset;}
}

public class EndGameEvent
{
    public string playerWinnerName;

    public EndGameEvent(string _playerWinnerName){playerWinnerName = _playerWinnerName;}
}

public class PopUpEvent
{
    // currPlayer has to be either "thief" or "ghost" for manager to work correctly
    public string currIcon;
    public string currPlayer;

    public PopUpEvent(string _currIcon, string _currPlayer)
    {
        currIcon = _currIcon;
        currPlayer = _currPlayer;
    }
}

public class  ButtonPressEvent
{
    public string currButton;
    public string currPlayer;
    public ButtonPressEvent(string _currButton, string _currPlayer)
    {
        currButton = _currButton;
        currPlayer = _currPlayer;
        Debug.Log("button: " + currButton + " player: " + currPlayer);
    }

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
    public bool resetPosition;
    public ThiefDiedEvent(int _livesLost, bool _resetPosition) { 
        livesLost = _livesLost; 
        resetPosition = _resetPosition;
    }

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

public class ghost_set {
    public float duration;
    public ghost_set(float duration)
    {
        this.duration = duration;
    }
}

public class button_mash
{
    public float duration;
    public button_mash(float duration)
    {
        this.duration = duration;
    }
}

public class health_drop {
    public float value;
    public health_drop(float value)
    {
        this.value = value;
    }
}

public class respawn
{
    public respawn() { }
}

public class Reset {
    public Reset() { }
}
