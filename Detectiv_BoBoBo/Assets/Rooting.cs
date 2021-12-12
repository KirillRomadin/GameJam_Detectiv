using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooting : MonoBehaviour
{
    // Transforms to act as start and end markers for the journey.
    private Transform startMarker;
    private Transform endMarker;
    
    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength = 45;

    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;
        speed = 3.0F;
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, 1, transform.position.z), 
                                          new Vector3(transform.position.x, 4, transform.position.z),
                                          fractionOfJourney);
    }
}
