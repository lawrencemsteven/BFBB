using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;
using FMOD.Studio;

public class fmodTimer : MonoBehaviour
{
    /*
    public string eventEmitterName;
    public StudioEventEmitter emitter;
    public EventReference eventRef;
    public FMOD.Studio.EventInstance eventInstance;
    public int timelinePos;
    */
    [SerializeField] private EndOfDayScript endOfDayScript;
    public string eventObjectName;
    //public ScriptUsageTimeline event;
    public ScriptUsageTimeline.TimelineInfo timeline;
    public int beat;
    public int bar;
    private int previousBeat;
    private bool beatChange = false;

    //public FMOD.Studio.TIMELINE_BEAT_PROPERTIES beatProperties;

    // Start is called before the first frame update
    public void Initialize()
    {
        /*
        if(emitter == null && GameObject.Find(eventEmitterName).GetComponent<StudioEventEmitter>())
        {
            emitter = GameObject.Find(eventEmitterName).GetComponent<StudioEventEmitter>();
            eventRef = emitter.EventReference;
            eventInstance = emitter.EventInstance;
            //beatProperties = eventInstance.FMOD_STUDIO_TIMELINE_BEAT_PROPERTIES;
        }
        */
        /*
        if(event == null && GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>())
        {
            event = GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>()
            timeline = event.timelineInfo;
        }
        */
        timeline = GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().timelineInfo;
        previousBeat = beat;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(timeline != null)
        {
            beat = timeline.currentBeat;
            bar = timeline.currentBar;
        }
        */
        
        beat = GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().timelineInfo.currentBeat;
        bar  = GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().timelineInfo.currentBar;
        if (beat != previousBeat)
        {
            beatChange = true;
        }
        else
        {
            beatChange = false;
        }
        previousBeat = beat;

        //if(Input.GetKeyDown(KeyCode.0)) 
        //    print(eventRef.getTimelinePosition());
        //eventInstance.getTimelinePosition(out timelinePos);
    }

    public bool BeatChange()
    {
        return beatChange;
    }
}
