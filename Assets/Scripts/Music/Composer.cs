using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ComposerInterpreter))]
public class Composer : Singleton<Composer>
{
    private float countdownTimer = 10f;
    public bool debugTimer = true;
    public bool isFading = false;  //Meant to mean is fading out, will be replaced by isFade
    public bool eqEffect = false;
    public int isFade = 0;  //Replacement for isFading, I did not touch isFading since you needed it to work, so please implement this when possible, I have it right now so the states change but nothing happens
    private float newTime;
    private ComposerInterpreter composerInterpreter;
    public static float MAX_PITCH = 1.0f;
    public static float MIN_PITCH = 0.0f;
    public static float DEF_PITCH = 0.33f;
    public static float MAX_VOLUME = 1.0f;
    public static float MIN_VOLUME = 0.0f;
    public static float DEF_VOLUME = 1f;
    [SerializeField] private HiHatFmod hiHatFmod;

    protected override void Awake()
    {
        base.Awake();
        composerInterpreter = GetComponent<ComposerInterpreter>();
    }

    // Update is called once per frame
    void Update()
    {

        newTime = composerInterpreter.getTime();
        //Simulates event triggers for music fade in/fade out
        if (Input.GetKeyDown(KeyCode.DownArrow) || newTime <= 0f)  //Event that will be triggered when internal timer hits 0 (10 seconds), or if we trigger with down arrow
        {
            isFading = true;
            isFade = 1;
        }

        if (Input.GetKey(KeyCode.UpArrow) && isFading == false)  //Holding the key simulates being active on the station
        {
            composerInterpreter.timerIncrement(2 * Time.deltaTime);
            newTime = composerInterpreter.getTime();// Timer increases by 1 second for each second held, can be done for volume as well
            isFade = 2;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && isFading == true)  //When the player stopsworking on the station or station is back to full volume
        {
            composerInterpreter.setTime(10f);
            isFading = false;
            isFade = 0;
        }


        //Low pass eq on master or component tracks
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            eqEffect = !eqEffect;
        }

        if (GlobalVariables.camState == 3)
        {
            //Pour batter on left click hold
            if (Input.GetMouseButtonDown(0))
            {
                composerInterpreter.pourBatter();
            }
            if (Input.GetMouseButtonUp(0))
            {
                composerInterpreter.stopBatter();
            }
        }
    }

    public void VolumeChange(int track, float volume)
    {
        volume -= 1;
        if (volume >= 0.0f)
        {
            composerInterpreter.setVolume(Mathf.Lerp(DEF_VOLUME, MAX_VOLUME, volume), track);
        }
        else
        {
            composerInterpreter.setVolume(Mathf.Lerp(DEF_VOLUME, MIN_VOLUME, -volume), track);
        }
    }

    public void PitchChange(float pitch)
    {
        pitch = Mathf.Clamp(pitch, -1.0f, 1.0f);

        if (GlobalVariables.camState == 1)
        {

            if (pitch >= 0.0f)
            {
                composerInterpreter.setPitch(Mathf.Lerp(DEF_PITCH, MAX_PITCH, pitch));
            }
            else
            {
                composerInterpreter.setPitch(Mathf.Lerp(DEF_PITCH, MIN_PITCH, -pitch));
            }
        }
        else
        {
            composerInterpreter.setPitch(0f);
        }
    }

    public void PlayHiHat()
    {
        hiHatFmod.PlayHiHat();
    }
    public void PlayEarlyHit()
    {
        hiHatFmod.PlaySqueak();
    }

    public void PlayLateHit()
    {
        hiHatFmod.PlayScrape();
    }


}