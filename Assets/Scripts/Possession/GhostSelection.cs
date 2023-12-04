using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSelection : MonoBehaviour
{
    public LayerMask raycastLayerMask; // LayerMask to filter which objects are raycasted
    public float lenOfBox = 2f;
    private GameObject previousClosestObject = null; // To keep track of the previously closest object
    private GameObject closestObject = null;
    private Subscription<PossessionEvent> possessionSubscription;
    private bool hasPossession = false;

    // temp fix for having no visible outline at the start
    private void Awake()
    {
        Outline[] allOutlines = GameObject.FindObjectsOfType<Outline>();
        foreach (Outline outline in allOutlines)
        {
            outline.enabled = false;
        }

        // subscribe to possession so know when to stop outlining
        EventBus.Subscribe<PossessionEvent>(_OnPossession);
    }

    private void _OnPossession(PossessionEvent e)
    {
        // toggle it bc (hopefully) will never have possession event without changing
        // whether or not ghost currently has possession of something
        hasPossession = !hasPossession;
    }

    // perform a raycast box around the player and find the closest object
    void Update()
    {
        Vector3 boxCenter = transform.position + new Vector3(0, 1.5f, 0); // Center of the box, needs to be adjusted for animation
        Vector3 boxHalfExtents = new Vector3(lenOfBox, lenOfBox, lenOfBox); // Half the size of the box in each direction
        Quaternion boxOrientation = Quaternion.identity; // Rotation of the box, 'Quaternion.identity' for no rotation
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, boxHalfExtents, boxOrientation, raycastLayerMask);

        GameObject closestObject = FindClosestObject(hitColliders);

        UpdateOutlineEffect(closestObject);
    }

    GameObject FindClosestObject(Collider[] colliders)
    {
        /*Debug.Log("object count: " + colliders.Length);
        Debug.Log("has possession: " + hasPossession);
        Debug.Log("closest object: " + closestObject);*/
        // If there is a closest object but it isn't in range and not currently possessed, deactivate outline
        if (colliders.Length == 0 && !hasPossession && closestObject)
        {
            DisableOutlineEffect(closestObject);
            closestObject = null;
        }
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // find the closest object in raycast box to the ghost
        foreach (Collider collider in colliders)
        {
            float distance = (collider.gameObject.transform.position - currentPosition).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = collider.gameObject;
            }
        }

        return closestObject;
    }

    public GameObject GetClosestObject()
    {
        return closestObject;
    }

    // update the outline effect, enabling it on the closest object and disabling it on others
    void UpdateOutlineEffect(GameObject closestObject)
    {
        if (closestObject != previousClosestObject)
        {
            if (previousClosestObject != null)
            {
                DisableOutlineEffect(previousClosestObject);
            }

            if (closestObject != null)
            {
                EnableOutlineEffect(closestObject);
            }

            previousClosestObject = closestObject;
        }
    }

    void EnableOutlineEffect(GameObject obj)
    {
        Outline outline = obj.GetComponent<Outline>();
        PossessionActionBase type = obj.GetComponent<PossessionActionBase>();
        if (outline != null)
        {
            outline.enabled = true;
            // change color of outline depending on if ghost can afford or not
            if (type.AffordCheck()) outline.OutlineColor = Color.blue;
            else outline.OutlineColor = Color.red;
            outline.OutlineWidth = 5;
            EventBus.Publish<OutlineEvent>(new OutlineEvent(true));
        }
        else
            Debug.Log("outline enable is null on " + obj.name);
    }

    void DisableOutlineEffect(GameObject obj)
    {
        if (obj == null)
        {
            Debug.Log("Attempted to disable outline effect on a null object.");
            return;
        }
        Outline outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
            EventBus.Publish<OutlineEvent>(new OutlineEvent(false));
        }
        else
        {
            Debug.Log("outline disable is null on" + obj.name);
        }
    }

    // called by Unity to draw Gizmos to visualize raycast.
    void OnDrawGizmos()
    {
        Vector3 boxCenter = transform.position + new Vector3(0,1.5f,0); // Center of the box
        Vector3 boxHalfExtents = new Vector3(lenOfBox, lenOfBox, lenOfBox); // Half the size of the box in each direction

        // Draw a wire cube at the position with the given size
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCenter, boxHalfExtents * 4); // Multiply by 2 because it needs full size
    }

}

