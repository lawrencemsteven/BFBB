using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SongInfo
{
    private static uint bpm = 120u;
    private static uint beatsPerMeasure = 4u;

    private static float secondsPerBeat;
    private static float nextBeatTime = 0.0f;
    private static uint measureCounter = 0u;


    private static void Start()
    {
        // NEED TO GET bpm
        // bpm = ???

        // NEED TO GET beatsPerMeasure
        // beatsPerMeasure = ???

        secondsPerBeat = bpm / 60.0f;

        nextBeatTime = Time.time + secondsPerBeat;
    }

    private static void Update()
    {
        // Time passing for each beat
        if (Time.time > nextBeatTime)
        {
            nextBeatTime += secondsPerBeat;
            onBeat();
            measureCounter += 1;

            // Enough beats for a measure
            if (measureCounter >= beatsPerMeasure)
            {
                onMeasure();
            }
        }
    }

    private static void onBeat()
    {
        // Call Station onBeat() Functions
    }

    private static void onMeasure()
    {
        // Call Station onMeasure() Functions
    }

    // Returns a range (-0.5f, 0.5f] that will determine how far away or close to a beat this function was called.
    private static float onBeatCheck()
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
    private static float getSecondsPerBeat()
    {
        return secondsPerBeat;
    }
}
