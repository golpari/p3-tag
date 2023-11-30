using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class spirit_slider : MonoBehaviour
{
    // Start is called before the first frame update

    public static float current_value;
    public float min;
    public float max;

    bool stop_possesion = false;
    public static bool zero_spirit;

    Slider slider;


    float A = -1.5f;


    void Start()
    {
        zero_spirit = false;
        slider = this.GetComponent<Slider>();
        EventBus.Subscribe<SpiritEvent>(_spirit);
        EventBus.Subscribe<SpiritPossesion>(_spiritPoss);
        current_value = 50.0f;
    }

    void _spirit(SpiritEvent e) {
        if (current_value + e.spirit < max) {
            current_value += e.spirit;
        }
        //StartCoroutine(run_possesion());
    }


    void _spiritPoss(SpiritPossesion e)
    {
        if (e.active_inactive == true)
        {
            StartCoroutine(run_possesion(e.scale_factor));
        }
        else if (e.active_inactive == false) {
            stop_possesion = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = current_value;


    }

    // enable action and disable action
    public IEnumerator run_possesion(float scale_factor) {
        float past = current_value;
        float t = 0.0f;
        float limit = Mathf.Ceil(Mathf.Abs(current_value / A));

        while (t <= limit && !stop_possesion) {
            t += Time.deltaTime;
            current_value = scale_factor * t + past;
            yield return null;
        }

        if (current_value <= 0.0f)
        {
            current_value = 0.0f;
            zero_spirit = true;
        }

        stop_possesion = false;
        yield return null;
    }


    public IEnumerator increase_decrease(float val) {
        float add = (val) / (10);
        for (int i = 0; i < 10; i++)
        {
            current_value += add;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }
}
