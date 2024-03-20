using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SongInfo
{
    public uint bpm = GlobalVariables.bpm;
    public uint beatsPerMeasure = 4u;
    
    // Returns the time in seconds for each beat
    public float GetSecondsPerBeat()
    {
        return 60.0f / bpm;
    }

    public uint GetBeatsPerMeasure()
    {
        return beatsPerMeasure;
    }
    public void UpdateBPM() {
        bpm = GlobalVariables.bpm;
    }
}
