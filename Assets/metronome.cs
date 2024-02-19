using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metronome : MonoBehaviour
{
    // Start is called before the first frame update
    public float bpm = 135f; // Beats per minute

    public fmodTimer timer;
    private float beatDuration; // Duration of each beat in seconds
    private float nextBeatTime; // Time of the next beat
    private int scaleSign = 1; // Sign of the scale on the X-axis

    void Start()
    {
        // Calculate the duration of each beat
        beatDuration = (60f / bpm)/2;
        // Set the initial time of the next beat
        nextBeatTime = Time.time + beatDuration;
    }

    void Update()
    {
        // Check if it's time for the next beat
        if (Time.time >= nextBeatTime)
        {
            // Toggle the sign of the scale on the X-axis
            scaleSign *= -1;
            // Update the scale of the GameObject
            if (timer.BeatChange())
            {
                transform.localScale = new Vector3(scaleSign * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            // Calculate the time of the next beat
            nextBeatTime += beatDuration;
        }
    }
}
