using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrywayTrigger : MonoBehaviour
{
    public static int level = 0;
    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the doorway trigger collider
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            ++level;
            // End the level, player wins
            string winner = "Player";
            EventBus.Publish<EndGameEvent>(new EndGameEvent(winner));
        }
    }
}
