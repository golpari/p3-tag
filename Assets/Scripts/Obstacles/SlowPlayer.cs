using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Put this script on the player.
 * This script slows the player's movement down while the any trap is interacted with.
 * These types of traps are tagged with "SlowTrap" and include traps like quicksand.
 */

public class SlowPlayer : MonoBehaviour
{

    private float originalSpeed = 0;
    private float slowSpeed = 0;
    private PlayerController playerController;
    [SerializeField] private AudioSource sloshSound;

    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();

        originalSpeed = playerController.movementSpeed;
        slowSpeed = 1.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        // slow to half speed
        if (other.gameObject.CompareTag("SlowTrap"))
        {
            playerController.movementSpeed = slowSpeed;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        // slow to half speed
        if (other.gameObject.CompareTag("SlowTrap"))
        {
            playerController.movementSpeed = slowSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // reset to regular speed when exiting the slowtrap
        if (other.gameObject.CompareTag("SlowTrap"))
        {
            playerController.movementSpeed = originalSpeed;
        }
    }


}
