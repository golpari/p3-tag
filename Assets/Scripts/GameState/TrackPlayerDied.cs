using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script goes on the player and publishes a 'died' event every time
 * the player either falls off the map or if the the players health hits 0.
 * Traps marked with the 'DeathTrap' tag just injure the player.
 */
public class TrackPlayerDied : MonoBehaviour
{
    bool alreadyDead = false;
    private float timeSinceLastEvent = 0f; // Time tracker
    [SerializeField] private AudioSource lavaSound;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastEvent += Time.deltaTime; // Update the timer every frame

        if (this.transform.position.y > -15)
        {
            alreadyDead = false;
        }

        //check if the player has fallen off the map or into a pit
        if (this.transform.position.y < -15 && !alreadyDead)
        {
            EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(25, true));
            alreadyDead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the player collides with a DeathTrap
        if (other.gameObject.CompareTag("DeathTrap"))
        {
            // listen for this event in another script and decrease the number of lives
            EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(25, true));
        }

        // Check if the player collides with lava
        if (other.gameObject.CompareTag("Lava"))
        {
            // Check if 2 seconds have passed
            if (timeSinceLastEvent >= 2f)
            {
                lavaSound.Play();

                EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(45, false));
                timeSinceLastEvent = 0f; // Reset the timer
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the player collides with lava
        if (other.gameObject.CompareTag("Lava"))
        {
            // Check if 2 seconds have passed
            if (timeSinceLastEvent >= 2f)
            {
                EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(30, false));
                timeSinceLastEvent = 0f; // Reset the timer
            }
        }
    }

    // for sake of consistency/less bugs, check for both triggers and collisions
    private void OnCollisionEnter(Collision collision)
    {
        // check if the player collides with a DeathTrap
        if (collision.gameObject.CompareTag("DeathTrap"))
        {
            // listen for this event in another script and decrease the number of lives
            EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(25, true));
        }

        // Check if the player collides with lava
        if (collision.gameObject.CompareTag("Lava"))
        {
            lavaSound.Play();

            // Check if 2 seconds have passed
            if (timeSinceLastEvent >= 2f)
            {
                EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(45, false));
                timeSinceLastEvent = 0f; // Reset the timer
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the player collides with lava
        if (collision.gameObject.CompareTag("Lava"))
        {
            // Check if 2 seconds have passed
            if (timeSinceLastEvent >= 2f)
            {
                EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(30, false));
                timeSinceLastEvent = 0f; // Reset the timer
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            lavaSound.Stop();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            lavaSound.Stop();
        }
    }
}
