using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SongInfo
{
    public int bpm = GlobalVariables.bpm;
    public int beatsPerMeasure = 4;
    
    // Returns the time in seconds for each beat
    public float GetSecondsPerBeat()
    {
        return 60.0f / bpm;
    }

    public int GetBeatsPerMeasure()
    {
        return beatsPerMeasure;
    }
    public void UpdateBPM() {
        bpm = GlobalVariables.bpm;
    }
}
