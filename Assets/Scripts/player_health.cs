using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_health : MonoBehaviour
{
    // Start is called before the first frame update
    public general_slider slider;
    public float duration;
    void Start()
    {
        EventBus.Subscribe<ThiefDiedEvent>(_health_decrease);
        EventBus.Subscribe<Reset>(_reset);
    }

    void _reset(Reset e)
    {
        //slider.slide.value = 100;
        StartCoroutine(player_death(-1 * (100 - slider.slide.value)));
    }
    void _health_decrease(ThiefDiedEvent e) {
        if (slider.slide.value - e.livesLost > 0)
        {
            StartCoroutine(player_death((float)e.livesLost));
            if (e.resetPosition) { EventBus.Publish<respawn>(new respawn()); }
        }
        else {
            slider.slide.value = 0;
            EventBus.Publish<EndGameEvent>(new EndGameEvent("Ghost"));
        }
    }

    public IEnumerator player_death(float val)
    {
        yield return StartCoroutine(slider.start_slide(slider.slide.value, slider.slide.value - val, duration));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
