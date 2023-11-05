using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    public int roomNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // in another script, listen for this event and open a door based on the given room number of the event
            EventBus.Publish<ArtifactPickupEvent>(new ArtifactPickupEvent(roomNumber));

            // get rid of the artifact (it will still exist, but the player can't see or interact with it)
            this.gameObject.SetActive(false);
        }
    }
}
