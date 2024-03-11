using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class StationInfo
{
    public string earlySound;
    public string onTimeSound;
    public string lateSound;

    public void SetSounds(string early, string onTime, string late)
    {
        earlySound = early;
        onTimeSound = onTime;
        lateSound = late;
    }
}
