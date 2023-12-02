using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health : MonoBehaviour
{
    // Start is called before the first frame update
    public general_slider slider;
    public float duration;
    void Start()
    {
        EventBus.Subscribe<ThiefDiedEvent>(_health_decrease);
    }

    void _health_decrease(ThiefDiedEvent e) {
        if (slider.slide.value - e.livesLost > 0)
        {
            StartCoroutine(player_death((float)e.livesLost));
            if (e.resetPosition) { EventBus.Publish<respawn>(new respawn()); }
        }
        else {
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
