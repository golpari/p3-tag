using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    public int roomNumber = 0;
    private void Start()
    {
        EventBus.Subscribe<Reset>(_reset);
    }

    void _reset(Reset e) { 
        this.gameObject.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the artifact's trigger collider
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            GameObject[] locks = GameObject.FindGameObjectsWithTag("i_wall");
            Debug.Log(locks.Length);
            for (int i = 0; i < locks.Length; i++)
            {
                locks[i].SetActive(false);
            }
            // in another script, listen for this event and open a door based on the given room number of the event
            EventBus.Publish<StartCountDownTimer>(new StartCountDownTimer());
            EventBus.Publish<ArtifactPickupEvent>(new ArtifactPickupEvent(roomNumber));

            Camera cam  = Camera.main;

            Camera.main.GetComponent<AudioSource>().Stop();
            Camera.main.GetComponent<AudioSource>().clip = clip;
            Camera.main.GetComponent<AudioSource>().Play();

            // get rid of the artifact (it will still exist, but the player can't see or interact with it)
            this.gameObject.SetActive(false);

        }
    }
}
