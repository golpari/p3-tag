using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spirit_spawn : MonoBehaviour
{
    // Start is called before the first frame update
    public int number_spirits;
    public BoxCollider [] test_trig;
    public GameObject[] spirit_lis;

    public GameObject spirit_prefab;

    [SerializeField] LayerMask _donotspawnon;

    int room_index = 0;

    void Start()
    {
        EventBus.Subscribe<ChangeDoorsEvent>(_reset);

        EventBus.Subscribe<SpiritPickup>(_pickup);


        spirit_lis = new GameObject[number_spirits];
        for(int i = 0; i < number_spirits; i++)
        {
            spirit_lis[i] = Instantiate(spirit_prefab);
            spirit_lis[i].gameObject.GetComponent<spirit_pickup>().index = i;
        }


        new_room(test_trig[room_index]);

        // when [icked up by ghost affects spirit
        // when picked up by player affects time

        EventBus.Subscribe<Reset>(_resetA);
    }

    // Update is called once per frame


    void _resetA(Reset e) {
        room_index = 0;
        new_room(test_trig[room_index]);
    }


    void _reset(ChangeDoorsEvent e) {
        room_index = e.doorRoomNum;
        new_room(test_trig[room_index]);
    }


    public void turn_on(float val) {
        for (int i = 0; i < number_spirits; i++)
        {
            /*Color temp = spirit_lis[i].GetComponent<MeshRenderer>().material.color;
            temp.a = val;
            spirit_lis[i].GetComponent<MeshRenderer>().material.color = temp;*/
        }
    }

    void _pickup(SpiritPickup e) {
        Single(test_trig[room_index], e.index);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("Q Pressed");
            new_room(test_trig[room_index]); // problem here when pressed nothing happens why?
        }
    }


    Vector3 RandomSpawnPosition(BoxCollider coll) {
        Bounds bounds = coll.bounds;
        float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
        float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
        float z = UnityEngine.Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }

    void new_room(BoxCollider coll) {
        for (int i = 0; i < spirit_lis.Length; i++) {
            for (int j = 0; j < 300; j++) {
                Single(coll, i);
                
            }
            
        }
    }
    
    void Single(BoxCollider coll, int index) {
        Vector3 possible = RandomSpawnPosition(coll);
        
        Collider[] colliders = Physics.OverlapSphere(possible, 0.5f);

        for (int k = 0; k < colliders.Length; k++)
        {
            if (((colliders[k].gameObject.layer) & _donotspawnon) != 0)
            {
                spirit_lis[index].transform.position = possible;
                break;
            }
        }
        
    }


}
