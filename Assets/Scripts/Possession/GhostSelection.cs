
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSelection : MonoBehaviour
{
    public LayerMask raycastLayerMask; // LayerMask to filter which objects are raycasted
    public float lenOfBox = 1f;
    private GameObject previousClosestObject = null; // To keep track of the previously closest object
    private GameObject closestObject = null;

    // temp fix for having no visible outline at the start
    private void Awake()
    {
        Outline[] allOutlines = GameObject.FindObjectsOfType<Outline>();
        foreach (Outline outline in allOutlines)
        {
            outline.enabled = false;
        }
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
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

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
        if (outline != null)
        {
            outline.enabled = true;
            if (obj.gameObject.tag == "torch")
            {
                if (spirit_slider.current_value >= 25f)
                {
                    EventBus.Publish<OutlineEvent>(new OutlineEvent(true));
                }
                else {
                    outline.OutlineColor = Color.red;
                }
                
            }
            else {
                EventBus.Publish<OutlineEvent>(new OutlineEvent(true));
            }
            
        }
        else
            Debug.Log("outline enable is null on " + obj.name);
    }

    void DisableOutlineEffect(GameObject obj)
    {
        Outline outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
            outline.OutlineColor = Color.blue;
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
        Gizmos.DrawWireCube(boxCenter, boxHalfExtents * 2); // Multiply by 2 because it needs full size
    }

}

