using UnityEngine;
using System.Collections;

public abstract class ObjectPossessed : MonoBehaviour
{
    protected float speed = -1f;

    // Isn't currently implemented in PossessionController, but would be easy to add
    // Would allow us to fine tune the speed of the individual objects instead of
    // relying on their possesion type
    public float GetPossessedSpeed()
    {
        return speed;
    }
}

