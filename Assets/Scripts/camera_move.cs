using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_move : MonoBehaviour
{
    Vector3[] camera_pos = { new Vector3(0, 0, -10), new Vector3(21, 0, -10), new Vector3(28,10,-10) };
    Vector3[] starting_pos = { new Vector3(0, -11, 0), new Vector3(21, -11, 1), new Vector3(43, -11, 8) };
    Vector3[] ghost_pos = { new Vector3(1, -11, 0), new Vector3(22, -11, 1), new Vector3(45, -11, 8) };

    public GameObject player;
    public GameObject ghost;

    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<int>(_change_index);
        EventBus.Publish<int>(0);
    }


    void _change_index(int ind) {
        if (ind < 3)
        {
            this.transform.position = camera_pos[ind];
            player.transform.position = starting_pos[ind];
            ghost.transform.position = ghost_pos[ind];
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
