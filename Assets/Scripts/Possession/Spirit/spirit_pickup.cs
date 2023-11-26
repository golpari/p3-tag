using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spirit_pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int index;
    public float spirit_gain;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GhostController>() != null)
        {
            EventBus.Publish<SpiritEvent>(new SpiritEvent(spirit_gain));
            EventBus.Publish<SpiritPickup>(new SpiritPickup(index, other.gameObject.tag)); // which index?

        }

        // call spawnpickup
    }
}
