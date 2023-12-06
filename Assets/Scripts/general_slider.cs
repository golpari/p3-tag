using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Slider = UnityEngine.UI.Slider;

public class general_slider : MonoBehaviour
{
    float progress = 0.0f;
    public Slider slide;
    float current_value;
    float initial_time;
    bool GameEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<EndGameEvent>(_change_bool);
        slide = GetComponent<Slider>();
        current_value = 100.0f;
        EventBus.Subscribe<Reset>(_reset);
    }

    void _reset(Reset e)
    {
        current_value = 100;
        GameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        slide.value = current_value;
    }

    void _change_bool(EndGameEvent e) {
        GameEnd = true;
    }

    public IEnumerator start_slide(float start,float end,float duration)
    {
        float test = 0.0f;

        initial_time = Time.time;
        progress = (Time.time - initial_time) / duration;
        current_value = start;

        while (progress < 1.0f && !GameEnd)
        {
            test += Time.deltaTime;
            progress = (Time.time - initial_time) / duration;
            current_value = Mathf.Lerp(start, end, progress);
            yield return null;
        }
        current_value = end;
    }

    public void set_value(float val) {
        initial_time += val;
    }
}

