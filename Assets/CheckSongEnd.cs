using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckSongEnd : MonoBehaviour
{
    [SerializeField] private fmodTimer timer;
    [SerializeField] private EndOfDayScript endOfDayScript;
    [SerializeField] private GameObject Metronome;
    [SerializeField] private GameObject ComposerInterpreter;
    [SerializeField] private ScriptUsageTimeline pauser;

    // Update is called once per frame
    void Update()
    {
        if (Metronome.activeSelf && timer.bar > GlobalVariables.songLengthMeasures)
        {
            endOfDayScript.showDisplay();
            timer.bar = 0;
            timer.beat = 0;
            DisableMusic();
        }        
    }

    public void DisableMusic() {
        pauser.Unpause();
        pauser.StopMusic();
        Metronome.SetActive(false);
        timer.gameObject.SetActive(false);
        ComposerInterpreter.SetActive(false);
    }
}
