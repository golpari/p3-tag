using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider_prog : MonoBehaviour
{
    bool change_color = false;
    Color primary;
    float factor;
    // Start is called before the first frame update
    void Start()
    {
        primary = this.GetComponent<Image>().color;
        factor = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!change_color && this.GetComponent<Image>().color == primary)
        {
            StartCoroutine(changeColor(primary, Color.green));
        }
        else if (!change_color && this.GetComponent<Image>().color == Color.green)
        {
            StartCoroutine(changeColor(Color.green, primary));
        }

        if (spirit_slider.current_value >= 100)
        {
            factor = 1f;
        }
        else {
            factor = 2f;
        }

    }

    private IEnumerator changeColor(Color firstcolor, Color secondcolor)
    {
        change_color = true;
        float t = 0;
        while (t < factor)
        {
            t += Time.deltaTime;
            this.GetComponent<Image>().color = Color.Lerp(firstcolor, secondcolor, t);
            yield return null;
        }
        change_color = false;
    }

}

