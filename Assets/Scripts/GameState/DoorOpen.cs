using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    private Subscription<ArtifactPickupEvent> artifactPickupSubscription;
    [SerializeField] private int roomNumber = 0;
    [SerializeField] private float speed = 2;
    [SerializeField] private float distanceUp = 3;

    //private bool moveDoor = false;
    // Start is called before the first frame update

    public static bool artifactPicked = false;
    Vector3 start;
    void Start()
    {
        start = transform.position;
        if (artifactPicked) {
        transform.position += new Vector3(0, distanceUp, 0);
        }
        EventBus.Subscribe<ArtifactPickupEvent>(_OnArtifactPickup);
        EventBus.Subscribe<Reset>(_reset);
    }

    void _reset(Reset e) {
        artifactPicked = false;
        transform.position = start;
    }

    // Update is called once per frame


    void _OnArtifactPickup(ArtifactPickupEvent e)
    {
        // if the artifact of the room that this door is in gets picked up'
        if (this != null)
        {
            if (e.artRoomNum == roomNumber)
            {
                //open door (slide it up)

                StartCoroutine(SlideUpCoroutine(distanceUp, speed));
            }


        }
    }

    IEnumerator SlideUpCoroutine(float distance, float duration)
    {
        artifactPicked = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, distance, 0);
        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null; // Wait until next frame
        }

        transform.position = endPosition; // Ensure the position is set exactly at the end position after the loop
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnDestroy()
    {
        artifactPicked = false;
        EventBus.Unsubscribe(artifactPickupSubscription);
    }
}
