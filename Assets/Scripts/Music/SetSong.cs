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

        GlobalVariables.upPrep = "D";
        GlobalVariables.upRightPrep = "C2";
        GlobalVariables.upLeftPrep = "F";
        GlobalVariables.downPrep = "Eb";
        GlobalVariables.downRightPrep = "Ab";
        GlobalVariables.downLeftPrep = "G";
        GlobalVariables.rightPrep = "D";
        GlobalVariables.leftPrep = "C";

        GlobalVariables.volume = "Volume 2";

    }

    public void songBoogie()
    {
        GlobalVariables.songChoice = "event:/BoogieSong";

        GlobalVariables.bpm = 120u;

        GlobalVariables.earlyDish = "EarlyDish";

        GlobalVariables.onTimeDish = "HiHat";

        GlobalVariables.lateDish = "LateDish";

        GlobalVariables.upPrep = "Db";
        GlobalVariables.upRightPrep = "Bb";
        GlobalVariables.upLeftPrep = "F";
        GlobalVariables.downPrep = "F";
        GlobalVariables.downRightPrep = "A";
        GlobalVariables.downLeftPrep = "G";
        GlobalVariables.rightPrep = "D";
        GlobalVariables.leftPrep = "C";

        GlobalVariables.volume = "Volume 1";
    }

    public void songRock()
    {
        GlobalVariables.songChoice = "event:/BoogieRock";

        GlobalVariables.bpm = 180u;

        GlobalVariables.earlyDish = "EarlyDish";

        GlobalVariables.onTimeDish = "HiHat";

        GlobalVariables.lateDish = "LateDish";

        GlobalVariables.upPrep = "D";
        GlobalVariables.upRightPrep = "B";
        GlobalVariables.upLeftPrep = "E";
        GlobalVariables.downPrep = "Gb";
        GlobalVariables.downRightPrep = "G";
        GlobalVariables.downLeftPrep = "E";
        GlobalVariables.rightPrep = "A";
        GlobalVariables.leftPrep = "D";

        GlobalVariables.volume = "Volume 2";

    }
}
