using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Put this script on the player.
 * This script slows the player's movement down while the any trap is interacted with.
 * These types of traps are tagged with "SlowTrap" and include traps like quicksand.
 */

public class QuicksandAudio : MonoBehaviour
{
    [SerializeField] private AudioSource sloshSound;

    private void OnTriggerEnter(Collider other)
    {
        // slow to half speed
        if (other.gameObject.CompareTag("Player"))
        {
            sloshSound.Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        // reset to regular speed when exiting the slowtrap
        if (other.gameObject.CompareTag("Player"))
        {
            sloshSound.Stop();
        }
    }
}
