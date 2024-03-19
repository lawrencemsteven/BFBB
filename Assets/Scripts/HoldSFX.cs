using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using System;

public class HoldSFX : MonoBehaviour
{
    public StudioEventEmitter emitter;
    [SerializeField]
    private bool isPlaying = false;
    public KeyCode inputCondition;
    public string inputConditionString;
    public bool active = false;



    // Start is called before the first frame update
    void Start()
    {
        SetInputCondition(inputConditionString);
    }

    // Update is called once per frame
    void Update()
    {
        if (Stations.Pancake.IsRunning())
        {
            if(!isPlaying && Input.GetKeyDown(inputCondition))
            {
                isPlaying = true;
                emitter.Play();
            }

            if(isPlaying && Input.GetKeyUp(inputCondition))
            {
                isPlaying = false;
                emitter.Stop();
            }
        }
    }

    void SetInputCondition(string condition)
    {
        try
        {
            inputCondition = (KeyCode) System.Enum.Parse(typeof(KeyCode), condition);
        }
        catch(Exception e)
        {
            print(e.Message);
        }
    }

    //Call if the sound should be played when the input condition is met
    //i.e. the player is in the appropriate station
    public void Activate()
    {
        active = true;
    }

    public void Deactivate()
    {
        active = false;
    }
}
