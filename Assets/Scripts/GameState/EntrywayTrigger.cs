using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrywayTrigger : MonoBehaviour
{
    public bool final_door;
    public static int level = 0;
    private void OnTriggerEnter(Collider other)
    {
        // If the player enters the doorway trigger collider
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            if (final_door)
            {
                string winner = "Player";
                EventBus.Publish<PauseCountDownTimer>(new PauseCountDownTimer());
                EventBus.Publish<EndGameEvent>(new EndGameEvent(winner));
            }
            
            ++level;
            
        }
    }
}
