using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public ParameterScript parameterScript;

    void Start()
    {
    }

    // Callback method to update the music tempo based on BPM
    public void UpdateMusicTempo(float newBPM)
    {
        // Adjust the pitch to change the tempo
        float tempoRatio = (newBPM / 139f);
        musicAudioSource.pitch = tempoRatio;
    }

    public void ChangeVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }
}
