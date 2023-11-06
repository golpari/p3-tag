using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrywayTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the doorway trigger collider
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            // End the level, player wins
            string winner = "Player";
            EventBus.Publish<EndGameEvent>(new EndGameEvent(winner));
        }
    }
}
