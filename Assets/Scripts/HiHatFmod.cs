using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class HiHatFmod : MonoBehaviour
{
    //[FMODUnity.EventReference]
    public string hihat;
    public bool debugMode;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayHiHat()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(hihat, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(debugMode)
        {
            debugMode = false;
            PlayHiHat();   
        }
    }
}
