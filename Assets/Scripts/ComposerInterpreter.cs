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
    public int mode;
    // Start is called before the first frame update
    void Start()
    {
        composer = this.transform.GetComponent<Composer>();
    }

    // Update is called once per frame
    void Update()
    {
        //All effect functions are called every frame
        //Logic is handled within the functions
        VolumeFader();
        EQ();
        timerDecrement();

    }

    void VolumeFader()
    {
        float volumeParameter = 0;
        GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.getParameterByName("Volume", out volumeParameter);

        if(composer.isFading)
        {
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume", volumeParameter - fadeRate * Time.deltaTime);
        } 
        else if(volumeParameter < 1)
        {
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("Volume", volumeParameter + fadeRate * Time.deltaTime);
        }
    }
    
    void EQ()
    {
        float eqParameter = 0;
        GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.getParameterByName("EQF", out eqParameter);

        if(composer.eqEffect)
        {
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF", eqParameter - eqChangeRate * Time.deltaTime);
        } 
        else if(eqParameter < 1)
        {
            GameObject.Find(eventObjectName).GetComponent<ScriptUsageTimeline>().musicInstance.setParameterByName("EQF", eqParameter + eqChangeRate * Time.deltaTime);
        }
    }

    void timerDecrement()  //How the timer counts down
    {
        if (countdownTimer > 0f && toggleTimer == true)
        {
            countdownTimer -= Time.deltaTime;
            //Debug.Log("Time is " + countdownTimer);
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
        Debug.Log("Sponge is on Plate");
    }

    public void pourBatter() //implement fmod sound here
    {
        Debug.Log("Pouring Batter");
    }

    public void stopBatter()  //left this if you need another call to stop fmod sound
    {
        Debug.Log("Stopped Pouring Batter");
    }

    public static void setPitch(int hitState)
    {
        if (hitState == 0)
        {
            Debug.Log("You are behind, Pitching Down");
        }
        else if (hitState == 1)
        {
            Debug.Log("On Beat! Keep pitch/resetting pitch");
        }
        else if (hitState == 2)
        {
            Debug.Log("You are behind, Pitching Up");
        }
    }

    public static void setMode(int state)
    {
        mode = state;
    }
    

}
