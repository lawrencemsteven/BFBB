using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metronome : MonoBehaviour
{
    private float beatDuration;
    private float nextBeatTime;
    private bool beatTimeInit = false;

    void Start()
    {
    }

    void Update()
    {
        if ((((int)SongInfo.Instance.getBeatsPassed()) == 1) && (!beatTimeInit))
        {
            beatTimeInit=true;
            nextBeatTime = ((float)SongInfo.Instance.getSecondsPerBeat());
            beatDuration = nextBeatTime;
        }
        if (Time.time >= nextBeatTime)
        {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
                nextBeatTime = Time.time + beatDuration;
        }
    }
}