using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetSong : MonoBehaviour
{
    public string scene = "";

    // Start is called before the first frame update
    void Start()
    {

    }


    public void startGame()
    {
        SceneManager.LoadScene(scene);
    }

    public void song60BPM()
    {
        GlobalVariables.songChoice = "event:/60BPM";
    }

    public void songBoogie()
    {
        GlobalVariables.songChoice = "event:/BoogieSong";
    }

    public void songRock()
    {
        GlobalVariables.songChoice = "event:/BoogieRock";
    }
}
