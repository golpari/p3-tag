using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPickup : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    public int roomNumber = 0;
    AudioClip copy;
    private void Start()
    {
        copy = Camera.main.GetComponent<AudioSource>().clip;
        EventBus.Subscribe<Reset>(_reset);
    }

    void _reset(Reset e) { 
        this.gameObject.SetActive(true);
        Camera.main.GetComponent<AudioSource>().Stop();
        Camera.main.GetComponent<AudioSource>().clip = copy;
        Camera.main.GetComponent<AudioSource>().Play();
    }

    [SerializeField] LayerMask exclude;
    [SerializeField] LayerMask include;
    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the artifact's trigger collider
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            GameObject[] locks = GameObject.FindGameObjectsWithTag("i_wall");
            
            for (int i = 0; i < locks.Length; i++)
            {
                locks[i].GetComponent<BoxCollider>().excludeLayers = exclude;
                locks[i].GetComponent<BoxCollider>().includeLayers = include;
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
