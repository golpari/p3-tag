using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script goes on the player and publishes a 'died' event every time
 * the player either falls off the map or comes into contact with a trap.
 * Trap gameobjects that kill the player on impact are marked in the inspector with the 'DeathTrap' tag.
 */
public class TrackPlayerDied : MonoBehaviour
{
    bool alreadyDead = false;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y > -15)
        {
            alreadyDead = false;
        }

        //check if the player has fallen off the map or into a pit
        if (this.transform.position.y < -15 && !alreadyDead)
        {
            EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(1));
            alreadyDead = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if the player collides with a DeathTrap
        if (other.gameObject.CompareTag("DeathTrap"))
        {
            // listen for this event in another script and decrease the number of lives
            EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(1));
        }
    }

    // for sake of consistency/less bugs, check for both triggers and collisions
    private void OnCollisionEnter(Collision collision)
    {
        // check if the player collides with a DeathTrap
        if (collision.gameObject.CompareTag("DeathTrap"))
        {
            // listen for this event in another script and decrease the number of lives
            EventBus.Publish<ThiefDiedEvent>(new ThiefDiedEvent(1));
        }
    }
}
