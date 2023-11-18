using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spirit_pickup : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GhostController>() != null) {
            EventBus.Publish<SpiritEvent>(new SpiritEvent(25));
            this.gameObject.SetActive(false);
        }
    }
}
