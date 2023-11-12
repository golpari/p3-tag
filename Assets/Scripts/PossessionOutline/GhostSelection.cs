using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostSelection : MonoBehaviour
{
    public float raycastDistance = 10f; // Maximum distance of raycast
    public LayerMask raycastLayerMask; // LayerMask to filter which objects are raycasted
    private GameObject closestObject = null; // To keep track of the closest object
    private GameObject previousClosestObject = null; // To keep track of the previously closest object

    // perform raycasts in the four cardinal directions.
    void Update()
    {
        closestObject = null;
        float closestDistance = raycastDistance;

        CheckRaycast(Vector3.forward, ref closestDistance);
        CheckRaycast(-Vector3.forward, ref closestDistance);
        CheckRaycast(Vector3.right, ref closestDistance);
        CheckRaycast(-Vector3.right, ref closestDistance);

        UpdateOutlineEffect();
    }

    // perform a raycast in a given direction and update the closest object if necessary
    void CheckRaycast(Vector3 direction, ref float closestDistance)
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = direction;

        // Draw the ray in the Scene view
        Debug.DrawRay(rayOrigin, rayDirection * raycastDistance, Color.red, 0.1f);
        if (Physics.Raycast(transform.position, direction, out hit, raycastDistance, raycastLayerMask))
        {
            float distance = hit.distance;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = hit.collider.gameObject;
            }
        }
    }

    // update the outline effect, enabling it on the closest object and disabling it on others
    void UpdateOutlineEffect()
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
        var outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
        }
        else
            Debug.Log("outline comp is null");
    }

    void DisableOutlineEffect(GameObject obj)
    {
        var outline = obj.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
        else
        {
            Debug.Log("outline comp is null");
        }
    }





}
