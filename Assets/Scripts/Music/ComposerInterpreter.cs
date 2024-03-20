using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ComposerInterpreter : MonoBehaviour
{

    public string eventObjectName;
    public Composer composer;
    public float fadeRate = 0.1f;
    public float eqChangeRate = 0.1f;
    public float countdownTimer = 10f;
    public float maxTime = 10f; //maximum time the timer can have, we can change this whenever
    public bool toggleTimer = true;
    public int mouseSpeed;
    private string song;
    
    public void Begin()
    {
        composer = this.transform.GetComponent<Composer>();
        //WaitToInitialize();
        song = GlobalVariables.songChoice;
        Initialize();

    }


    void Initialize()
    {
        if (song == "event:/BoogieSong")
        {
            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 1", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 2", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 3", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 4", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 1", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 2", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 3", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 4", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Pitch Lead", 0.5f);

        }
        else if (song == "event:/60BPM")
        {
            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 1", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 1", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Pitch Master", 0.5f);
        }
        else if (song == "event:/BoogieRock")
        {

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 1", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 2", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume 3", 0.75f);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 1", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 2", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF 3", 1);

            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Pitch Lead", 0.5f);
        }
    }

    void VolumeFader()
    {
        float volumeParameter = 0;
        GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.getParameterByName("Volume", out volumeParameter);

        if(composer.isFading)
        {
            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume", volumeParameter - fadeRate * Time.deltaTime);
        } 
        else if(volumeParameter < 1)
        {
            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume", volumeParameter + fadeRate * Time.deltaTime);
        }
    }

    public void setVolume(float volume, int track)
    {
        string parameter = "Volume " + track;

        GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName(parameter, volume);
    }

    
    void EQ()
    {
        float eqParameter = 0;
        GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.getParameterByName("EQF", out eqParameter);

        if(composer.eqEffect)
        {
            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF", eqParameter - eqChangeRate * Time.deltaTime);
        } 
        else if(eqParameter < 1)
        {
            GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF", eqParameter + eqChangeRate * Time.deltaTime);
        }
    }

    void timerDecrement()  //How the timer counts down
    {
        if (countdownTimer > 0f && toggleTimer == true)
        {
            countdownTimer -= Time.deltaTime;
        }
    }

    public void timerIncrement(float amt)  //This is so when we hold up arrow, it gradually increases in time, but never goes over the max time
    {
        countdownTimer += amt;
        if (countdownTimer > maxTime)
        {
            countdownTimer = maxTime;
        }
    }

    public void setTime(float newTime)  //Sets timer
    {
        countdownTimer = newTime;
    }

    public float getTime() //Gets current time left on timer
    {
        return countdownTimer;
    }

    public void spongeOnPlate() //This function is called from "SpongeAsCursor" and only triggers on collision stay and if the mouse is moving.
    {
    }

    public void pourBatter() //implement fmod sound here
    {
    }

    public void stopBatter()  //left this if you need another call to stop fmod sound
    {
    }

    public void setPitch(float newPitch)
    {
        GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Pitch Lead", newPitch);
    }

    public void SetLeadVolume(float volume)
    {
        GameObject.Find(eventObjectName)?.GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName(GlobalVariables.volume, volume);
    }
    IEnumerator WaitToInitialize()
    {
        yield return new WaitForSeconds(3f);
        Initialize();
    }


}
