using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class shadow_script : MonoBehaviour
{
    public GameObject shadow;
    GameObject shadow_brush;
    Vector3 offset = new Vector3(0.0f, 0.6f, 0.0f);
    public Color color;
    public float MaxY;
    float previous = 0.0f;
    public LayerMask layer;
    bool draw = false;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        shadow_brush = Instantiate(shadow);
        shadow_brush.GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame

    // bug 00 with ghost where when the ghost goes up and down, the shadow won't follow it until moves up and down again or goes into the quicksand
    // solution factor out test_trigger

    // bug 01 when ghost is on column it move the shadow forward?
    // solution the the shadow was displaying the x and z positions of the object hit, not the actual object
    void FixedUpdate()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + (Vector3.down * 100));
       
        if (Physics.Raycast(this.transform.position, Vector3.down, out hit, Mathf.Infinity, layer))
        {
            draw = true;
            

        }
        else {
            draw = false;
        }

    }

    private void Update()
    {
        if (draw)
        {
            shadow_brush.SetActive(true);
            float distance = this.transform.position.y - hit.collider.gameObject.transform.position.y;
            if (this.gameObject.tag == "Ghost" || this.gameObject.tag == "Player")
            {
                shadow_brush.transform.localScale = new Vector3(distance / 7.0f + 1, distance / 7.0f + 1, 1.0f);
            }
            else {
                shadow_brush.transform.localScale = new Vector3(distance / 5.0f + 1,distance / 5.0f + 1, 1.0f);
            }
                
            float x = this.gameObject.transform.position.x;
            float y = hit.collider.gameObject.transform.position.y;
            float z = this.gameObject.transform.position.z;

            shadow_brush.transform.position = new Vector3(x, y, z) + new Vector3(0.0f, 0.6f, 0.0f);
        }
        else {
            shadow_brush.gameObject.SetActive(false);
        }
    }
}
