using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SongInfo : Singleton<SongInfo>
{
    private uint bpm = 60u;
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

        secondsPerBeat = 60.0F / bpm;

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
                measureCounter = 0;
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

    // Returns true if when called the beat is less than or equal to the provided acceptable offset

    public bool testInput(float offset) {
        float beatError = Mathf.Abs(onBeatCheck());
        Debug.Log(beatError);
        Debug.Log(offset);
        if (beatError <= offset) {
            return true;
            Debug.Log("True");
        }
        else {
            return false;
            Debug.Log("False");
        }
    }

    // Returns the time in seconds for each beat
    public float getSecondsPerBeat()
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

    public uint getBeatsPassed()
    {
        return measureCounter;
    }
}
