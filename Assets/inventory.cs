using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class inventory : MonoBehaviour
{

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = "Lives: " + PlayerController.num_lives;
    }
}
