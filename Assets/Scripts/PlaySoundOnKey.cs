using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnKey : MonoBehaviour
{
    public AudioSource[] soundToPlay;
    public GameObject camera2;
    public GameObject camera1;


    // Update is called once per frame
    void Update()
    {
        //Checks if Camera1 is enabled. If it is, these sounds will be bound instead of camera2
        if (camera1.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                soundToPlay[0].Play();
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                soundToPlay[1].Play();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                soundToPlay[2].Play();
            }
        }
        if (camera2.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                soundToPlay[3].Play();
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                soundToPlay[4].Play();
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                soundToPlay[5].Play();
            }
        }
    }
}
