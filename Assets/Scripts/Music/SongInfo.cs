using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SongInfo : Singleton<SongInfo>
{
    private uint bpm = 120u;
    private uint beatsPerMeasure = 4u;

    private float secondsPerBeat;
    private float nextBeatTime = 0.0f;
    private uint measureCounter = 0u;

    public UnityEvent onBeat = new();
    public UnityEvent onMeasure = new();

    private void Start()
    {
        // NEED TO GET bpm
        // bpm = ???

        // NEED TO GET beatsPerMeasure
        // beatsPerMeasure = ???

        secondsPerBeat = bpm / 60.0f;

        nextBeatTime = Time.time + secondsPerBeat;
    }

    private void Update()
    {
        // Time passing for each beat
        if (Time.time > nextBeatTime)
        {
            nextBeatTime += secondsPerBeat;
            onBeat?.Invoke();
            measureCounter += 1;

            // Enough beats for a measure
            if (measureCounter >= beatsPerMeasure)
            {
                onMeasure?.Invoke();
            }
        }
    }

    // Returns a range (-0.5f, 0.5f] that will determine how far away or close to a beat this function was called.
    private float onBeatCheck()
    {
        float beatTimeDifference = nextBeatTime - Time.time;
        float beatTimeRatio = beatTimeDifference / secondsPerBeat;
        if (beatTimeRatio > 0.5f)
        {
            return 1.0f - beatTimeRatio;
        }
        return -beatTimeRatio;
    }

    // Returns the time in seconds for each beat
    private float getSecondsPerBeat()
    {
        return secondsPerBeat;
    }

    public uint getBeatsPerMeasure()
    {
        return beatsPerMeasure;
    }

    public float getBeatProgress()
    {
        return (nextBeatTime - Time.time) / secondsPerBeat;
    }
}
