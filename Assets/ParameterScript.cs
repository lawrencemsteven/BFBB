using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParameterScript : MonoBehaviour
{
    public GameObject parameterScreen;
    public AudioMixer audioMixer;
    bool ParameterScreenOn = false;

    public SpongeMover sponge1;
    public SpongeMover sponge2;
    public SpongeMover sponge3;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (ParameterScreenOn == false) {
                AudioListener.pause = true;
                Time.timeScale = 0;
                ParameterScreenOn = true;
                parameterScreen.SetActive(true);
            }
            else {
                AudioListener.pause = false;
                Time.timeScale = 1f;
                ParameterScreenOn = false;
                parameterScreen.SetActive(false);

            }
        }
    }

    public void setAudio(float volumeNum) {
        audioMixer.SetFloat("Volume", volumeNum);
    }

    public void changeMove(float moveNum) {
        sponge1.moveSpeed = moveNum;
        sponge2.moveSpeed = moveNum;
        sponge3.moveSpeed = moveNum;
    }
}
