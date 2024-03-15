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
        GameInfoManager.Instance.ReadFile();
        SceneManager.LoadScene(scene);
        //GameInfoManager.Instance.WriteFile();
    }

    public void song60BPM()
    {
        GlobalVariables.songChoice = "event:/60BPM";

        GlobalVariables.bpm = 60u;

        GlobalVariables.earlyDish = "EarlyDish";

        GlobalVariables.onTimeDish = "HiHat";

        GlobalVariables.lateDish = "LateDish";

    }

    public void songBoogie()
    {
        GlobalVariables.songChoice = "event:/BoogieSong";

        GlobalVariables.bpm = 120u;

        GlobalVariables.earlyDish = "EarlyDish";

        GlobalVariables.onTimeDish = "HiHat";

        GlobalVariables.lateDish = "LateDish";
    }

    public void songRock()
    {
        GlobalVariables.songChoice = "event:/BoogieRock";

        GlobalVariables.bpm = 180u;

        GlobalVariables.earlyDish = "EarlyDish";

        GlobalVariables.onTimeDish = "HiHat";

        GlobalVariables.lateDish = "LateDish";
    }
}
