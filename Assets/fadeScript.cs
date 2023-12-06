using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class fadeScript : MonoBehaviour
{
    CanvasGroup load;

    // Start is called before the first frame update
    void Start()
    {
        load = GetComponent<CanvasGroup>();
        load.alpha = 1.0f;
        EventBus.Subscribe<fadeOut>(_fade_change);
        StartCoroutine(startup());
       
    }



    IEnumerator startup() {
        yield return new WaitForSeconds(1.0f);
        EventBus.Publish<fadeOut>(new fadeOut(false));
    }

    public void _fade_change(fadeOut e)
    {
        float start = 0.0f, end = 1.0f;
        if (!e.fade) {
            start = 1.0f;
            end = 0.0f;
        }
        StartCoroutine(fade_out(start,end,0.75f));
    }

    public IEnumerator fade_out(float start, float end, float duration) {

        float initial_time = Time.time;
        // The "progress" variable will go from 0.0f -> 1.0f over the course of "duration_sec" seconds.
        float progress = (Time.time - initial_time) / duration;

        while (progress < 1.0f)
        {
            progress = (Time.time - initial_time) / duration;
            load.alpha = Mathf.Lerp(start, end, progress); ;
            yield return null;
        }
        load.alpha = end;
    }

}
