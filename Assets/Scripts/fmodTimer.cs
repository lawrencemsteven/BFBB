using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;
using UnityEngine.UIElements;

public class fmodTimer : MonoBehaviour
{
    /*
    public string eventEmitterName;
    public StudioEventEmitter emitter;
    public EventReference eventRef;
    public FMOD.Studio.EventInstance eventInstance;
    public int timelinePos;
    */
    public string eventObjectName;
    //public ScriptUsageTimeline event;
    public ScriptUsageTimeline.TimelineInfo timeline;
    public int beat;
    public int bar;
    public int position;
    public int previousBarPosition = -1;
    public int positionBarLength = -1;
    private int previousBar = -1;
    //public FMOD.Studio.TIMELINE_BEAT_PROPERTIES beatProperties;

    // Start is called before the first frame update
    void Start()
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
        position = GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().timelineInfo.position;
        if (bar != previousBar)
        {
            OnBarChange();
        }

        //if(Input.GetKeyDown(KeyCode.0)) 
        //    print(eventRef.getTimelinePosition());
        //eventInstance.getTimelinePosition(out timelinePos);
    }

    void OnBarChange()
    {
        positionBarLength = position - previousBarPosition;
        previousBar = bar;
        previousBarPosition = GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().timelineInfo.position;
    }

    public bool OnBar()
    {
        Debug.Log("::::" +position);
        Debug.Log("::::" +previousBarPosition);
        if ((Mathf.Abs(position - previousBarPosition) <= 100) || ((Mathf.Abs((position - previousBarPosition) - positionBarLength)) <= 100) || (Mathf.Abs((position - previousBarPosition) - 1333) <= 100))
        {
            Debug.Log("Yes on bar");
            return true;
        }
        else
        {
            Debug.Log("Not on bar");
            return false;
        }
    }

    public bool OnXBars(int position1, int position2, float barNum)
    {
        Debug.Log("POS1: " + position1 + " POS2: " + position2 + " BARLENGTH: " + positionBarLength);
        float debugnum;
        debugnum = Mathf.Abs(((position2 - position1)) - ((positionBarLength) * barNum));
        Debug.Log(debugnum);
        Debug.Log("is less than " + 0.9 * positionBarLength);
        if ((Mathf.Abs(((position2 - position1)) - ((positionBarLength) * barNum))) < (0.9 * positionBarLength))
        {
            Debug.Log("Everything else is good");
        }
        if (((Mathf.Abs(((position2 - position1)) - ((positionBarLength) * barNum))) < (0.9 * positionBarLength)) && (OnBar()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
