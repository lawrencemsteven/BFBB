using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ParameterScript : MonoBehaviour
{
    public GameObject parameterScreen;
    public AudioMixer audioMixer;
    bool ParameterScreenOn = false;
    public UpDownMovement upDownMovement1;
    public UpDownMovement upDownMovement2;
    public UpDownMovement upDownMovement3;

    public MusicController musicController;
    public FreeSpongeMovement sponge1;
    private float newVol;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ParameterScreenOn == false)
            {
                AudioListener.pause = true;
                Time.timeScale = 0;
                ParameterScreenOn = true;
                parameterScreen.SetActive(true);
            }
            else
            {
                AudioListener.pause = false;
                Time.timeScale = 1f;
                ParameterScreenOn = false;
                parameterScreen.SetActive(false);

            }
        }
    }

    public void setAudio(float volumeNum)
    {
        newVol = Mathf.Log(volumeNum) * 20;
        audioMixer.SetFloat("Volume", newVol);
        PlayerPrefs.SetFloat("Volume", newVol);
        PlayerPrefs.Save();
    }

    public void changeMove(float moveNum)
    {
        sponge1.moveSpeed = moveNum;
    }

    public void changeBPM(float bpmValue)
    {
        Debug.Log(bpmValue);
        upDownMovement1.bpm = bpmValue;
        upDownMovement2.bpm = bpmValue;
        upDownMovement3.bpm = bpmValue;
        musicController.UpdateMusicTempo(bpmValue);
    }

    public void changePlateSize(float scaleBy)
    {
        DishStationManager.Instance.SetScale(scaleBy);
    }

}

