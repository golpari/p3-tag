using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class general_slider : MonoBehaviour
{
    float progress = 0.0f;
    public Slider slide;
    float current_value;
    float initial_time;
    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>();
        current_value = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        slide.value = current_value;
    }


    public IEnumerator start_slide(float start,float end,float duration)
    {
        float test = 0.0f;

        initial_time = Time.time;
        progress = (Time.time - initial_time) / duration;
        current_value = start;
        while (progress < 1.0f)
        {
            test += Time.deltaTime;
            progress = (Time.time - initial_time) / duration;
            current_value = Mathf.Lerp(start, end, progress);
            yield return null;
        }
        current_value = end;
        Debug.Log(test);
    }

    public void set_value(float val) {
        initial_time += val;
    }
}

