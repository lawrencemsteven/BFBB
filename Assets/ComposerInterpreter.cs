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
}
