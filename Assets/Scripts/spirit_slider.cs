using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spirit_slider : MonoBehaviour
{
    // Start is called before the first frame update

    public static float current_value;
    public float min;
    public float max;

    Slider slider;

    void Start()
    {
        slider = this.GetComponent<Slider>();
        EventBus.Subscribe<SpiritEvent>(_spirit);
        current_value = 25.0f;
    }

    void _spirit(SpiritEvent e) {
        if (current_value + e.spirit >= min && current_value + e.spirit <= max) {
            StartCoroutine(increase_decrease(e.spirit));
        }
    }
    // Update is called once per frame
    void Update()
    {
        // for testing purposes
        if (Input.GetKeyDown(KeyCode.T))
        {
            EventBus.Publish<SpiritEvent>(new SpiritEvent(25));
        }
        else if(Input.GetKeyDown(KeyCode.E)) {
            EventBus.Publish<SpiritEvent>(new SpiritEvent(-25));
        }

        // for testing purposes

        slider.value = current_value;

       

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
