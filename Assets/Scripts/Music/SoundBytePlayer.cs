using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundBytePlayer : MonoBehaviour
{   
    public enum PlayMode
    {
        NONE,
        THREE_SOUNDS,
    };

    private PlayMode playMode;
    private string earlySound;
    private string onTimeSound;
    private string lateSound;
    [SerializeField]
    private FMODUnity.StudioEventEmitter emitter;

    public void SetSounds(string early, string onTime, string late)
    {
        earlySound = $"event:/{early}";
        onTimeSound = $"event:/{onTime}";
        lateSound = $"event:/{late}";
    }

    public void SetPlayMode(PlayMode playMode) { this.playMode = playMode; }


    public void PlayOnTime()
    {
        RuntimeManager.PlayOneShotAttached(onTimeSound, gameObject);
    }

    public void PlayEarly()
    {
        if (playMode == PlayMode.THREE_SOUNDS)
        {
            RuntimeManager.PlayOneShotAttached(earlySound, gameObject);
        }
        else
        {
            RuntimeManager.PlayOneShotAttached(onTimeSound, gameObject);
        }
    }

    public void PlayLate()
    {
        if (playMode == PlayMode.THREE_SOUNDS)
        {
            RuntimeManager.PlayOneShotAttached(lateSound, gameObject);
        }
        else
        {
            RuntimeManager.PlayOneShotAttached(onTimeSound, gameObject);
        }
    }

    public void PlayRight()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.rightPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public void PlayLeft()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.leftPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public void PlayUp()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.upPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public void PlayDown()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.downPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public void PlayUpRight()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.upRightPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public void PlayUpLeft()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.upLeftPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public void PlayDownRight()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.downRightPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public void PlayDownLeft()
    {
        GlobalVariables.instance = FMODUnity.RuntimeManager.CreateInstance($"event:/{GlobalVariables.downLeftPrep}");
        GlobalVariables.instance.start();
        GlobalVariables.instance.release();
    }

    public bool isPlaying(string fEvent) 
    {
        FMOD.Studio.PLAYBACK_STATE state;
        GlobalVariables.instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}