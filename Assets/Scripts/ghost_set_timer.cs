using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghost_set_timer : MonoBehaviour
{
    public general_slider slider;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<ghost_set>(_bar_show);
        EventBus.Subscribe<ghost_set>(_start_count);
        EventBus.Subscribe<button_mash>(button_mash);
        slider.slide.gameObject.SetActive(false);
    }

    void _start_count(ghost_set e) {
        StartCoroutine(gate_lock(e.duration));
    }

    void _bar_show(ghost_set e) { 
        slider.slide.gameObject.SetActive(true);
    }

    void button_mash(button_mash e) {
        slider.set_value(e.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator gate_lock(float duration) { 
    yield return StartCoroutine(slider.start_slide(100.0f,0.0f, duration));
    PlayerController.player_lock = false;
    EventBus.Publish<StartCountDownTimer>(new StartCountDownTimer());
    slider.slide.gameObject.SetActive(false);
    }
}
