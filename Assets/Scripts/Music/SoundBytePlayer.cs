using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundBytePlayer : MonoBehaviour
{   
    private enum PlayMode
    {
        NONE,
        THREE_SOUNDS,
    };

    private PlayMode playMode;
    private string earlySound;
    private string onTimeSound;
    private string lateSound;

    public void SetSounds(string early, string onTime, string late)
    {
        earlySound = $"event:/{early}";
        onTimeSound = $"event:/{onTime}";
        lateSound = $"event:/{late}";
    }


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
}