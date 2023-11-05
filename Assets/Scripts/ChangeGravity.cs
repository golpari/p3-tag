using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    public float newGravity = 0.1f;
    void OnCollisionEnter(Collision other)
    {
        // Attempt to retrieve the GhostController component on the other object
        GhostController ghost = other.gameObject.GetComponent<GhostController>();

        // Check if the component was found
        if (ghost != null)
        {
            EventBus.Publish<ChangeGravityEvent>(new ChangeGravityEvent(newGravity));
        }
    }

}
