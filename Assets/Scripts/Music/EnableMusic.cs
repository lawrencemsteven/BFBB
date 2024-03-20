using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMusic : MonoBehaviour
{

    public GameObject MusicPlayer;
    public GameObject Metronome;

    public GameObject ComposerInterpreter;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void PlayMusic() {
        Metronome.SetActive(true);
        MusicPlayer.SetActive(true);
        ComposerInterpreter.SetActive(true);
        
    }
}
