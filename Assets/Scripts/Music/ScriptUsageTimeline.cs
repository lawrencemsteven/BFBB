using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ScriptUsageTimeline : MonoBehaviour
{
    // Variables that are modified in the callback need to be part of a seperate class.
    // This class needs to be 'blittable' otherwise it can't be pinned in memory.
    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentBar = 0;
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    public TimelineInfo timelineInfo;
    GCHandle timelineHandle;

    FMOD.Studio.EVENT_CALLBACK beatCallback;
    //IntPtr instancePtr;
    
    public FMOD.Studio.EventInstance musicInstance;

    private bool isPaused = false;

    private string eventLocation;

    void Start()
    {
        eventLocation = GlobalVariables.songChoice;
        
        timelineInfo = new TimelineInfo();

        // Explicitly create the delegate object and assign it to a member so it doesn't get freed
        // by the garbage collected while it's being used
        beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);

        musicInstance = FMODUnity.RuntimeManager.CreateInstance(eventLocation);

        // Pin the class that will store the data modified during the callback
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        // Pass the object through the userdata of the instance
        musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT /*| FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER*/);
        musicInstance.start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            musicInstance.setPaused(true);
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            musicInstance.setPaused(false);
            isPaused = false;
        }
    }

    void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }

    
    void OnGUI()
    {
        GUILayout.Box(String.Format("Current Bar = {0}, Current Beat = {1}", timelineInfo.currentBar.ToString(), timelineInfo.currentBeat.ToString()));
    }
    

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        // Retrieve the user data
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;
            
            var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
            timelineInfo.currentBar = parameter.bar;
            timelineInfo.currentBeat = parameter.beat;
                    

            /*
            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBar = parameter.bar;
                        timelineInfo.currentBeat = paramter.beat;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                    }
                    break;
            }
            */
        }
        return FMOD.RESULT.OK;
        
    }


}